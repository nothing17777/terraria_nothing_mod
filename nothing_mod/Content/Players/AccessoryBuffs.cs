using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Players
{
	// Catalyst Band debuff (vanilla 1.4.5 BuffID 398 AcceleratePoisons): doubles the damage-over-time of poison/fire family debuffs.
	// Debuff icon is a vanilla placeholder.
	public class AcceleratedAfflictions : ModBuff
	{
		public override string Texture => "Terraria/Images/Buff_" + BuffID.Poisoned;

		public override void SetStaticDefaults()
		{
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = false;
		}
	}

	public class AcceleratedAfflictionsNPC : GlobalNPC
	{
		// Loss per tick (lifeRegen units) of each boostable DoT, from the reference port's Enemy.ts DOTTally table.
		static int Loss(int buff) => buff switch
		{
			BuffID.Poisoned => 12,
			BuffID.Venom => 60,
			BuffID.OnFire => 8,
			BuffID.OnFire3 => 30,
			BuffID.Frostburn => 16,
			BuffID.Frostburn2 => 50,
			BuffID.CursedInferno => 48,
			BuffID.ShadowFlame => 30,
			_ => 0
		};

		static bool IsFlamable(int buff) => buff == BuffID.OnFire || buff == BuffID.OnFire3 || buff == BuffID.Frostburn
			|| buff == BuffID.Frostburn2 || buff == BuffID.CursedInferno || buff == BuffID.ShadowFlame;

		public override void UpdateLifeRegen(NPC npc, ref int damage)
		{
			if (!npc.HasBuff(ModContent.BuffType<AcceleratedAfflictions>()))
				return;
			int extra = 0;
			bool slimed = npc.HasBuff(BuffID.Slimed); // ref AddFlamable doubles fire DoT on slimed enemies
			for (int i = 0; i < NPC.maxBuffs; i++)
			{
				if (npc.buffTime[i] <= 0)
					continue;
				int loss = Loss(npc.buffType[i]);
				if (loss > 0)
					extra += slimed && IsFlamable(npc.buffType[i]) ? loss * 2 : loss;
			}
			if (extra > 0)
			{
				npc.lifeRegen -= extra;
				if (damage < extra / 6) // UNVERIFIED display value; vanilla batches popups per DoT
					damage = extra / 6;
			}
		}
	}

	// Torch God's Flavor buff (vanilla 1.4.5 BuffID 376 TorchGodPotion, 8 minutes).
	// Reference (Game.ts torchGodsFlavor / TorchGodsFlavor Player.cs:39529): every tick a 1/10 roll, then up to 1000 random samples
	// inside the screen rectangle for a lit biome torch whose style differs from the current biome style, converted to the biome style.
	// The vanilla version flies a helper projectile (1084) to the torch first; here the torch is converted directly (simplification).
	public class TorchBlessing : ModBuff
	{
		public override string Texture => "Terraria/Images/Buff_" + BuffID.Shine; // placeholder icon

		// ref Torch.ts IS_BIOME_TORCH_STYLE
		static readonly bool[] BiomeStyles = MakeStyles();

		static bool[] MakeStyles()
		{
			var a = new bool[32];
			foreach (int s in new[] { 0, 18, 19, 20, 21, 23, 13, 7, 9, 22, 16 })
				a[s] = true;
			return a;
		}

		// ref Torch.ts biomeTorchPlaceStyle (Player.cs:40550-40585), 0 = no biome torch.
		static int BiomeStyle(Player p)
		{
			if (p.ZoneShimmer) return 23;
			if (p.ZoneDungeon) return 13;
			if (p.ZoneLihzhardTemple) return 21;
			if (p.ZoneUnderworldHeight) return 7;
			if (p.ZoneGlowshroom) return 22;
			if (p.ZoneHallow) return 20;
			if (p.ZoneCorrupt) return 18;
			if (p.ZoneCrimson) return 19;
			if (p.ZoneSnow) return 9;
			if (p.ZoneJungle) return 21;
			if ((p.ZoneDesert && p.Center.Y / 16f < Main.worldSurface) || p.ZoneUndergroundDesert) return 16;
			if (p.ZoneDesert && Main.remixWorld) return 16;
			return 0;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			if (player.whoAmI != Main.myPlayer || !Main.rand.NextBool(10))
				return;
			int style = BiomeStyle(player);
			int halfW = Main.screenWidth / 2 / 16;
			int halfH = Main.screenHeight / 2 / 16;
			int cx = (int)(player.Center.X / 16f);
			int cy = (int)(player.Center.Y / 16f);
			for (int n = 0; n < 1000; n++)
			{
				int x = Main.rand.Next(cx - halfW, cx + halfW + 1);
				int y = Main.rand.Next(cy - halfH, cy + halfH + 1);
				if (!WorldGen.InWorld(x, y, 10))
					continue;
				Tile t = Main.tile[x, y];
				if (!t.HasTile || t.TileType != TileID.Torches || t.TileFrameY < 0)
					continue;
				if (player.ZoneDungeon != Main.wallDungeon[t.WallType])
					continue;
				int cur = t.TileFrameY / 22;
				if (cur < 0 || cur >= BiomeStyles.Length || !BiomeStyles[cur] || cur == style)
					continue;
				t.TileFrameY = (short)(style * 22);
				NetMessage.SendTileSquare(-1, x, y);
				break;
			}
		}
	}
}
