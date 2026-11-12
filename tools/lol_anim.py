"""League .skl skeleton + .anm animation (r3d2anmd v3/v4/v5, r3d2canm v1) loading and CPU skinning."""
import struct, os
import numpy as np

def elf_hash(name):
    h = 0
    for c in name.lower().encode():
        h = ((h << 4) + c) & 0xFFFFFFFF
        g = h & 0xF0000000
        if g: h ^= g >> 24
        h &= ~g & 0xFFFFFFFF
    return h

def quat_mat(q):
    """q: (...,4) xyzw -> (...,3,3)"""
    q = q / np.maximum(np.linalg.norm(q, axis=-1, keepdims=True), 1e-9)
    x, y, z, w = q[..., 0], q[..., 1], q[..., 2], q[..., 3]
    m = np.empty(q.shape[:-1] + (3, 3), np.float32)
    m[..., 0, 0] = 1 - 2 * (y * y + z * z); m[..., 0, 1] = 2 * (x * y - z * w); m[..., 0, 2] = 2 * (x * z + y * w)
    m[..., 1, 0] = 2 * (x * y + z * w); m[..., 1, 1] = 1 - 2 * (x * x + z * z); m[..., 1, 2] = 2 * (y * z - x * w)
    m[..., 2, 0] = 2 * (x * z - y * w); m[..., 2, 1] = 2 * (y * z + x * w); m[..., 2, 2] = 1 - 2 * (x * x + y * y)
    return m

def trs(t, s, q):
    """(...,3),(...,3),(...,4) -> (...,4,4) = T * R * S"""
    m = np.zeros(t.shape[:-1] + (4, 4), np.float32)
    m[..., :3, :3] = quat_mat(q) * s[..., None, :]
    m[..., :3, 3] = t
    m[..., 3, 3] = 1
    return m

class Skeleton:
    def __init__(self, path):
        d = open(path, 'rb').read()
        fs, magic, ver = struct.unpack_from('<3I', d, 0)
        if magic != 0x22FD4FC3: raise ValueError('old skl format')
        flags, jc, ic, jo, jio, io = struct.unpack_from('<HHIiii', d, 12 + 0)[0:6] if False else (None,) * 6
        jc = struct.unpack_from('<H', d, 14)[0]
        ic = struct.unpack_from('<I', d, 16)[0]
        jo, jio, io = struct.unpack_from('<3i', d, 20)
        self.n = jc
        self.parent = np.zeros(jc, int); self.hash = np.zeros(jc, np.uint32)
        lt = np.zeros((jc, 3), np.float32); ls = np.ones((jc, 3), np.float32); lr = np.zeros((jc, 4), np.float32)
        it = np.zeros((jc, 3), np.float32); is_ = np.ones((jc, 3), np.float32); ir = np.zeros((jc, 4), np.float32)
        for i in range(jc):
            o = jo + i * 100
            _, jid, par, _ = struct.unpack_from('<HHhH', d, o)
            h, _r = struct.unpack_from('<If', d, o + 8)
            v = struct.unpack_from('<3f3f4f3f3f4f', d, o + 16)
            self.parent[i] = par; self.hash[i] = h
            lt[i], ls[i], lr[i], it[i], is_[i], ir[i] = v[0:3], v[3:6], v[6:10], v[10:13], v[13:16], v[16:20]
        self.lt, self.ls, self.lr = lt, ls, lr
        self.inv = trs(it, is_, ir)
        self.influences = np.frombuffer(d, '<u2', ic, io).astype(int)
        self.index = {int(h): i for i, h in enumerate(self.hash)}

    def skin_matrices(self, T, S, Q):
        """T,S,Q: (J,3),(J,3),(J,4) local pose -> (J,4,4) skinning matrices"""
        loc = trs(T, S, Q)
        glob = np.empty_like(loc)
        for i in range(self.n):
            p = self.parent[i]
            glob[i] = loc[i] if p < 0 else glob[p] @ loc[i]
        return glob @ self.inv

def _quat48(b):
    """b: (N,6) uint8 -> (N,4) xyzw"""
    b = b.astype(np.uint64)
    bits = b[:, 0] | (b[:, 1] << 8) | (b[:, 2] << 16) | (b[:, 3] << 24) | (b[:, 4] << 32) | (b[:, 5] << 40)
    mi = ((bits >> 45) & 3).astype(int)
    sq2 = np.sqrt(2.0)
    comp = [(((bits >> s) & 0x7FFF).astype(np.float64) / 32767.0) * sq2 - 1 / sq2 for s in (30, 15, 0)]
    out = np.zeros((len(b), 4))
    big = np.sqrt(np.clip(1 - comp[0] ** 2 - comp[1] ** 2 - comp[2] ** 2, 0, 1))
    for m in range(4):
        sel = mi == m
        others = [i for i in range(4) if i != m]
        out[sel, m] = big[sel]
        for k, oi in enumerate(others): out[sel, oi] = comp[k][sel]
    return out.astype(np.float32)

class Animation:
    """Stores per-track keyframes keyed by joint hash; sample(t) returns T,S,Q arrays for the skeleton."""
    def __init__(self, path):
        d = open(path, 'rb').read()
        magic = d[:8]; ver = struct.unpack_from('<I', d, 8)[0]
        self.tracks = {}  # hash -> dict(type -> (times, values))
        if magic == b'r3d2canm': self._canm(d)
        elif magic == b'r3d2anmd' and ver in (4, 5): self._anmd(d, ver)
        elif magic == b'r3d2anmd' and ver == 3: self._legacy(d)
        else: raise ValueError('unsupported anm %r %d' % (magic, ver))

    def _anmd(self, d, ver):
        tc, fc = struct.unpack_from('<2i', d, 28)
        dur = struct.unpack_from('<f', d, 36)[0]
        hoff, aoff, toff, vpo, qpo, fo = [x + 12 for x in struct.unpack_from('<6i', d, 40)]  # offsets are relative to byte 12
        self.duration = dur * fc
        vecs = np.frombuffer(d, '<f4', (qpo - vpo) // 4, vpo).reshape(-1, 3)
        if ver == 5:
            quats = _quat48(np.frombuffer(d, np.uint8, (hoff - qpo) // 6 * 6, qpo).reshape(-1, 6))
            hashes = np.frombuffer(d, '<u4', tc, hoff)
            fr = np.frombuffer(d, '<u2', fc * tc * 3, fo).reshape(fc, tc, 3)
        else:
            quats = np.frombuffer(d, '<f4', (fo - qpo) // 4, qpo).reshape(-1, 4)
            raw = np.frombuffer(d, np.uint8, fc * tc * 12, fo).reshape(fc, tc, 12)
            hashes = raw[0, :, 0:4].copy().view('<u4').reshape(-1)
            fr = np.ascontiguousarray(raw[:, :, 4:10]).view('<u2').reshape(fc, tc, 3)
        times = np.arange(fc) * dur
        for t in range(tc):
            self.tracks[int(hashes[t])] = {0: (times, quats[fr[:, t, 2]]), 1: (times, vecs[fr[:, t, 0]]), 2: (times, vecs[fr[:, t, 1]])}

    def _canm(self, d):
        jc, fcnt, jcc = struct.unpack_from('<3i', d, 24)
        dur, fps = struct.unpack_from('<2f', d, 36)
        tmin = np.array(struct.unpack_from('<3f', d, 68)); tmax = np.array(struct.unpack_from('<3f', d, 80))
        smin = np.array(struct.unpack_from('<3f', d, 92)); smax = np.array(struct.unpack_from('<3f', d, 104))
        fo, jo, no = struct.unpack_from('<3i', d, 116)
        self.duration = dur
        hashes = np.frombuffer(d, '<u4', jc, no + 12)
        raw = np.frombuffer(d, np.uint8, fcnt * 10, fo + 12).reshape(fcnt, 10)
        t = raw[:, 0:2].copy().view('<u2').reshape(-1).astype(np.float32) / 65535 * dur
        jw = raw[:, 2:4].copy().view('<u2').reshape(-1)
        typ = jw >> 14; jid = jw & 0x3FFF
        val = raw[:, 4:10]
        q = _quat48(val)
        u = val.copy().view('<u2').reshape(-1, 3).astype(np.float32) / 65535
        tv = tmin + u * (tmax - tmin); sv = smin + u * (smax - smin)
        for j in range(jc):
            tr = {}
            for ty, arr in ((0, q), (1, tv), (2, sv)):
                sel = (jid == j) & (typ == ty)
                if not sel.any(): continue
                tt = t[sel]; vv = arr[sel]
                _, first = np.unique(tt, return_index=True)  # drop duplicated padding keys
                tr[ty] = (tt[first], vv[first])
            self.tracks[int(hashes[j])] = tr

    def _legacy(self, d):
        tc, fc, fps = struct.unpack_from('<3I', d, 16)
        self.duration = fc / max(fps, 1)
        o = 28
        times = np.arange(fc) / max(fps, 1)
        for _ in range(tc):
            name = d[o:o + 32].split(b'\0')[0].decode('ascii', 'ignore'); o += 36
            a = np.frombuffer(d, '<f4', fc * 7, o).reshape(fc, 7); o += fc * 28
            self.tracks[elf_hash(name)] = {0: (times, a[:, 0:4]), 1: (times, a[:, 4:7])}

    def sample(self, sk, t):
        T = sk.lt.copy(); S = sk.ls.copy(); Q = sk.lr.copy()
        for h, tr in self.tracks.items():
            j = sk.index.get(h)
            if j is None: continue
            for ty, (tt, vv) in tr.items():
                if len(tt) == 1: v = vv[0]
                elif ty == 0:
                    i = int(np.clip(np.searchsorted(tt, t) - 1, 0, len(tt) - 2))
                    f = float(np.clip((t - tt[i]) / max(tt[i + 1] - tt[i], 1e-9), 0, 1))
                    a, b = vv[i], vv[i + 1]
                    if np.dot(a, b) < 0: b = -b
                    v = a * (1 - f) + b * f
                    v = v / max(np.linalg.norm(v), 1e-9)
                    Q[j] = v; continue
                else:
                    v = np.array([np.interp(t, tt, vv[:, k]) for k in range(3)])
                if ty == 0: Q[j] = v
                elif ty == 1: T[j] = v
                else: S[j] = v
        return T, S, Q

def skin_vertices(sk, mats, pos, bone_idx, weights):
    """pos (V,3), bone_idx (V,4) skn bone indices, weights (V,4) -> posed (V,3)"""
    jid = sk.influences[bone_idx]
    m = mats[jid]                                   # (V,4,4,4)
    p = np.concatenate([pos, np.ones((len(pos), 1), np.float32)], 1)
    pp = np.einsum('vkij,vj->vki', m, p)[..., :3]  # (V,4,3)
    return (pp * weights[..., None]).sum(1)
