#!/usr/bin/env python3
"""Render each champion's base-skin .skn mesh (bind pose) + .tex textures to a side-view 2D sprite.
Output: nothing_mod/Content/LoL/Models/<Alias>.png  (transparent, facing right).
Usage: python lol_render_models.py [Alias ...]   (no args = all)"""
import os, sys, struct, io, glob
import numpy as np
from PIL import Image

EXT = '/Volumes/T7 Shield/league_of_legends_cilent/extracted/champions'
OUT = os.path.abspath(os.path.join(os.path.dirname(os.path.abspath(__file__)), '..', 'nothing_mod', 'Content', 'LoL', 'Models'))
SS = 3          # supersample
SKIP_PARTS = ('tail', 'babyfox', 'ground', 'shadow', 'fx', 'orb', 'spirit')
H = 192          # final sprite height (px)

def read_tex(path):
    d = open(path, 'rb').read()
    if d[:4] != b'TEX\0': return None
    w, h, _, fmt, _, mips = struct.unpack('<HHBBBB', d[4:12])
    body = d[12:]
    if fmt == 20:  # BGRA8
        raw = body[-w * h * 4:]
        return Image.frombuffer('RGBA', (w, h), raw, 'raw', 'BGRA', 0, 1)
    if fmt in (10, 12):
        blk = 8 if fmt == 10 else 16
        size = max(1, (w + 3) // 4) * max(1, (h + 3) // 4) * blk
        raw = body[-size:] if mips else body[:size]
        hdr = bytearray(128)
        hdr[0:4] = b'DDS '
        struct.pack_into('<7I', hdr, 4, 124, 0x81007, h, w, size, 0, 1)
        struct.pack_into('<2I4s', hdr, 76, 32, 4, b'DXT1' if fmt == 10 else b'DXT5')
        hdr[76:80] = struct.pack('<I', 32)
        struct.pack_into('<I', hdr, 80, 4)
        hdr[84:88] = b'DXT1' if fmt == 10 else b'DXT5'
        struct.pack_into('<I', hdr, 108, 0x1000)
        try:
            return Image.open(io.BytesIO(bytes(hdr) + raw)).convert('RGBA')
        except Exception:
            return None
    return None

def read_skn(path):
    d = open(path, 'rb').read()
    magic, major, minor = struct.unpack('<IHH', d[:8])
    if magic != 0x00112233: raise ValueError('bad skn')
    o = 8
    subs = []
    if major >= 1:
        (n,) = struct.unpack_from('<I', d, o); o += 4
        for _ in range(n):
            name = d[o:o + 64].split(b'\0')[0].decode('ascii', 'ignore')
            sv, nv, si, ni = struct.unpack_from('<4I', d, o + 64)
            subs.append((name, sv, nv, si, ni)); o += 80
    if major >= 4: o += 4  # flags
    nidx, nvert = struct.unpack_from('<2I', d, o); o += 8
    vsize = 52; vtype = 0
    if major >= 4:
        vsize, vtype = struct.unpack_from('<2I', d, o); o += 8
        o += 24 + 16  # bbox + sphere
    idx = np.frombuffer(d, '<u2', nidx, o); o += nidx * 2
    raw = np.frombuffer(d, np.uint8, nvert * vsize, o).reshape(nvert, vsize)
    pos = raw[:, 0:12].copy().view('<f4').reshape(nvert, 3)
    nor = raw[:, 32:44].copy().view('<f4').reshape(nvert, 3) if vsize >= 52 else None
    uv = raw[:, 44:52].copy().view('<f4').reshape(nvert, 2)
    return subs, idx.astype(np.int64), pos, nor, uv

def pick_tex(dirpath, subname, alias):
    texs = [f for f in os.listdir(dirpath) if f.lower().endswith('.tex')]
    skip = ('loadscreen', 'mis_', '_fire', 'mult', 'circle', 'tile', 'splash', 'particle', 'fx_', 'joke', 'msk', 'mask')
    cand = [f for f in texs if not any(s in f.lower() for s in skip)]
    def best(fs):
        fs = [f for f in fs if 'cm' in f.lower()] or fs
        return os.path.join(dirpath, sorted(fs, key=lambda f: (len(f), f))[0]) if fs else None
    low = subname.lower().replace('_', '')
    if low:
        hit = best([f for f in cand if low in f.lower().replace('_', '')])
        if hit: return hit
        for tok in sorted(subname.lower().split('_'), key=len, reverse=True):
            if len(tok) >= 4 and tok not in ('body', 'main', 'base', 'skin'):
                hit = best([f for f in cand if tok in f.lower()])
                if hit: return hit
    main = [f for f in cand if f.lower().endswith('_tx_cm.tex')] or [f for f in cand if 'tintcolor_cm' in f.lower()]
    if main: return best(main)
    return os.path.join(dirpath, max(cand, key=lambda f: os.path.getsize(os.path.join(dirpath, f)))) if cand else None

def render(alias):
    cdir = os.path.join(EXT, alias, 'assets', 'characters')
    names = {n.lower(): n for n in os.listdir(cdir)}
    own = names.get(alias.lower())
    if not own: raise FileNotFoundError('no character dir')
    skin = os.path.join(cdir, own, 'skins', 'base')
    skn = sorted((f for f in os.listdir(skin) if f.endswith('.skn')), key=lambda f: -os.path.getsize(os.path.join(skin, f)))
    if not skn: raise FileNotFoundError('no skn')
    subs, idx, pos, nor, uv = read_skn(os.path.join(skin, skn[0]))
    if not subs: subs = [('', 0, len(pos), 0, len(idx))]
    # side view: screen x = world z, screen y = -world y. Flip later if facing left.
    X = pos[:, 2]; Y = -pos[:, 1]; Z = pos[:, 0]
    valid = np.zeros(len(pos), bool)
    for name, sv, nv, si, ni in subs:
        if not any(k in name.lower() for k in SKIP_PARTS): valid[np.unique(idx[si:si + ni])] = True
    valid &= np.isfinite(pos).all(1)
    x0, x1 = X[valid].min(), X[valid].max(); y0, y1 = Y[valid].min(), Y[valid].max()
    scale = H * SS / (y1 - y0)
    W = int((x1 - x0) * scale) + 2
    if W > 4000: scale *= 4000 / W; W = 4000
    HH = H * SS + 2
    col = np.zeros((HH, W, 4), np.uint8); zb = np.full((HH, W), np.inf)
    sx = (X - x0) * scale + 1; sy = (Y - y0) * scale + 1
    for name, sv, nv, si, ni in subs:
        if any(k in name.lower() for k in SKIP_PARTS): continue
        tp = pick_tex(skin, name, alias)
        im = read_tex(tp) if tp else None
        tex = np.asarray(im) if im is not None else None
        th, tw = (tex.shape[:2]) if tex is not None else (1, 1)
        tri = idx[si:si + ni].reshape(-1, 3)
        for a, b, c in tri:
            xs = sx[[a, b, c]]; ys = sy[[a, b, c]]
            minx, maxx = max(int(xs.min()), 0), min(int(xs.max()) + 1, W - 1)
            miny, maxy = max(int(ys.min()), 0), min(int(ys.max()) + 1, HH - 1)
            if minx > maxx or miny > maxy: continue
            den = (ys[1] - ys[2]) * (xs[0] - xs[2]) + (xs[2] - xs[1]) * (ys[0] - ys[2])
            if abs(den) < 1e-9: continue
            gx, gy = np.meshgrid(np.arange(minx, maxx + 1) + .5, np.arange(miny, maxy + 1) + .5)
            l0 = ((ys[1] - ys[2]) * (gx - xs[2]) + (xs[2] - xs[1]) * (gy - ys[2])) / den
            l1 = ((ys[2] - ys[0]) * (gx - xs[2]) + (xs[0] - xs[2]) * (gy - ys[2])) / den
            l2 = 1 - l0 - l1
            m = (l0 >= 0) & (l1 >= 0) & (l2 >= 0)
            if not m.any(): continue
            z = l0 * Z[a] + l1 * Z[b] + l2 * Z[c]
            sub = zb[miny:maxy + 1, minx:maxx + 1]
            m &= z < sub
            if tex is not None:
                u = l0 * uv[a, 0] + l1 * uv[b, 0] + l2 * uv[c, 0]
                v = l0 * uv[a, 1] + l1 * uv[b, 1] + l2 * uv[c, 1]
                px = tex[np.clip((v % 1.0) * th, 0, th - 1).astype(int), np.clip((u % 1.0) * tw, 0, tw - 1).astype(int)]
                m &= px[..., 3] > 128
            else:
                px = np.full(m.shape + (4,), 200, np.uint8)
            if nor is not None:
                nz = (l0[..., None] * nor[a] + l1[..., None] * nor[b] + l2[..., None] * nor[c])
                nz /= np.maximum(np.linalg.norm(nz, axis=-1, keepdims=True), 1e-6)
                # side view: camera looks along world x; light from upper-front
                lam = np.clip(nz @ np.array([-0.55, 0.45, 0.70]), 0, 1)
                px = px.copy()
                px[..., :3] = np.clip(px[..., :3].astype(np.float32) * (0.55 + 0.6 * lam[..., None]), 0, 255).astype(np.uint8)
            sub[m] = z[m]
            col[miny:maxy + 1, minx:maxx + 1][m] = px[m]
    img = Image.fromarray(col, 'RGBA')
    bb = img.getbbox()
    if not bb: raise ValueError('empty render')
    img = img.crop(bb)
    img = img.resize((max(1, img.width // SS), max(1, img.height // SS)), Image.LANCZOS)
    return img.transpose(Image.FLIP_LEFT_RIGHT)  # models face -x on screen; sprites face right

if __name__ == '__main__':
    names = sys.argv[1:] or sorted(os.listdir(EXT))
    os.makedirs(OUT, exist_ok=True)
    ok = fail = 0
    for a in names:
        try:
            im = render(a)
            im.save(os.path.join(OUT, a + '.png')); ok += 1
            print('ok', a, im.size, flush=True)
        except Exception as e:
            fail += 1; print('FAIL', a, e, flush=True)
    print('done', ok, 'ok', fail, 'failed')
