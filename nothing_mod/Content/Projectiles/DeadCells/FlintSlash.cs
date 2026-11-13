using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace nothing_mod.Content.Projectiles.DeadCells
{
	// Visual aura of Flint's swing (yellow, brighter than the blade, bigger when charged). Deals no damage. ai[0] = aim rotation, ai[1] = scale.
	public class FlintSlash : ModProjectile
	{
		private const int Life = 12; // UNVERIFIED duration

		public override void SetStaticDefaults()
		{
			Main.projFrames[Type] = 4;
		}

		public override void SetDefaults()
		{
			Projectile.width = 8;
			Projectile.height = 8;
			Projectile.friendly = false;
			Projectile.penetrate = -1;
			Projectile.timeLeft = Life;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
		}

		public override void AI()
		{
			Player p = Main.player[Projectile.owner];
			Projectile.Center = p.Center;
			Projectile.rotation = Projectile.ai[0];
			Projectile.frame = (Life - Projectile.timeLeft) * Main.projFrames[Type] / Life;
			if (Projectile.frame >= Main.projFrames[Type])
				Projectile.frame = Main.projFrames[Type] - 1;
			Lighting.AddLight(Projectile.Center, 1f, 0.8f, 0.3f);
		}

		public override bool PreDraw(ref Color lightColor)
		{
			Texture2D tex = Terraria.GameContent.TextureAssets.Projectile[Type].Value;
			int fh = tex.Height / Main.projFrames[Type];
			Rectangle src = new Rectangle(0, Projectile.frame * fh, tex.Width, fh);
			Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition, src, Color.White * 0.9f, Projectile.rotation, src.Size() / 2f, Projectile.ai[1], SpriteEffects.None, 0);
			return false;
		}
	}
}
