#!/usr/bin/env python3
"""Generate nothing_mod/Content/LoL from the extracted League client data.
Run: tools/.venv/bin/python tools/lol_generate.py  (needs xxhash, pillow)
Sources: extracted_assets/rcp-be-lol-game-data (champion + item JSON, icons).
Spell damage numbers are NOT in this JSON (all zero) -> abilities use tiered placeholder damage."""
import json, glob, os, re, sys, shutil
import xxhash
from PIL import Image

SRC = '/Volumes/T7 Shield/league_of_legends_cilent/extracted_assets/rcp-be-lol-game-data/'
OUT = os.path.join(os.path.dirname(os.path.abspath(__file__)), '..', 'nothing_mod', 'Content', 'LoL')
OUT = os.path.abspath(OUT)

files = {}
for a in ('default-assets', 'default-assets2'):
    for sub in os.listdir(SRC + a):
        for f in os.listdir(f'{SRC}{a}/{sub}'):
            files[f.split('.')[0]] = f'{SRC}{a}/{sub}/{f}'

def asset(path):
    """/lol-game-data/assets/X -> local file (xxh64 of lowercase plugin path)."""
    if not path: return None
    p = path.replace('/lol-game-data/assets/', '').lower()
    h = xxhash.xxh64_hexdigest(('plugins/rcp-be-lol-game-data/global/default/' + p).encode())
    return files.get(h)

def icon(path, dest, size):
    f = asset(path)
    if not f: return False
    os.makedirs(os.path.dirname(dest), exist_ok=True)
    im = Image.open(f).convert('RGBA').resize((size, size), Image.LANCZOS)
    im.save(dest)
    return True

def strip(s):
    s = re.sub(r'<br\s*/?>', ' ', s or '')
    s = re.sub(r'<[^>]+>', '', s)
    s = re.sub(r'@[^@]*@', '?', s)
    return re.sub(r'\s+', ' ', s).strip()

def cs(s):
    return '"' + s.replace('\\', '\\\\').replace('"', '\\"') + '"'

# ---------------- champions ----------------
champs = {}
for f in glob.glob(SRC + 'default-assets2/data/*.json'):
    try: d = json.load(open(f))
    except Exception: continue
    if isinstance(d, dict) and 'spells' in d and 'passive' in d and len(d['spells']) == 4:
        a = d['alias']
        if a.startswith('Jade_') or not re.fullmatch(r'[A-Za-z]+', a) or not d.get('isVisibleInClient', True): continue
        champs[a] = d

DASH = re.compile(r'\b(dash|leap|jump|blink|teleport|charge|lunge|vault|rush|flash)', re.I)
SELF = re.compile(r'\b(nearby|surrounding|around (him|her|them|itself)|all (nearby )?enemies)', re.I)
AREA = re.compile(r'\b(area|explod|slam|zone|erupt|rain|barrage|target location|ground)', re.I)
CONE = re.compile(r'\b(cone|sweep|slash|arc|swing|strike)', re.I)
HEAL = re.compile(r'\b(heal|shield|restor|regenerat)', re.I)
FLAGS = [('slow', r'\bslow'), ('stun', r'\b(stun|root|snare|airborne|knock|suppress|immobil)'),
         ('fear', r'\b(fear|charm|taunt|flee|sleep|polymorph|silence)')]

def classify(sp, key):
    t = strip(sp.get('dynamicDescription') or '') + ' ' + strip(sp.get('description') or '')
    if DASH.search(t): arch = 1
    elif SELF.search(t): arch = 3
    elif AREA.search(t): arch = 4
    elif CONE.search(t): arch = 2
    else: arch = 0
    pure = bool(HEAL.search(t)) and not re.search(r'damage', t, re.I)
    heal = bool(HEAL.search(t))
    if pure: arch = 5  # pure self buff / heal
    fl = 0
    for i, (_, rx) in enumerate(FLAGS):
        if re.search(rx, t, re.I): fl |= 1 << i
    return arch, fl, 1 if heal else 0

champ_cs = []
spell_rows = []
for alias, d in sorted(champs.items()):
    ok = icon(d['squarePortraitPath'], f'{OUT}/Champions/{alias}Champion.png', 40)
    if not os.path.exists(f'{OUT}/Models/{alias}.png'): icon(d['squarePortraitPath'], f'{OUT}/Models/{alias}.png', 80)  # fallback; real renders come from lol_render_models.py
    ti = d['tacticalInfo']
    dmg = {'kMagic': 1, 'kPhysical': 0, 'kMixed': 1}.get(ti.get('damageType'), 0)
    ranged = 1 if ti.get('attackType') == 'ranged' else 0
    sp_defs = []
    allsp = [('P', d['passive'])] + [(s['spellKey'].upper(), s) for s in d['spells']]
    for key, sp in allsp:
        iconok = icon(sp.get('abilityIconPath'), f'{OUT}/Abilities/{alias}_{key}.png', 32)
        if key == 'P':
            sp_defs.append(f'new SpellDef({cs(key)}, {cs(sp["name"])}, {cs(strip(sp["description"]))}, 0, 0, 0, 0, 0, 0, 0)')
            continue
        arch, fl, heal = classify(sp, key.lower())
        cd = (sp.get('cooldownCoefficients') or [8])[0]
        cd = max(1.0, min(60.0, cd * 0.5)) if cd else 5.0
        mana = (sp.get('costCoefficients') or [0])[0]
        mana = int(min(100, mana) / 2)
        rng = (sp.get('range') or [600])[0]
        rng = rng if 100 < rng < 3000 else 700
        desc = strip(sp.get('dynamicDescription') or sp.get('description'))
        sp_defs.append(f'new SpellDef({cs(key)}, {cs(sp["name"])}, {cs(desc)}, {cd:.1f}f, {mana}, {int(rng)}, {arch}, {fl}, {int(heal)}, {1 if iconok else 0})')
    spells = ', '.join(sp_defs)
    champ_cs.append(f'\t\t\t["{alias}"] = new ChampionDef({cs(d["name"])}, {cs(d["title"])}, {cs(strip(d["shortBio"]))}, {dmg}, {ranged}, new SpellDef[] {{ {spells} }}),')
    cls = f'''using nothing_mod.Content.LoL;
using Terraria.ModLoader;

namespace nothing_mod.Content.LoL.Champions
{{
	public class {alias}Champion : LolChampionItem
	{{
		protected override string Alias => "{alias}";
	}}
}}
'''
    os.makedirs(f'{OUT}/Champions', exist_ok=True)
    open(f'{OUT}/Champions/{alias}Champion.cs', 'w').write(cls)

# ---------------- items ----------------
items = []
for f in glob.glob(SRC + 'default-assets2/data/*.json'):
    try: d = json.load(open(f))
    except Exception: continue
    if isinstance(d, list) and d and isinstance(d[0], dict) and 'priceTotal' in d[0] and 'iconPath' in d[0]:
        items = d; break

STAT = {'Attack Damage': 'ad', 'Ability Power': 'ap', 'Armor': 'armor', 'Magic Resist': 'mr', 'Health': 'hp', 'Mana': 'mana',
        'Attack Speed': 'as', 'Critical Strike Chance': 'crit', 'Ability Haste': 'haste', 'Move Speed': 'ms',
        'Base Health Regen': 'hpregen', 'Base Mana Regen': 'mpregen', 'Life Steal': 'lifesteal', 'Omnivamp': 'lifesteal'}
seen = set(); item_cs = []; made = 0
for it in sorted(items, key=lambda i: i['id']):
    name = it['name']
    if not it.get('inStore') or not name or it['priceTotal'] <= 0 or it['id'] >= 10000: continue
    if (name, it['priceTotal']) in seen: continue
    seen.add((name, it['priceTotal']))
    cid = f'Lol{it["id"]}'
    if not icon(it['iconPath'], f'{OUT}/Items/{cid}.png', 36): continue
    stats = {}
    for m in re.finditer(r'<attention>\s*([\d.]+)(%?)</attention>\s*([A-Za-z ]+?)\s*(?:<|$)', it['description']):
        k = STAT.get(m.group(3).strip())
        if k: stats[k] = stats.get(k, 0) + float(m.group(1))
    st = ', '.join(f'["{k}"] = {v}f' for k, v in stats.items())
    frm = ', '.join(str(x) for x in it['from'] if x)
    item_cs.append(f'\t\t\t[{it["id"]}] = new ItemDef({cs(name)}, {cs(strip(it["description"]))}, {it["price"]}, {it["priceTotal"]}, new int[] {{ {frm} }}, new Dictionary<string, float> {{ {st} }}),')
    open(f'{OUT}/Items/{cid}.cs', 'w').write(f'''namespace nothing_mod.Content.LoL.Items
{{
	public class {cid} : LolStatItem
	{{
		protected override int LolId => {it["id"]};
	}}
}}
''')
    made += 1

open(f'{OUT}/LolData.cs', 'w').write('''// <auto-generated> by tools/lol_generate.py from the League client data. Do not edit. </auto-generated>
using System.Collections.Generic;

namespace nothing_mod.Content.LoL
{
	public static class LolData
	{
		public static readonly Dictionary<string, ChampionDef> Champions = new()
		{
''' + '\n'.join(champ_cs) + '''
		};

		public static readonly Dictionary<int, ItemDef> Items = new()
		{
''' + '\n'.join(item_cs) + '''
		};
	}
}
''')
print('champions', len(champs), 'items', made)
