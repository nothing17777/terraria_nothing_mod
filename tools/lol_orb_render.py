#!/usr/bin/env python3
"""Render Ahri's Q orb from the game's own assets: Q_Sphere_01.scb + Q_Swirls_01.scb meshes and billboard textures,
with the colours/blend modes read from Ahri_Base_Q_mis in the skin bin. 16 rotation frames -> Special/Ahri_orb_sheet.png"""
import os, struct, math, numpy as np, moderngl
from PIL import Image
import lol_render_models as M

P = M.EXT + '/Ahri/assets/characters/ahri/skins/base/particles/'
OUT = os.path.abspath(os.path.join(os.path.dirname(os.path.abspath(__file__)), '..', 'nothing_mod', 'Content', 'LoL', 'Special'))
N = 128; FRAMES = 16

def read_scb(path):
    d = open(path, 'rb').read()
    major, minor = struct.unpack_from('<HH', d, 8); o = 12 + 128
    nv, nf, flags = struct.unpack_from('<3I', d, o); o += 12 + 24
    if (major, minor) == (3, 2): vtype = struct.unpack_from('<I', d, o)[0]; o += 4
    else: vtype = 0
    v = np.frombuffer(d, '<f4', nv * 3, o).reshape(nv, 3); o += nv * 12
    if vtype == 1: o += nv * 4
    o += 12
    tris = []; uvs = []
    for _ in range(nf):
        i = struct.unpack_from('<3I', d, o); u = struct.unpack_from('<3f', d, o + 76); w = struct.unpack_from('<3f', d, o + 88); o += 100
        for k in range(3): tris.append(v[i[k]]); uvs.append((u[k], w[k]))
    return np.array(tris, np.float32), np.array(uvs, np.float32)

def load_tex(ctx, name):
    im = M.read_tex(P + name + '.tex')
    a = np.asarray(im).copy()
    if a[..., 3].min() > 250:                       # opaque luma texture -> brightness is coverage
        a[..., 3] = a[..., :3].max(2)
    t = ctx.texture(im.size, 4, a.tobytes()); t.build_mipmaps(); t.filter = (moderngl.LINEAR_MIPMAP_LINEAR, moderngl.LINEAR)
    t.repeat_x = t.repeat_y = True
    return t

VS = '''#version 330
uniform mat4 m; in vec3 p; in vec2 uv; out vec2 v_uv;
void main(){ gl_Position = m * vec4(p,1.0); v_uv = uv; }'''
FS = '''#version 330
uniform sampler2D tex; uniform vec4 col; in vec2 v_uv; out vec4 c;
void main(){ vec4 t = texture(tex, v_uv); c = vec4(t.rgb * col.rgb, t.a * col.a); }'''

def rot_y(a):
    c, s = math.cos(a), math.sin(a)
    return np.array([[c, 0, s, 0], [0, 1, 0, 0], [-s, 0, c, 0], [0, 0, 0, 1]], np.float32)
def rot_x(a):
    c, s = math.cos(a), math.sin(a)
    return np.array([[1, 0, 0, 0], [0, c, -s, 0], [0, s, c, 0], [0, 0, 0, 1]], np.float32)
def scale(k): return np.diag([k, k, k * 0.2, 1]).astype(np.float32)

def main():
    ctx = moderngl.create_standalone_context()
    prog = ctx.program(vertex_shader=VS, fragment_shader=FS)
    fb = ctx.simple_framebuffer((N, N), components=4)
    quad = np.array([[-1, -1, 0], [1, -1, 0], [1, 1, 0], [-1, -1, 0], [1, 1, 0], [-1, 1, 0]], np.float32)
    quv = np.array([[0, 1], [1, 1], [1, 0], [0, 1], [1, 0], [0, 0]], np.float32)
    def vao(pos, uv):
        return ctx.vertex_array(prog, [(ctx.buffer(pos.tobytes()), '3f', 'p'), (ctx.buffer(uv.astype('f4').tobytes()), '2f', 'uv')])
    qv = vao(quad, quv)
    sph_p, sph_uv = read_scb(P + 'ahri_base_q_sphere_01.scb'); sw_p, sw_uv = read_scb(P + 'ahri_base_q_swirls_01.scb')
    sv, wv = vao(sph_p, sph_uv), vao(sw_p, sw_uv)
    rad = 0.78 / float(np.abs(sph_p).max())
    T = {n: load_tex(ctx, n) for n in ('Ahri_Base_Q_OrbBackground', 'Ahri_Base_Q_Smoke_Erode_2', 'Ahri_Base_Q_OrbEdge_3', 'Ahri_Base_BlastRing', 'Ahri_Base_Mote_01')}
    ctx.enable(moderngl.BLEND)
    ALPHA = (moderngl.SRC_ALPHA, moderngl.ONE_MINUS_SRC_ALPHA); ADD = (moderngl.SRC_ALPHA, moderngl.ONE)
    sheet = Image.new('RGBA', (N * FRAMES, N))
    for f in range(FRAMES):
        fb.use(); ctx.clear(0, 0, 0, 0)
        ang = f * 2 * math.pi / FRAMES
        def draw(v, tex, col, blend, m):
            ctx.blend_func = blend; T[tex].use(0); prog['tex'].value = 0
            prog['col'].value = tuple(col); prog['m'].write(m.T.astype('f4').tobytes()); v.render(moderngl.TRIANGLES)
        # emitter recipe from Ahri_Base_Q_mis (colour RGBA, blend 1 = alpha, 4 = additive)
        draw(qv, 'Ahri_Base_Q_OrbBackground', (0.27, 0.376, 0.753, 1.0), ALPHA, scale(0.9))              # Inner_Darkness
        draw(sv, 'Ahri_Base_Q_Smoke_Erode_2', (0.56, 0.93, 1.0, 0.58), ALPHA, scale(rad) @ rot_x(0.35) @ rot_y(ang))   # Dissolve_SPHERE (mesh)
        draw(qv, 'Ahri_Base_Q_OrbBackground', (0.49, 1.0, 0.92, 0.84), ADD, scale(0.9))                   # PreSwap
        draw(wv, 'Ahri_Base_BlastRing', (0.145, 0.93, 1.0, 1.0), ALPHA, scale(rad * 0.9) @ rot_x(0.35) @ rot_y(-ang * 1.5))  # Temp_Mesh3 (swirl mesh)
        draw(qv, 'Ahri_Base_Q_OrbEdge_3', (1, 1, 1, 1), ADD, scale(0.95))                                 # Inner_Light5
        draw(qv, 'Ahri_Base_Mote_01', (0.27, 0.9, 1.0, 0.67), ADD, scale(0.7) @ rot_y(0))                 # sparkles
        img = np.frombuffer(fb.read(components=4), np.uint8).reshape(N, N, 4)[::-1].astype(np.float32) / 255
        rgb = img[..., :3]; a = np.clip(np.maximum(img[..., 3], rgb.max(2)) * 1.2, 0, 1)
        yy, xx = np.mgrid[0:N, 0:N]; r = np.hypot(xx - N / 2 + .5, yy - N / 2 + .5) / (N / 2)
        a *= np.clip((1 - r) / 0.2, 0, 1)
        col = np.where(a[..., None] > 0, rgb / np.maximum(a[..., None], 1e-4), 0)
        sheet.paste(Image.fromarray((np.dstack([np.clip(col, 0, 1), a]) * 255).astype(np.uint8), 'RGBA'), (f * N, 0))
    sheet.save(f'{OUT}/Ahri_orb_sheet.png')
    prev = Image.new('RGBA', (N * 8, N * 2), (60, 90, 55, 255))
    for f in range(FRAMES): prev.alpha_composite(sheet.crop((f * N, 0, f * N + N, N)), ((f % 8) * N, (f // 8) * N))
    prev.save('/private/tmp/claude-501/-Volumes-T7-Shield-terraria-mod/2a686851-4889-4f7e-8a11-f1ab27d345dc/scratchpad/orb3d.png')

if __name__ == '__main__': main()
