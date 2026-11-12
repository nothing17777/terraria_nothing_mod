"""Extract real Q/W/E/R spell numbers for every champion from the client's champion .bin files and write
nothing_mod/Content/LoL/LolSpellData.cs.

Pipeline: bin/ritobin-tools convert <alias>.bin -t json -> resolve field names via hashes/hashes.binfields.txt ->
character record 'spells' (4 object keys) -> object.mSpell fields.
Usage: .venv/bin/python lol_spell_extract.py            (converts everything, then writes the C#)
"""
import json, os, re, subprocess, sys
sys.path.insert(0, os.path.dirname(os.path.abspath(__file__)))
import lol_vfx

HERE = os.path.dirname(os.path.abspath(__file__))
EXT = '/Volumes/T7 Shield/league_of_legends_cilent/extracted/champions'
JSON_DIR = os.environ.get('LOL_JSON_DIR', '/tmp/lol_champ_json')
OUT = os.path.join(HERE, '..', 'nothing_mod/Content/LoL/LolSpellData.cs')
NAMES = lol_vfx.load_names()

def nm(k):
    return NAMES.get(int(k, 16), k) if isinstance(k, str) and k.startswith('0x') else k

def res(x):
    if isinstance(x, dict): return {nm(k): res(v) for k, v in x.items()}
    if isinstance(x, list): return [res(i) for i in x]
    return x

def ranks(arr, n=5):
    """values for ranks 1..n (index 0 of the 7-entry arrays is the unused base slot)."""
    if not isinstance(arr, list) or not arr: return [0.0] * n
    a = [float(v) for v in arr if isinstance(v, (int, float))]
    if len(a) >= n + 1: a = a[1:n + 1]
    while len(a) < n: a.append(a[-1] if a else 0.0)
    return a[:n]

def first(arr, default=0.0, i=1):
    r = ranks(arr, 5)
    return r[min(i, 4)] if r else default

CC = re.compile(r'(stun|root|snare|slow|knock|fear|charm|silence|sleep|taunt|suppress|blind|airborne|polymorph|ground)', re.I)
DUR = re.compile(r'(dur|time)', re.I)

def spell_stats(sp, script):
    dv = {d['name']: ranks(d.get('values')) for d in sp.get('DataValues', []) if isinstance(d, dict) and 'name' in d}
    dmg_key = next((k for k in dv if re.search(r'damage', k, re.I) and not re.search(r'ratio|percent|bonus|per|max|mod|reduc|amp', k, re.I)), None)
    if dmg_key is None:
        dmg_key = next((k for k in dv if re.search(r'damage', k, re.I) and not re.search(r'ratio', k, re.I)), None)
    ratio_key = next((k for k in dv if re.search(r'(ratio|coef)', k, re.I) and re.search(r'(^|[a-z])(ap|ad)', k, re.I) and not re.search(r'dur|time|shield|heal', k, re.I)), None) \
        or next((k for k in dv if re.search(r'(ratio|coef)', k, re.I) and not re.search(r'dur|time|shield|heal', k, re.I)), None)
    cc = 0.0
    for k, v in dv.items():
        if CC.search(k) and DUR.search(k) and not re.search(r'cooldown|percent|amount|strength', k, re.I):
            cc = max(cc, v[2])
    ms = sp.get('mMissileSpec') or {}
    mc = ms.get('movementComponent') or {}
    speed = mc.get('mSpeed') or sp.get('missileSpeed') or 0.0
    rng = first(sp.get('castRangeDisplayOverride'), 0.0, 3) or first(sp.get('castRange'), 0.0, 3)
    if rng >= 5000: rng = 1000.0   # 25000 = 'global' placeholder with no display range: UNVERIFIED default
    width = sp.get('mLineWidth') or 0.0
    return {
        'n': script,
        'cd': ranks(sp.get('cooldownTime')),
        'mana': ranks(sp.get('mana')),
        'range': rng,
        'radius': first(sp.get('castRadius'), 0.0, 3),
        'cone': float(sp.get('castConeDistance') or 0.0),
        'coneAng': float(sp.get('castConeAngle') or 0.0),
        'speed': float(speed), 'width': float(width),
        'type': int(sp.get('mCastType') or 0),
        'cast': float(sp.get('mCastTime') if isinstance(sp.get('mCastTime'), (int, float)) else sp.get('spellCastTime') or 0.0),
        'dmg': dv.get(dmg_key, [0.0] * 5), 'dmgName': dmg_key or '',
        'ratio': (dv.get(ratio_key, [0.0] * 5)[2] if ratio_key else 0.0), 'ratioName': ratio_key or '',
        'cc': cc,
        'channel': int(bool(sp.get('mCantCancelWhileChanneling') or sp.get('mCanMoveWhileChanneling'))),
        'toggle': int(bool(sp.get('bIsToggleSpell'))),
    }

def champion_spells(alias):
    path = os.path.join(JSON_DIR, alias + '.json')
    if not os.path.exists(path):
        src = os.path.join(EXT, alias, 'data/characters', alias.lower(), alias.lower() + '.bin')
        if not os.path.exists(src): return None
        os.makedirs(JSON_DIR, exist_ok=True)
        subprocess.run([os.path.join(HERE, 'bin/ritobin-tools'), 'convert', src, '-t', 'json', '-o', path], capture_output=True)
    objs = json.load(open(path))['objects']
    rec = None
    for v in objs.values():
        if isinstance(v, dict) and any(nm(k) == 'spellNames' for k in v): rec = res(v); break
    if not rec: return None
    out = []
    for key in rec.get('spells', [])[:4]:
        o = objs.get(key)
        if not o: out.append(None); continue
        o = res(o)
        sp = o.get('mSpell')
        out.append(spell_stats(sp, o.get('mScriptName', '')) if sp else None)
    return out

def cs_arr(a): return '{ ' + ', '.join(f'{v:g}f' for v in a) + ' }'

def main():
    data = {}
    for alias in sorted(os.listdir(EXT)):
        if alias == 'TFTChampion': continue
        try: s = champion_spells(alias)
        except Exception as e: print('FAIL', alias, type(e).__name__, e); continue
        if not s or len(s) < 4 or any(x is None for x in s): print('incomplete', alias, None if not s else [x and x['n'] for x in s]); 
        if s: data[alias] = s
    os.makedirs(os.path.join(HERE, 'spell_data'), exist_ok=True)
    json.dump(data, open(os.path.join(HERE, 'spell_data/spells.json'), 'w'))
    rows = []
    for alias, s in data.items():
        parts = []
        for x in s:
            if x is None: parts.append('null'); continue
            parts.append(f'new RealSpell("{x["n"]}", new float[] {cs_arr(x["cd"])}, new float[] {cs_arr(x["mana"])}, {x["range"]:g}f, {x["radius"]:g}f, {x["cone"]:g}f, {x["coneAng"]:g}f, '
                         f'{x["speed"]:g}f, {x["width"]:g}f, {x["type"]}, {x["cast"]:g}f, new float[] {cs_arr(x["dmg"])}, {x["ratio"]:g}f, "{x["ratioName"]}", {x["cc"]:g}f, {x["channel"]}, {x["toggle"]})')
        rows.append(f'\t\t\t["{alias}"] = new RealSpell[] {{\n\t\t\t\t' + ',\n\t\t\t\t'.join(parts) + '\n\t\t\t},')
    open(OUT, 'w').write('''// <auto-generated> by tools/lol_spell_extract.py from the champion .bin files. Do not edit. </auto-generated>
using System.Collections.Generic;

namespace nothing_mod.Content.LoL
{
	// Real League numbers for one spell. Arrays are ranks 1-5. Units are League units (1 unit ~ 1/100 of a champion width).
	public class RealSpell
	{
		public string Script;
		public float[] Cd, Mana, Dmg;
		public float Range, Radius, Cone, ConeAngle, Speed, Width, CastTime, Ratio, CcDur;
		public int CastType, Channel, Toggle;
		public string RatioName;

		public RealSpell(string script, float[] cd, float[] mana, float range, float radius, float cone, float coneAngle, float speed, float width,
			int castType, float castTime, float[] dmg, float ratio, string ratioName, float ccDur, int channel, int toggle) {
			Script = script; Cd = cd; Mana = mana; Range = range; Radius = radius; Cone = cone; ConeAngle = coneAngle; Speed = speed; Width = width;
			CastType = castType; CastTime = castTime; Dmg = dmg; Ratio = ratio; RatioName = ratioName; CcDur = ccDur; Channel = channel; Toggle = toggle;
		}
	}

	public static class LolSpellData
	{
		public static readonly Dictionary<string, RealSpell[]> Spells = new()
		{
''' + '\n'.join(rows) + '''
		};
	}
}
''')
    print('champions', len(data))

main()
