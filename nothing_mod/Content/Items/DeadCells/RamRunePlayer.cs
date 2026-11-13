using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.DeadCells
{
	// Ground slam: jump while holding DOWN in the air. On landing, damages nearby enemies.
	public class RamRunePlayer : ModPlayer
	{
		public bool Equipped;
		private bool slamming;

		public override void ResetEffects() => Equipped = false;

		public override void PostUpdateEquips()
		{
			if (!Equipped)
			{
				slamming = false;
				return;
			}
			bool airborne = Player.velocity.Y != 0f && !Player.mount.Active;
			if (!slamming && airborne && Player.controlDown && Player.controlJump && Player.releaseJump)
				slamming = true;
			if (!slamming)
				return;
			if (Player.velocity.Y == 0f || Player.mount.Active)
			{
				slamming = false;
				if (Player.whoAmI == Main.myPlayer)
				{
					int dmg = (int)Player.GetTotalDamage(DamageClass.Melee).ApplyTo(20f); // UNVERIFIED base damage
					foreach (NPC n in Main.ActiveNPCs)
					{
						if (n.CanBeChasedBy() && n.Distance(Player.Bottom) < 160f) // UNVERIFIED radius
							Player.ApplyDamageToNPC(n, dmg, 6f, n.Center.X > Player.Center.X ? 1 : -1, false);
					}
				}
				for (int i = 0; i < 20; i++)
					Dust.NewDust(Player.BottomLeft, Player.width, 4, Terraria.ID.DustID.Smoke, Main.rand.NextFloat(-3f, 3f), -1f);
				return;
			}
			Player.velocity.Y = System.Math.Max(Player.velocity.Y, 16f); // UNVERIFIED slam speed
		}
	}
}
