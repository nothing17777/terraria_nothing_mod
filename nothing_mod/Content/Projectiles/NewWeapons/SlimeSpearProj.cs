using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Projectiles.NewWeapons
{
	// Slime Spear thrust. Uses the vanilla Spear AI and sprite (placeholder).
	public class SlimeSpearProj : ModProjectile
	{
		public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.Spear;

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.Spear);
			AIType = ProjectileID.Spear;
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.AddBuff(BuffID.Slimed, 180); // 100% chance, 3 seconds (wiki)
		}
	}
}
