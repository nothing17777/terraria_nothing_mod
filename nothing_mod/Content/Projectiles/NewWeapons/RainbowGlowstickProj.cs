using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Projectiles.NewWeapons
{
	// Bouncy glowstick that cycles through rainbow colors. Sprite is a placeholder (Bouncy Glowstick).
	public class RainbowGlowstickProj : ModProjectile
	{
		public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.BouncyGlowstick;

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.BouncyGlowstick);
			AIType = ProjectileID.BouncyGlowstick;
			Projectile.ignoreWater = true; // "works when wet"
		}

		public override void PostAI()
		{
			Color c = Main.hslToRgb((Main.GlobalTimeWrappedHourly * 0.8f) % 1f, 1f, 0.6f);
			Lighting.AddLight(Projectile.Center, c.ToVector3() * 0.9f);
		}
		// TODO: slower speed loss per bounce (1%) than the Bouncy Glowstick; full-bright trail.
	}
}
