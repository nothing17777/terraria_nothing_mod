using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace nothing_mod.Content.LoL
{
	// Archetype ids (SpellDef.Arch), inferred from the spell text by tools/lol_generate.py:
	// 0 skillshot, 1 dash, 2 cone sweep, 3 burst around self, 4 burst at cursor, 5 self heal/buff.
	// Flag bits (SpellDef.Flags): 1 slow, 2 stun/root/knockup, 4 fear/charm/silence.
	public class SpellDef
	{
		public string Key, Name, Desc;
		public float Cooldown;
		public int Mana, Range, Arch, Flags, Heal, HasIcon;

		public SpellDef(string key, string name, string desc, float cooldown, int mana, int range, int arch, int flags, int heal, int hasIcon) {
			Key = key; Name = name; Desc = desc; Cooldown = cooldown; Mana = mana; Range = range; Arch = arch; Flags = flags; Heal = heal; HasIcon = hasIcon;
		}
	}

	public class ChampionDef
	{
		public string Name, Title, Bio;
		public int Magic, Ranged;
		public SpellDef[] Spells; // P, Q, W, E, R

		public ChampionDef(string name, string title, string bio, int magic, int ranged, SpellDef[] spells) {
			Name = name; Title = title; Bio = bio; Magic = magic; Ranged = ranged; Spells = spells;
		}

		// Stable hue (0-35) per champion so each one gets its own effect colour.
		public int Hue => (int)((uint)Name.GetHashCode() % 36);
		public Color EffectColor => Main_HsvToColor(Hue * 10f / 360f);

		static Color Main_HsvToColor(float h) => Terraria.Main.hslToRgb(h, 0.9f, 0.6f);
	}

	public class ItemDef
	{
		public string Name, Desc;
		public int Price, PriceTotal;
		public int[] From;
		public Dictionary<string, float> Stats;

		public ItemDef(string name, string desc, int price, int priceTotal, int[] from, Dictionary<string, float> stats) {
			Name = name; Desc = desc; Price = price; PriceTotal = priceTotal; From = from; Stats = stats;
		}
	}

	public static class LolText
	{
		// Terraria tooltips do not wrap, so split long text into ~52 char lines.
		public static List<string> Wrap(string text, int width = 52) {
			var lines = new List<string>();
			var sb = new StringBuilder();
			foreach (string word in text.Split(' ')) {
				if (sb.Length + word.Length + 1 > width && sb.Length > 0) { lines.Add(sb.ToString()); sb.Clear(); }
				if (sb.Length > 0) sb.Append(' ');
				sb.Append(word);
			}
			if (sb.Length > 0) lines.Add(sb.ToString());
			return lines;
		}
	}
}
