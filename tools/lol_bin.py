"""Reader for League of Legends .bin property files ('PROP'), with CommunityDragon hash-name resolution.
Types: ids >= 0x80 are the complex ones (list, list2, pointer, embed, link, option, map, flag)."""
import struct, os

HASH_DIR = os.path.join(os.path.dirname(os.path.abspath(__file__)), 'hashes')

def fnv1a(s):
    h = 0x811C9DC5
    for c in s.lower().encode():
        h = ((h ^ c) * 0x01000193) & 0xFFFFFFFF
    return h

_names = {}
def load_hashes(files=('binfields', 'bintypes', 'binentries', 'binhashes')):
    for f in files:
        p = os.path.join(HASH_DIR, f'hashes.{f}.txt')
        if not os.path.exists(p): continue
        with open(p, 'r', errors='ignore') as fh:
            for line in fh:
                parts = line.rstrip('\n').split(' ', 1)
                if len(parts) == 2:
                    try: _names[int(parts[0], 16)] = parts[1]
                    except ValueError: pass

def name(h):
    return _names.get(h, '{%08x}' % h)

class R:
    def __init__(self, d): self.d = d; self.o = 0
    def u8(self): v = self.d[self.o]; self.o += 1; return v
    def u16(self): v = struct.unpack_from('<H', self.d, self.o)[0]; self.o += 2; return v
    def u32(self): v = struct.unpack_from('<I', self.d, self.o)[0]; self.o += 4; return v
    def f(self, fmt):
        n = struct.calcsize(fmt); v = struct.unpack_from(fmt, self.d, self.o); self.o += n
        return v[0] if len(v) == 1 else v
    def s(self): n = self.u16(); v = self.d[self.o:self.o + n].decode('latin1'); self.o += n; return v

PRIM = {0: None, 1: '?', 2: 'b', 3: 'B', 4: 'h', 5: 'H', 6: 'i', 7: 'I', 8: 'q', 9: 'Q', 10: 'f', 11: '2f', 12: '3f', 13: '4f', 14: '16f', 15: '4B'}

def read_value(r, t):
    if t in PRIM and t not in (0,):
        v = r.f('<' + PRIM[t]); return v
    if t == 0: return None
    if t == 16: return r.s()
    if t == 17: return ('hash', r.u32())
    if t == 18: return ('file', r.f('<Q'))
    if t in (0x80, 0x81):
        vt = r.u8(); r.u32(); n = r.u32()
        return [read_value(r, vt) for _ in range(n)]
    if t == 0x82:
        nh = r.u32()
        if nh == 0: return None
        r.u32(); n = r.u16()
        return {'__type': name(nh), **read_fields(r, n)}
    if t == 0x83:
        nh = r.u32(); r.u32(); n = r.u16()
        return {'__type': name(nh), **read_fields(r, n)}
    if t == 0x84: return ('link', r.u32())
    if t == 0x85:
        vt = r.u8(); n = r.u8()
        return read_value(r, vt) if n else None
    if t == 0x86:
        kt = r.u8(); vt = r.u8(); r.u32(); n = r.u32()
        return [(read_value(r, kt), read_value(r, vt)) for _ in range(n)]
    if t == 0x87: return bool(r.u8())
    raise ValueError('unknown bin type %#x at %d' % (t, r.o))

def read_fields(r, n):
    out = {}
    for _ in range(n):
        nh = r.u32(); t = r.u8()
        out[name(nh)] = read_value(r, t)
    return out

def parse(path):
    d = open(path, 'rb').read()
    r = R(d)
    if d[:4] == b'PTCH': r.o = 8; r.u32(); r.u32()  # patch header: skip
    assert d[r.o:r.o + 4] == b'PROP', 'not PROP'
    r.o += 4; ver = r.u32()
    linked = []
    if ver >= 2:
        for _ in range(r.u32()): linked.append(r.s())
    n = r.u32()
    types = [r.u32() for _ in range(n)]
    entries = {}
    for th in types:
        size = r.u32(); end = r.o + size
        path = r.u32(); cnt = r.u16()
        entries[path] = {'__type': name(th), **read_fields(r, cnt)}
        r.o = end
    return entries, linked

def entry_name(h): return name(h)
