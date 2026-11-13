using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.DeadCells
{
	// Flint charge-up: holding use charges (no swing); releasing swings. Full charge (0.5 s, wiki) also launches the wave of fire.
	public class FlintPlayer : ModPlayer
	{
		public const int FullChargeTicks = 30;
		public int Charge;
		public bool ReleasedFullCharge;
		private bool releasing;

		public override bool PreItemCheck()
		{
			Item held = Player.HeldItem;
			if (held.type != ModContent.ItemType<Flint>() || Player.whoAmI != Main.myPlayer)
			{
				Charge = 0;
				return true;
			}
			if (Player.itemAnimation > 0)
			{
				releasing = false;
				return true;
			}
			if (Player.controlUseItem && !Player.mouseInterface && !Main.playerInventory)
			{
				Charge++;
				if (Charge == FullChargeTicks)
					SoundEngine.PlaySound(SoundID.Item29, Player.Center); // UNVERIFIED placeholder for "wave of fire charged"
				if (Charge >= 5 && Main.rand.NextBool(3))
				{
					Dust d = Dust.NewDustDirect(Player.Center + new Vector2(Player.direction * 20f, -20f), 8, 8, DustID.Torch, 0f, -2f, 100, default, Charge >= FullChargeTicks ? 1.6f : 1f);
					d.noGravity = true;
				}
				Player.controlUseItem = false; // block the use while charging
			}
			else if (Charge > 0)
			{
				ReleasedFullCharge = Charge >= FullChargeTicks;
				Charge = 0;
				Player.controlUseItem = true;
				Player.releaseUseItem = true;
				releasing = true;
			}
			return true;
		}

		public override void ResetEffects()
		{
		}
	}
}
