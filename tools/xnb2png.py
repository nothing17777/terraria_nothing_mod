#!/usr/bin/env python3
"""Extract textures from the installed Terraria 1.4.5.8 Content folder as PNGs.
Usage: python3 -I xnb2png.py OUT_DIR NAME [NAME...]
NAME is a path relative to .../Terraria.app/Contents/Resources/Content/Images without .xnb,
e.g. Projectile_1115  Armor_Head_250  Armor/Armor_248  Item_5480
Output: OUT_DIR/<basename>.png (straight alpha, un-premultiplied)."""
import os, struct, subprocess, sys, tempfile, zlib
IMG = "/Volumes/T7 Shield/SteamLibrary/steamapps/common/Terraria/Terraria.app/Contents/Resources/Content/Images"
FNA = "/Volumes/T7 Shield/SteamLibrary/steamapps/common/tModLoader/Libraries/FNA/1.0.0/FNA.dll"
HERE = os.path.dirname(os.path.abspath(__file__))
DOTNET = "/usr/local/share/dotnet/dotnet"

def r7(b, o):
    n = sh = 0
    while True:
        x = b[o]; o += 1; n |= (x & 0x7f) << sh; sh += 7
        if not x & 0x80: return n, o

def png(w, h, rgba):
    raw = b"".join(b"\x00" + rgba[y*w*4:(y+1)*w*4] for y in range(h))
    def ch(t, d):
        c = struct.pack(">I", len(d)) + t + d
        return c + struct.pack(">I", zlib.crc32(t + d) & 0xffffffff)
    return (b"\x89PNG\r\n\x1a\n" + ch(b"IHDR", struct.pack(">IIBBBBB", w, h, 8, 6, 0, 0, 0))
            + ch(b"IDAT", zlib.compress(raw, 6)) + ch(b"IEND", b""))

def main():
    out = sys.argv[1]; names = sys.argv[2:]
    os.makedirs(out, exist_ok=True)
    with tempfile.TemporaryDirectory() as tmp:
        lst = os.path.join(tmp, "list.txt")
        open(lst, "w").write("\n".join(f"{IMG}/{n}.xnb" for n in names))
        subprocess.run([DOTNET, os.path.join(HERE, "xnbtool_bin", "xnbtool.dll"), "unxnb", FNA, lst, os.path.join(tmp, "bins")], check=True)
        for n in names:
            base = os.path.basename(n)
            b = open(os.path.join(tmp, "bins", base + ".bin"), "rb").read()
            o = 0; cnt, o = r7(b, o)
            for _ in range(cnt):
                l, o = r7(b, o); o += l + 4
            _, o = r7(b, o); _, o = r7(b, o)
            fmt, w, h, lv = struct.unpack_from("<iiii", b, o); o += 16
            size = struct.unpack_from("<I", b, o)[0]; o += 4
            d = bytearray(b[o:o+size])
            if fmt != 0 or size != w*h*4:
                print("skip", n, "format", fmt); continue
            for i in range(0, len(d), 4):
                a = d[i+3]
                if 0 < a < 255:
                    for k in range(3): d[i+k] = min(255, d[i+k]*255//a)
            open(os.path.join(out, base + ".png"), "wb").write(png(w, h, bytes(d)))
            print("wrote", base, w, h)

main()
