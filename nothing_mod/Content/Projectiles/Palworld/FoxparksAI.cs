using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using nothing_mod.Content.Items.Palworld;

namespace nothing_mod.Content.Projectiles.Palworld
{
	// Shared behaviour of the Foxparks / Trusty Foxparks minions (wiki: AI_026).
	// Simplified: hovers instead of walking. projectile.ai[0] = shots fired, localAI[0] = shot cooldown.
	public static class FoxparksAI
	{
		public static bool IsHeld(Player owner)
		{
			int fox = ModContentItem<Foxparks>();
			int trusty = ModContentItem<TrustyFoxparks>();
			int t = owner.HeldItem.type;
			return (t == fox || t == trusty) && owner.controlUseItem && owner.itemAnimation > 0;
		}

		private static int ModContentItem<T>() where T : Terraria.ModLoader.ModItem => Terraria.ModLoader.ModContent.ItemType<T>();

		public static void Run(Projectile p, Player owner, bool trusty)
		{
			// Held flamethrower: Foxparks teleports to the player and is held while the button is down.
			if (IsHeld(owner))
			{
				p.Center = owner.Center + new Vector2(owner.direction * 18f, -6f);
				p.velocity = Vector2.Zero;
				p.spriteDirection = owner.direction;
				return;
			}
			if (p.localAI[0] > 0f)
				p.localAI[0]--;

			NPC target = null;
			float bestDist = float.MaxValue;
			float taxi = (trusty ? 72.5f : 50f) * 16f;
			float horiz = (trusty ? 39.375f : 28.125f) * 16f;
			foreach (NPC n in Main.ActiveNPCs)
			{
				if (!n.CanBeChasedBy(p))
					continue;
				float dx = System.Math.Abs(n.Center.X - p.Center.X);
				float dy = System.Math.Abs(n.Center.Y - p.Center.Y);
				if (dx > horiz || dx + dy > taxi)
					continue;
				if (dx + dy < bestDist)
				{
					bestDist = dx + dy;
					target = n;
				}
			}
			if (owner.HasMinionAttackTargetNPC)
			{
				NPC forced = Main.npc[owner.MinionAttackTargetNPC];
				if (forced.CanBeChasedBy(p))
					target = forced;
			}

			if (target != null)
			{
				// Keep a little distance and stay level with the target.
				Vector2 hover = target.Center + new Vector2(System.Math.Sign(p.Center.X - target.Center.X) * 160f, -20f);
				Projectiles.NewWeapons.SimpleMinionAI.MoveToward(p, hover, 7f, 20f);
				p.spriteDirection = target.Center.X >= p.Center.X ? 1 : -1;
				if (p.localAI[0] <= 0f && p.owner == Main.myPlayer && Collision.CanHitLine(p.Center, 1, 1, target.Center, 1, 1))
				{
					p.localAI[0] = trusty ? 30f : 42f; // wiki: every 0.5 s (Trusty) / 0.7 s
					p.ai[0]++;
					Vector2 aimAt = target.Center + target.velocity * 20f; // predicts 0.33 s ahead (wiki)
					Vector2 v = p.Center.DirectionTo(aimAt) * 10f;
					int mode = trusty && p.ai[0] % 3f == 0f ? 1 : 0; // every 3rd Trusty fireball splits
					Projectile.NewProjectile(p.GetSource_FromThis(), p.Center, v, Terraria.ModLoader.ModContent.ProjectileType<FoxparksFireball>(), p.damage, p.knockBack, p.owner, mode, trusty ? 1f : 0f);
				}
			}
			else
			{
				Projectiles.NewWeapons.SimpleMinionAI.MoveToward(p, Projectiles.NewWeapons.SimpleMinionAI.IdlePosition(p, owner), 10f, 20f);
				if (System.Math.Abs(p.velocity.X) > 0.5f)
					p.spriteDirection = p.velocity.X >= 0 ? 1 : -1;
			}
			p.rotation = 0f;
			p.frameCounter++;
			if (p.frameCounter >= 6)
			{
				p.frameCounter = 0;
				p.frame = (p.frame + 1) % 8;
			}
		}

		// The sheet is 6 columns x ~27 rows of 64x46 cells; column 0 rows 0-7 is used as the cycle (layout UNVERIFIED).
		public static bool Draw(Projectile p)
		{
			Texture2D tex = TextureAssets.Projectile[p.type].Value;
			Rectangle src = new Rectangle(0, p.frame * 46, 64, 46);
			SpriteEffects fx = p.spriteDirection < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
			Main.EntitySpriteDraw(tex, p.Center - Main.screenPosition + new Vector2(0f, -6f), src, Lighting.GetColor((int)(p.Center.X / 16f), (int)(p.Center.Y / 16f)), p.rotation, src.Size() / 2f, 1f, fx, 0);
			return false;
		}
	}
}
