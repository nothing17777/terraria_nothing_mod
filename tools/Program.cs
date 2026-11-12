using System.Reflection;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Text.Json;

static class P
{
    static int Main(string[] a)
    {
        if (a[0] == "ids") return Ids(a[1], a[2]);
        if (a[0] == "types") { foreach (var t in Assembly.LoadFrom(a[1]).GetTypes()) if (t.Name.Contains("Lzx")) { Console.WriteLine(t.FullName); foreach (var c in t.GetConstructors(BindingFlags.Public|BindingFlags.NonPublic|BindingFlags.Instance)) Console.WriteLine("  ctor " + string.Join(",", c.GetParameters().Select(x=>x.ParameterType.Name))); } return 0; }
        if (a[0] == "methods")
        {
            using var fs2 = File.OpenRead(a[1]); using var pe2 = new PEReader(fs2); var m2 = pe2.GetMetadataReader();
            foreach (var th in m2.TypeDefinitions) { var t = m2.GetTypeDefinition(th); if (m2.GetString(t.Name) != a[2]) continue;
                foreach (var mh in t.GetMethods()) { var m = m2.GetMethodDefinition(mh); var n = m2.GetString(m.Name); if (n.Contains(a[3])) Console.WriteLine(n + " rva=" + m.RelativeVirtualAddress); } }
            return 0;
        }
        if (a[0] == "res") return Res(a[1], a[2]);
        if (a[0] == "unxnb") return Unxnb(a[1], a[2], a[3]);
        return 1;
    }

    // Dump constant fields of Terraria.ID.{ItemID,ProjectileID,ArmorIDs.*} from a .NET exe.
    static int Ids(string exe, string outJson)
    {
        using var fs = File.OpenRead(exe);
        using var pe = new PEReader(fs);
        var md = pe.GetMetadataReader();
        var result = new Dictionary<string, Dictionary<string, long>>();
        foreach (var th in md.TypeDefinitions)
        {
            var t = md.GetTypeDefinition(th);
            string ns = md.GetString(t.Namespace), name = md.GetString(t.Name);
            string full;
            if (ns == "Terraria.ID" && (name == "ItemID" || name == "ProjectileID" || name == "BuffID")) full = name;
            else if (t.GetDeclaringType().IsNil == false)
            {
                var outer = md.GetTypeDefinition(t.GetDeclaringType());
                if (md.GetString(outer.Name) == "ArmorIDs" && md.GetString(outer.Namespace) == "Terraria.ID") full = "ArmorIDs." + name; else continue;
            }
            else continue;
            var d = new Dictionary<string, long>();
            foreach (var fh in t.GetFields())
            {
                var f = md.GetFieldDefinition(fh);
                var ch = f.GetDefaultValue();
                if (ch.IsNil) continue;
                var c = md.GetConstant(ch);
                var br = md.GetBlobReader(c.Value);
                long v;
                switch (c.TypeCode)
                {
                    case ConstantTypeCode.Int16: v = br.ReadInt16(); break;
                    case ConstantTypeCode.Int32: v = br.ReadInt32(); break;
                    case ConstantTypeCode.UInt16: v = br.ReadUInt16(); break;
                    case ConstantTypeCode.Byte: v = br.ReadByte(); break;
                    case ConstantTypeCode.SByte: v = br.ReadSByte(); break;
                    default: continue;
                }
                d[md.GetString(f.Name)] = v;
            }
            result[full] = d;
        }
        File.WriteAllText(outJson, JsonSerializer.Serialize(result));
        foreach (var kv in result) Console.WriteLine(kv.Key + " " + kv.Value.Count);
        return 0;
    }

    static int Res(string exe, string outDir)
    {
        Directory.CreateDirectory(outDir);
        using var fs = File.OpenRead(exe);
        using var pe = new PEReader(fs);
        var md = pe.GetMetadataReader();
        var cor = pe.PEHeaders.CorHeader.ResourcesDirectory;
        pe.PEHeaders.TryGetDirectoryOffset(cor, out int resOffset);
        foreach (var rh in md.ManifestResources)
        {
            var r = md.GetManifestResource(rh);
            string name = md.GetString(r.Name);
            if (!name.Contains("Localization") || !name.Contains("en-US")) continue;
            var blob = pe.GetEntireImage().GetReader(resOffset + (int)r.Offset, 4);
            int len = blob.ReadInt32();
            var data = pe.GetEntireImage().GetReader(resOffset + (int)r.Offset + 4, len).ReadBytes(len);
            File.WriteAllBytes(Path.Combine(outDir, name), data);
            Console.WriteLine(name + " " + len);
        }
        return 0;
    }

    // Decompress XNB files listed in a text file (one path per line) into outDir/<name>.bin
    static int Unxnb(string fna, string listFile, string outDir)
    {
        var asm = Assembly.LoadFrom(fna);
        var lzxType = asm.GetType("Microsoft.Xna.Framework.Content.LzxDecoder");
        if (lzxType == null) { Console.WriteLine("LzxDecoder not found"); return 2; }
        var dec = lzxType.GetMethod("Decompress");
        Directory.CreateDirectory(outDir);
        int ok = 0, bad = 0;
        foreach (var path in File.ReadAllLines(listFile))
        {
            if (string.IsNullOrWhiteSpace(path)) continue;
            try
            {
                var b = File.ReadAllBytes(path);
                bool compressed = (b[5] & 0x80) != 0;
                byte[] payload;
                if (compressed)
                {
                    int fileSize = BitConverter.ToInt32(b, 6);
                    int decSize = BitConverter.ToInt32(b, 10);
                    var lzx = Activator.CreateInstance(lzxType, 16);
                    var inS = new MemoryStream(b);
                    var outS = new MemoryStream();
                    long startPos = 14, pos = 14;
                    long todo = fileSize - 14;
                    while (pos - startPos < todo)
                    {
                        inS.Seek(pos, SeekOrigin.Begin);
                        int hi = inS.ReadByte(), lo = inS.ReadByte();
                        int blockSize = (hi << 8) | lo, frameSize = 0x8000;
                        if (hi == 0xFF)
                        {
                            hi = lo; lo = inS.ReadByte();
                            frameSize = (hi << 8) | lo;
                            hi = inS.ReadByte(); lo = inS.ReadByte();
                            blockSize = (hi << 8) | lo;
                            pos += 5;
                        }
                        else pos += 2;
                        if (blockSize == 0 || frameSize == 0) break;
                        dec.Invoke(lzx, new object[] { inS, blockSize, outS, frameSize });
                        pos += blockSize;
                    }
                    payload = outS.ToArray();
                    if (payload.Length < decSize) throw new Exception("size " + payload.Length + " != " + decSize);
                }
                else payload = b.Skip(10).ToArray();
                File.WriteAllBytes(Path.Combine(outDir, Path.GetFileNameWithoutExtension(path) + ".bin"), payload);
                ok++;
            }
            catch (Exception e) { bad++; Console.WriteLine("FAIL " + path + " " + e.Message); }
        }
        Console.WriteLine($"ok {ok} bad {bad}");
        return 0;
    }
}
