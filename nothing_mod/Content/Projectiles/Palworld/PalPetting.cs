using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

namespace nothing_mod.Content.Projectiles.Palworld
{
	// Right-click petting (wiki: "Right click to pet it!"). Returns true if the cursor is on the pal and it is nearby. Effect and sound are placeholders (UNVERIFIED).
	public static class PalPetting
	{
		public static bool TryPet(Player player, int projType)
		{
			foreach (Projectile p in Main.ActiveProjectiles)
			{
				if (p.owner != player.whoAmI || p.type != projType)
					continue;
				Rectangle box = p.Hitbox;
				box.Inflate(12, 12);
				if (!box.Contains(Main.MouseWorld.ToPoint()) || p.Distance(player.Center) > 100f)
					continue;
				if (Main.GameUpdateCount % 20 == 0)
				{
					SoundEngine.PlaySound(SoundID.Item16, p.Center);
					for (int i = 0; i < 4; i++)
					{
						Dust d = Dust.NewDustDirect(p.position - new Vector2(0f, 12f), p.width, p.height, DustID.PinkTorch, 0f, -2f, 100, default, 1.2f);
						d.noGravity = true;
					}
				}
				return true;
			}
			return false;
		}
	}
}
