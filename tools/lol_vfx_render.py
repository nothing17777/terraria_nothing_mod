"""Render League VfxSystemDefinitionData entries (parsed by lol_vfx.py) as 2D sprite-particle frames.

v1 scope (anything else is counted and skipped, see SKIPPED in the report):
  - sprite (quad) primitives only; mesh primitives are skipped
  - rate, particleLifetime, emitter lifetime, isSingleParticle, timeBeforeFirstEmission
  - spawn offset (SpawnShape.emitOffset), birthVelocity, worldAcceleration, birthDrag
  - birthScale0 x scale0 curve, birthRotation0 (Z), rotation over life not applied
  - birthColor x Color gradient over life
  - blendMode: 4 = additive, anything else = alpha (UNVERIFIED mapping, from memory notes)
Front view: screen x = world x, screen y = world y (world up), z ignored.

Usage (from tools/):  .venv/bin/python lol_vfx_render.py <entry.rito> <entry_key_hex> <out_dir> <texture_dir>
"""
import math
import os
import random
import sys

import numpy as np
from PIL import Image

sys.path.insert(0, os.path.dirname(os.path.abspath(__file__)))
import lol_vfx  # noqa: E402
from lol_render_models import read_tex  # noqa: E402

ADDITIVE_BLEND = 4  # UNVERIFIED: value taken from earlier memory notes on Ahri Q orb


def _const_or_curve(node, default):
    """Return f(u) for u in [0,1] (lifetime-normalised) from a constant or a dynamics curve."""
    if not isinstance(node, dict):
        if isinstance(node, (int, float)):
            return lambda u: np.full_like(np.asarray(u, float), float(node))
        return lambda u: np.full_like(np.asarray(u, float), default)
    if 'dynamics' in node and isinstance(node['dynamics'], dict) and 'times' in node['dynamics']:
        dyn = node['dynamics']
        times = np.asarray(dyn['times'], float)
        vals = np.asarray(dyn['values'], float)
        if vals.ndim == 1:
            return lambda u: np.interp(np.asarray(u, float), times, vals)
        return lambda u: np.stack([np.interp(np.asarray(u, float), times, vals[:, i])
                                   for i in range(vals.shape[1])], axis=-1)
    cv = node.get('constantValue', default)
    if isinstance(cv, list):
        cv = np.asarray(cv, float)
        return lambda u: np.broadcast_to(cv, np.asarray(u, float).shape + cv.shape).copy()
    return lambda u: np.full_like(np.asarray(u, float), float(cv))


def _first_number(x, default):
    if isinstance(x, list) and x:
        return _first_number(x[0], default)
    if isinstance(x, (int, float)):
        return float(x)
    if isinstance(x, dict) and 'constantValue' in x:
        return _first_number(x['constantValue'], default)
    return default


def _vec3(x, default=(0.0, 0.0, 0.0)):
    if isinstance(x, dict):
        for k in ('constantValue', 'emitOffset', 'v'):
            if k in x:
                return _vec3(x[k], default)
        return default
    if isinstance(x, list) and len(x) >= 2 and all(isinstance(v, float) for v in x[:2]):
        v = list(x) + [0.0, 0.0]
        return (float(v[0]), float(v[1]), float(v[2]))
    return default


def _vec4(x, default=(1.0, 1.0, 1.0, 1.0)):
    if isinstance(x, dict) and 'constantValue' in x:
        x = x['constantValue']
    if isinstance(x, list) and len(x) >= 4 and all(isinstance(v, float) for v in x[:4]):
        return tuple(float(v) for v in x[:4])
    return default


class Emitter:
    def __init__(self, em, tex_dir, skipped):
        self.name = em.get('emitterName', '?')
        self.skipped = skipped
        self.rate = _const_or_curve(em.get('rate'), 0.0)
        self.life = _const_or_curve(em.get('particleLifetime'), 1.0)
        self.emitter_life = _first_number(em.get('lifetime'), None)
        self.single = bool(em.get('isSingleParticle', False))
        self.t0 = _first_number(em.get('timeBeforeFirstEmission'), 0.0)
        ss = em.get('SpawnShape') or {}
        self.offset = np.asarray(_vec3(ss, (0.0, 0.0, 0.0)), float)
        self.birth_scale = np.asarray(_vec3(em.get('birthScale0'), (1.0, 1.0, 1.0)), float)
        self.scale_curve = _const_or_curve(em.get('scale0'), 1.0)
        self.birth_rot = _vec3(em.get('birthRotation0'), (0.0, 0.0, 0.0))[2]
        self.birth_color = np.asarray(_vec4(em.get('birthColor')), float)
        self.color_curve = _const_or_curve(em.get('Color'), 1.0)
        self.velocity = np.asarray(_vec3(em.get('birthVelocity'), (0.0, 0.0, 0.0)), float)
        self.accel = np.asarray(_vec3(em.get('worldAcceleration'), (0.0, 0.0, 0.0)), float)
        self.drag = _first_number(em.get('birthDrag'), 0.0)
        self.additive = int(_first_number(em.get('blendMode'), 0)) == ADDITIVE_BLEND
        self.ground = bool(em.get('isGroundLayer', False))
        self.enabled = True
        self.prerendered = False
        prim = em.get('primitive')
        mesh_name = None
        if isinstance(prim, dict):
            mesh = prim.get('mMesh') if isinstance(prim.get('mMesh'), dict) else prim
            mesh_name = mesh.get('mSimpleMeshName') if isinstance(mesh, dict) else None
        if self.ground:
            skipped['ground layer (floor decal)'] = skipped.get('ground layer (floor decal)', 0) + 1
            self.enabled = False
            self.tex = None
            return
        if mesh_name:
            self.tex = None
            self.enabled = self._build_mesh(mesh_name, em, tex_dir, skipped)
            return
        for key in ('erosionDriveCurve', 'mTrail', 'FlexShapeDefinition', 'fieldCollectionDefinition',
                    'paletteDefinition', 'alphaErosionDefinition'):
            if key in em:
                skipped[key] = skipped.get(key, 0) + 1
        tex_name = os.path.basename(str(em.get('texture', ''))).lower()
        self.tex = None
        if tex_name:
            p = os.path.join(tex_dir, tex_name)
            if os.path.exists(p):
                im = read_tex(p)
                self.tex = im
                if im is not None and np.asarray(im)[..., 3].min() >= 250:
                    # opaque texture: alpha blending would paint a solid box, so use additive (HEURISTIC, unverified)
                    self.additive = True
                    skipped['opaque texture -> additive (heuristic)'] = skipped.get('opaque texture -> additive (heuristic)', 0) + 1
            else:
                skipped['missing texture'] = skipped.get('missing texture', 0) + 1


MESH_PX_SCALE = 0.25  # league units -> pixels, same as render_frames default


def rasterize_mesh(verts, uvs, tex_arr, xy_scale, rot_deg):
    """Rasterise triangles into an RGBA sprite centred on the mesh origin. Painter order, no depth."""
    xy = verts[:, :2] * xy_scale
    if rot_deg:
        a = math.radians(rot_deg)
        c, s = math.cos(a), math.sin(a)
        xy = np.stack([xy[:, 0] * c - xy[:, 1] * s, xy[:, 0] * s + xy[:, 1] * c], axis=1)
    xy = xy * MESH_PX_SCALE
    half = int(math.ceil(np.abs(xy).max())) + 2
    W = H = max(4, 2 * half)
    cx = cy = W / 2.0
    out = np.zeros((H, W, 4), np.uint8)
    th, tw = tex_arr.shape[:2]
    for t in range(0, len(xy), 3):
        p = xy[t:t + 3] + np.array([cx, cy])
        uv = uvs[t:t + 3]
        x0 = max(0, int(math.floor(p[:, 0].min())))
        x1 = min(W - 1, int(math.ceil(p[:, 0].max())))
        y0 = max(0, int(math.floor(p[:, 1].min())))
        y1 = min(H - 1, int(math.ceil(p[:, 1].max())))
        if x1 < x0 or y1 < y0:
            continue
        gx, gy = np.meshgrid(np.arange(x0, x1 + 1) + 0.5, np.arange(y0, y1 + 1) + 0.5)
        (ax, ay), (bx, by), (qx, qy) = p
        den = (by - qy) * (ax - qx) + (qx - bx) * (ay - qy)
        if abs(den) < 1e-9:
            continue
        w1 = ((by - qy) * (gx - qx) + (qx - bx) * (gy - qy)) / den
        w2 = ((qy - ay) * (gx - qx) + (ax - qx) * (gy - qy)) / den
        w3 = 1.0 - w1 - w2
        inside = (w1 >= -1e-6) & (w2 >= -1e-6) & (w3 >= -1e-6)
        if not inside.any():
            continue
        u = w1 * uv[0, 0] + w2 * uv[1, 0] + w3 * uv[2, 0]
        v = w1 * uv[0, 1] + w2 * uv[1, 1] + w3 * uv[2, 1]
        tu = (np.mod(u, 1.0) * (tw - 1)).astype(int)
        tv = (np.mod(1.0 - v, 1.0) * (th - 1)).astype(int)
        col = tex_arr[tv, tu]
        region = out[y0:y1 + 1, x0:x1 + 1]
        region[inside] = col[inside]
    return Image.fromarray(out, 'RGBA')


def _build_mesh(self, mesh_name, em, tex_dir, skipped):
    from lol_orb_render import read_scb
    mesh_path = os.path.join(tex_dir, os.path.basename(str(mesh_name)).lower())
    if not os.path.exists(mesh_path):
        skipped['missing mesh'] = skipped.get('missing mesh', 0) + 1
        return False
    verts, uvs = read_scb(mesh_path)
    tex_name = os.path.basename(str(em.get('texture', ''))).lower()
    tex_img = read_tex(os.path.join(tex_dir, tex_name)) if tex_name and os.path.exists(os.path.join(tex_dir, tex_name)) else None
    if tex_img is None:
        tex_arr = np.full((1, 1, 4), 255, np.uint8)
    else:
        tex_arr = np.asarray(tex_img, np.uint8)
    bs = np.asarray(_vec3(em.get('birthScale0'), (1.0, 1.0, 1.0)), float)
    rot = _vec3(em.get('birthRotation0'), (0.0, 0.0, 0.0))[2]
    self.tex = rasterize_mesh(verts, uvs, tex_arr, bs[:2], rot)
    if tex_arr.shape[-1] == 4 and tex_arr[..., 3].min() >= 250:
        self.additive = True  # opaque texture heuristic, same as sprites (unverified)
    self.prerendered = True
    self.base_scale = float(self.scale_curve(np.array([0.0])).reshape(-1)[0]) or 1.0
    return True


Emitter._build_mesh = _build_mesh


def simulate(emitter, duration, fps, rng):
    """Return a list of particle dicts with birth time, lifetime and initial state."""
    parts = []
    horizon = emitter.emitter_life if emitter.emitter_life else duration
    if emitter.single:
        births = [emitter.t0]
    else:
        births, t, dt_acc = [], emitter.t0, 0.0
        step = 1.0 / 240.0
        while t < min(horizon, duration):
            r = float(emitter.rate(np.array([min(1.0, (t - emitter.t0) / max(horizon, 1e-6))]))[0])
            dt_acc += r * step
            while dt_acc >= 1.0:
                births.append(t)
                dt_acc -= 1.0
            t += step
    for b in births:
        L = float(emitter.life(np.array([0.0]))[0])
        vel = emitter.velocity + np.array([rng.uniform(-1, 1) for _ in range(3)]) * 0.0
        parts.append({'birth': b, 'life': max(L, 1e-3), 'offset': emitter.offset.copy(), 'vel': vel})
    return parts


def render_frames(entries, texture_dir, duration=1.0, fps=30, scale=0.25, canvas=512, seed=1):
    """entries: list of resolved VfxSystemDefinitionData dicts. Returns frames as RGBA uint8 arrays."""
    rng = random.Random(seed)
    skipped = {}
    emitters = []
    for entry in entries:
        for em in entry.get('complexEmitterDefinitionData', []):
            e = Emitter(em, texture_dir, skipped)
            if e.enabled and e.tex is not None:
                emitters.append(e)
    frames = []
    n = int(round(duration * fps))
    sims = [(e, simulate(e, duration, fps, rng)) for e in emitters]
    cx = cy = canvas / 2
    for f in range(n):
        t = f / fps
        acc = np.zeros((canvas, canvas, 4), np.float32)
        for e, parts in sims:
            for p in parts:
                age = t - p['birth']
                if age < 0 or age > p['life']:
                    continue
                u = np.array([age / p['life']])
                pos = p['offset'] + p['vel'] * age + 0.5 * e.accel * age * age
                sv = np.asarray(e.scale_curve(u), float).reshape(-1)
                if e.prerendered:
                    w_units = h_units = None  # sprite already at birth scale; scale by curve ratio below
                    ratio = float(sv[0]) / e.base_scale
                elif sv.size >= 2:
                    w_units = e.birth_scale[0] * sv[0]
                    h_units = e.birth_scale[1] * sv[1]
                else:
                    w_units = e.birth_scale[0] * sv[0]
                    h_units = e.birth_scale[1] * sv[0]
                cv = np.asarray(e.color_curve(u), float).reshape(-1)
                if cv.size >= 4:
                    col = cv[:4]
                elif cv.size == 1:
                    col = np.array([1.0, 1.0, 1.0, cv[0]])
                else:
                    col = np.array([1.0, 1.0, 1.0, 1.0])
                rgba = np.clip(e.birth_color.astype(np.float32) * col.astype(np.float32), 0, 1)
                if e.prerendered:
                    if ratio <= 1e-3:
                        continue
                    sw0, sh0 = e.tex.size
                    w_px = max(1, int(round(sw0 * ratio)))
                    h_px = max(1, int(round(sh0 * ratio)))
                    sprite = e.tex if (w_px, h_px) == (sw0, sh0) else e.tex.resize((w_px, h_px), Image.BILINEAR)
                else:
                    w_px = max(1, int(abs(w_units) * scale))
                    h_px = max(1, int(abs(h_units) * scale))
                    w_px = min(w_px, canvas)
                    h_px = min(h_px, canvas)
                    sprite = e.tex.resize((w_px, h_px), Image.BILINEAR)
                    if e.birth_rot:
                        sprite = sprite.rotate(-e.birth_rot, expand=True, resample=Image.BILINEAR)
                arr = np.asarray(sprite, np.float32) / 255.0
                arr = arr * rgba
                sh, sw = arr.shape[:2]
                x0 = int(round(cx + pos[0] * scale - sw / 2))
                y0 = int(round(cy - pos[1] * scale - sh / 2))
                xs0, ys0 = max(0, x0), max(0, y0)
                xs1, ys1 = min(canvas, x0 + sw), min(canvas, y0 + sh)
                if xs1 <= xs0 or ys1 <= ys0:
                    continue
                src = arr[ys0 - y0:ys1 - y0, xs0 - x0:xs1 - x0]
                dst = acc[ys0:ys1, xs0:xs1]
                if e.additive:
                    add = src[..., :3] * src[..., 3:4]
                    dst[..., :3] += add
                    # alpha follows brightness so black pixels stay transparent
                    dst[..., 3] = np.clip(dst[..., 3] + add.max(axis=-1), 0, 1)
                else:
                    a = src[..., 3:4]
                    dst[..., :3] = src[..., :3] * a + dst[..., :3] * (1 - a)
                    dst[..., 3:4] = a + dst[..., 3:4] * (1 - a)
        frames.append((np.clip(acc, 0, 1) * 255).astype(np.uint8))
    return frames, skipped, len(emitters)


def main():
    if len(sys.argv) < 5:
        print(__doc__)
        return
    rito, key, out_dir, tex_dir = sys.argv[1:5]
    names = lol_vfx.load_names()
    ents = dict(lol_vfx.parse_text(open(rito, errors='ignore').read().split('\n')))
    entry = lol_vfx.resolve(ents[key], names)
    os.makedirs(out_dir, exist_ok=True)
    frames, skipped, n_em = render_frames([entry], tex_dir)
    print('emitters rendered:', n_em, 'skipped:', skipped)
    for i, fr in enumerate(frames):
        Image.fromarray(fr, 'RGBA').save(os.path.join(out_dir, 'f%03d.png' % i))
    print('frames:', len(frames), 'written to', out_dir)


if __name__ == '__main__':
    main()
