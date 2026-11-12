#!/usr/bin/env python3
"""Pose each champion's base-skin mesh with its .anm animations and render side-view sprite sheets (GPU, moderngl).
Rows: 0 idle, 1 run, 2 attack, 3-6 spell Q/W/E/R.  Columns: FRAMES frames.  Output: Content/LoL/Anim/<Alias>.png
plus tools/anim_meta/<Alias>.json, and Content/LoL/LolAnimData.cs (merged from all meta files).
Usage: python lol_anim_render.py [Alias ...]"""
import os, sys, json, struct, glob
import numpy as np
import moderngl
from PIL import Image
sys.path.insert(0, os.path.dirname(os.path.abspath(__file__)))
import lol_render_models as M
from lol_anim import Skeleton, Animation, skin_vertices

ROOT = os.path.abspath(os.path.join(os.path.dirname(os.path.abspath(__file__)), '..'))
OUT = f'{ROOT}/nothing_mod/Content/LoL/Anim'
META = f'{ROOT}/tools/anim_meta'
FRAMES = int(os.environ.get('LOL_FRAMES', 8))
PX_H = 128           # idle character height in pixels
CAP = 240            # max frame width/height
SKIP = M.SKIP_PARTS
EXTRA_SKIP = {'Yuumi': ('props',)}  # per-champion extra parts to leave out
ROWS = ['idle', 'run', 'attack', 'spell1', 'spell2', 'spell3', 'spell4']
BAD = ('toidle', 'torun', '_to_', 'to_idle', 'to_run', 'haste', 'fast', 'var', 'injured', 'low', 'loop', '_in', '_out', 'end', 'cancel', 'death', 'stun', 'recall', 'dance', 'joke', 'taunt', 'laugh', 'knockup', 'respawn', 'channel')

def pick(names, key, extra_bad=()):
    c = [n for n in names if key in n and not any(b in n for b in BAD + tuple(extra_bad))]
    return min(c, key=lambda n: (len(n), n)) if c else None

def choose(names):
    low = {n.lower(): n for n in names}
    ln = list(low)
    ch = {'idle': pick(ln, 'idle', ('attack', 'spell')), 'run': pick(ln, 'run', ('attack', 'spell', 'idle')),
          'attack': pick(ln, 'attack', ('spell', 'crit', 'idle', 'run', 'ult', 'special', 'passive'))}
    for i in range(1, 5):
        ch[f'spell{i}'] = (pick(ln, f'spell{i}', ('idle', 'run', 'attack', 'east', 'west', 'north', 'south', 'ult'))
                           or pick(ln, f'spell{i}_east', ('idle', 'run', 'attack', 'ult')))  # directional casts, e.g. Ahri E
    out = {}
    for r in ROWS:
        n = ch.get(r) or (ch['attack'] if r.startswith('spell') else None) or ch['idle'] or ch['run']
        out[r] = low[n] if n else None
    return out

VS = '''#version 330
in vec3 p; in vec2 uv; in float sh;
out vec2 v_uv; out float v_sh;
void main(){ gl_Position = vec4(p, 1.0); v_uv = uv; v_sh = sh; }'''
FS = '''#version 330
uniform sampler2D tex; uniform int has_tex;
in vec2 v_uv; in float v_sh; out vec4 c;
void main(){
  vec4 t = has_tex == 1 ? texture(tex, v_uv) : vec4(0.8,0.8,0.8,1.0);
  if (t.a < 0.5) discard;
  c = vec4(t.rgb * v_sh, 1.0);
}'''

def read_skn_full(path):
    d = open(path, 'rb').read()
    magic, major, minor = struct.unpack('<IHH', d[:8]); o = 8
    subs = []
    (n,) = struct.unpack_from('<I', d, o); o += 4
    for _ in range(n):
        name = d[o:o + 64].split(b'\0')[0].decode('ascii', 'ignore')
        sv, nv, si, ni = struct.unpack_from('<4I', d, o + 64); subs.append((name, sv, nv, si, ni)); o += 80
    if major >= 4: o += 4
    nidx, nvert = struct.unpack_from('<2I', d, o); o += 8
    vsize = 52
    if major >= 4:
        vsize, _ = struct.unpack_from('<2I', d, o); o += 8; o += 40
    idx = np.frombuffer(d, '<u2', nidx, o).astype(np.uint32); o += nidx * 2
    raw = np.frombuffer(d, np.uint8, nvert * vsize, o).reshape(nvert, vsize)
    f = lambda a, b, k: raw[:, a:b].copy().view('<f4').reshape(nvert, k)
    return subs, idx, f(0, 12, 3), raw[:, 12:16].astype(int), f(16, 32, 4), f(32, 44, 3), f(44, 52, 2)

def render_champion(ctx, alias):
    cdir = f'{M.EXT}/{alias}/assets/characters'
    own = {n.lower(): n for n in os.listdir(cdir)}[alias.lower()]
    skin = f'{cdir}/{own}/skins/base'
    skn = max((f for f in os.listdir(skin) if f.endswith('.skn')), key=lambda f: os.path.getsize(f'{skin}/{f}'))  # largest mesh = the real body
    skl = skn[:-4] + '.skl' if os.path.exists(f'{skin}/{skn[:-4]}.skl') else [f for f in os.listdir(skin) if f.endswith('.skl')][0]
    subs, idx, pos, bidx, wts, nor, uv = read_skn_full(f'{skin}/{skn}')
    sk = Skeleton(f'{skin}/{skl}')
    SKIP = M.SKIP_PARTS + EXTRA_SKIP.get(alias, ())
    adir = f'{skin}/animations'
    names = [f for f in os.listdir(adir) if f.endswith('.anm')]
    sel = choose(names)
    keep = np.zeros(len(pos), bool)
    for name, sv, nv, si, ni in subs:
        if not any(k in name.lower() for k in SKIP): keep[np.unique(idx[si:si + ni])] = True
    # ---- pose every row/frame
    posed = []; shade = []; used = {}
    cache = {}
    light = np.array([-0.55, 0.45, 0.70], np.float32)
    for r in ROWS:
        fn = sel[r]
        anim = cache.get(fn) if fn in cache else None
        if fn and fn not in cache:
            try: cache[fn] = Animation(f'{adir}/{fn}')
            except Exception as e: print('  anim fail', fn, e); cache[fn] = None
        anim = cache.get(fn) if fn else None
        used[r] = fn if anim else None
        dur = max(0.1, min(anim.duration, 3.0)) if anim else 1.0
        loop = r in ('idle', 'run')
        for i in range(FRAMES):
            t = (i / FRAMES * dur) if loop else (i / (FRAMES - 1) * min(dur, 3.0) if anim else 0)
            if anim: T, S, Q = anim.sample(sk, t)
            else: T, S, Q = sk.lt, sk.ls, sk.lr
            mats = sk.skin_matrices(T, S, Q)
            P = skin_vertices(sk, mats, pos, bidx, wts)
            jid = sk.influences[bidx]
            N = np.einsum('vkij,vj->vki', mats[jid][..., :3, :3], nor)
            N = (N * wts[..., None]).sum(1)
            N /= np.maximum(np.linalg.norm(N, axis=1, keepdims=True), 1e-6)
            posed.append(P.astype(np.float32)); shade.append((0.55 + 0.6 * np.clip(N @ light, 0, 1)).astype(np.float32))
    allp = np.stack(posed)                          # (R*F, V, 3)
    sx = allp[..., 2]; sy = allp[..., 1]
    k = keep & np.isfinite(allp).all((0, 2))
    x0, x1 = sx[:, k].min(), sx[:, k].max(); y0, y1 = sy[:, k].min(), sy[:, k].max()
    big = max((x for x in subs if not any(q in x[0].lower() for q in SKIP)), key=lambda x: x[2])  # largest part = body
    mk = np.zeros(len(pos), bool); mk[np.unique(idx[big[3]:big[3] + big[4]])] = True
    idle_h = sy[0, mk].max() - sy[0, mk].min()
    s = PX_H / idle_h
    W = int((x1 - x0) * s) + 6; H = int((y1 - y0) * s) + 6
    if max(W, H) > CAP: f = CAP / max(W, H); s *= f; W = int((x1 - x0) * s) + 6; H = int((y1 - y0) * s) + 6
    pad = 3 / s
    cx0, cy0 = x0 - pad, y0 - pad
    zmin, zmax = allp[..., 0][:, k].min() - 1, allp[..., 0][:, k].max() + 1
    # ---- GL setup
    prog = ctx.program(vertex_shader=VS, fragment_shader=FS)
    ibuf = ctx.buffer(idx.tobytes())
    uvbuf = ctx.buffer(uv.astype('f4').tobytes())
    pbuf = ctx.buffer(reserve=len(pos) * 12); sbuf = ctx.buffer(reserve=len(pos) * 4)
    vao = ctx.vertex_array(prog, [(pbuf, '3f', 'p'), (uvbuf, '2f', 'uv'), (sbuf, '1f', 'sh')], ibuf)
    texs = []
    for name, sv, nv, si, ni in subs:
        tp = M.pick_tex(skin, name, alias); im = M.read_tex(tp) if tp else None
        if im is not None:
            t = ctx.texture(im.size, 4, im.tobytes()); t.build_mipmaps(); t.filter = (moderngl.LINEAR_MIPMAP_LINEAR, moderngl.LINEAR)
            t.repeat_x = t.repeat_y = True
        else: t = None
        texs.append(t)
    msaa = ctx.simple_framebuffer((W, H), components=4, samples=4)
    out_fb = ctx.simple_framebuffer((W, H), components=4)
    sheet = Image.new('RGBA', (W * FRAMES, H * len(ROWS)))
    ctx.enable(moderngl.DEPTH_TEST)
    for n in range(len(ROWS) * FRAMES):
        P = allp[n]
        clip = np.empty((len(pos), 3), np.float32)
        clip[:, 0] = (sx[n] - cx0) / (W / s) * 2 - 1
        clip[:, 1] = (sy[n] - cy0) / (H / s) * 2 - 1
        clip[:, 2] = (P[:, 0] - zmin) / (zmax - zmin) * 2 - 1
        pbuf.write(clip.tobytes()); sbuf.write(shade[n].tobytes())
        msaa.use(); ctx.clear(0, 0, 0, 0, depth=1.0)
        for (name, sv, nv, si, ni), t in zip(subs, texs):
            if any(kk in name.lower() for kk in SKIP): continue
            if t is not None: t.use(0); prog['tex'].value = 0
            prog['has_tex'].value = 1 if t is not None else 0
            vao.render(moderngl.TRIANGLES, vertices=ni, first=si)
        ctx.copy_framebuffer(out_fb, msaa)
        img = Image.frombytes('RGBA', (W, H), out_fb.read(components=4)).transpose(Image.FLIP_TOP_BOTTOM)
        sheet.paste(img, ((n % FRAMES) * W, (n // FRAMES) * H))
    for b in (ibuf, uvbuf, pbuf, sbuf, msaa, out_fb): b.release()
    for t in texs:
        if t: t.release()
    vao.release(); prog.release()
    os.makedirs(OUT, exist_ok=True); os.makedirs(META, exist_ok=True)
    sheet.save(f'{OUT}/{alias}.png', optimize=True)
    meta = dict(w=W, h=H, ox=int((sx[0, k].min() + sx[0, k].max()) / 2 - cx0) * 0 + int(((sx[0, k].min() + sx[0, k].max()) / 2 - cx0) * s),
                fy=int((cy0 + H / s - sy[0, k].min()) * s), frames=FRAMES, used=used)
    json.dump(meta, open(f'{META}/{alias}.json', 'w'))
    return meta

def write_cs():
    rows = []
    for f in sorted(glob.glob(f'{META}/*.json')):
        a = os.path.basename(f)[:-5]; m = json.load(open(f))
        rows.append(f'\t\t\t["{a}"] = new AnimDef({m["w"]}, {m["h"]}, {m["ox"]}, {m["fy"]}, {m["frames"]}),')
    open(f'{ROOT}/nothing_mod/Content/LoL/LolAnimData.cs', 'w').write('''// <auto-generated> by tools/lol_anim_render.py. Do not edit. </auto-generated>
using System.Collections.Generic;

namespace nothing_mod.Content.LoL
{
	// Sprite-sheet layout per champion: frame size, x origin and feet-y inside a frame, frames per row.
	// Rows: 0 idle, 1 run, 2 attack, 3-6 spells Q/W/E/R.
	public class AnimDef
	{
		public int W, H, OriginX, FeetY, Frames;
		public AnimDef(int w, int h, int ox, int fy, int frames) { W = w; H = h; OriginX = ox; FeetY = fy; Frames = frames; }
	}

	public static class LolAnimData
	{
		public static readonly Dictionary<string, AnimDef> Sheets = new()
		{
''' + '\n'.join(rows) + '''
		};
	}
}
''')

if __name__ == '__main__':
    ctx = moderngl.create_standalone_context()
    names = sys.argv[1:] or [n for n in sorted(os.listdir(M.EXT)) if n != 'TFTChampion']
    ok = fail = 0
    for a in names:
        try:
            m = render_champion(ctx, a); ok += 1
            print('ok', a, m['w'], m['h'], {k: v for k, v in m['used'].items()}, flush=True)
        except Exception as e:
            fail += 1; print('FAIL', a, type(e).__name__, e, flush=True)
    write_cs()
    print('done', ok, 'ok', fail, 'failed')
