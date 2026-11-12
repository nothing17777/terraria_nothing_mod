"""Minimal Wwise .bnk reader: BKHD/DIDX/DATA (embedded wems) and HIRC (events -> actions -> sounds / containers)."""
import struct

def fnv1_32(s):
    h = 2166136261
    for c in s.lower().encode():
        h = (h * 16777619) & 0xFFFFFFFF
        h ^= c
    return h

def parse(path):
    d = open(path, 'rb').read()
    out = {'didx': {}, 'data_off': None, 'hirc': {}, 'raw': d}
    p = 0
    while p + 8 <= len(d):
        tag = d[p:p + 4]; sz = struct.unpack_from('<I', d, p + 4)[0]; body = p + 8
        if tag == b'DIDX':
            for i in range(sz // 12):
                wid, off, ln = struct.unpack_from('<III', d, body + i * 12)
                out['didx'][wid] = (off, ln)
        elif tag == b'DATA':
            out['data_off'] = body
        elif tag == b'HIRC':
            n = struct.unpack_from('<I', d, body)[0]; q = body + 4
            for _ in range(n):
                t = d[q]; ln = struct.unpack_from('<I', d, q + 1)[0]
                oid = struct.unpack_from('<I', d, q + 5)[0]
                out['hirc'][oid] = (t, d[q + 5:q + 5 + ln])
                q += 5 + ln
        p = body + sz
    return out

def wem_bytes(b, wid):
    off, ln = b['didx'][wid]
    return b['raw'][b['data_off'] + off:b['data_off'] + off + ln]

def event_sounds(b, ev_id, seen=None):
    """Resolve an event to the list of source ids (wem file ids or external ids) it can play."""
    t, body = b['hirc'][ev_id]
    res = []
    if t != 4: return res
    cnt = body[4]  # Wwise 2016+: u8 action count
    ids = [struct.unpack_from('<I', body, 5 + 4 * i)[0] for i in range(cnt)]
    for aid in ids:
        if aid not in b['hirc'] or b['hirc'][aid][0] != 3: continue
        ab = b['hirc'][aid][1]
        at = struct.unpack_from('<H', ab, 4)[0]
        tgt = struct.unpack_from('<I', ab, 6)[0]
        if (at >> 8) & 0xFF == 0x04 or at in (0x0403, 0x0400):  # play
            res += sounds_of(b, tgt, set())
    return res

def sounds_of(b, oid, seen):
    if oid in seen or oid not in b['hirc']: return []
    seen.add(oid)
    t, body = b['hirc'][oid]
    if t == 2:  # Sound SFX: u32 id, u32 unk, u32 stream type (0 embedded,1 stream,2 prefetch), u32 source id
        cands = [struct.unpack_from('<I', body, o)[0] for o in (9, 12, 13)]  # layout differs between Wwise versions; the real id exists in DIDX
        sid = next((c for c in cands if c in b['didx']), cands[0])
        return [sid]
    if t in (5, 6, 7, 9):  # containers: children list sits after a variable header; scan for known ids
        out = []
        for i in range(0, len(body) - 3):
            cid = struct.unpack_from('<I', body, i)[0]
            if cid != oid and cid in b['hirc'] and b['hirc'][cid][0] in (2, 5, 6, 7, 9):
                out += sounds_of(b, cid, seen)
        return out
    return []
