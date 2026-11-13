using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Projectiles.DeadCells;

namespace nothing_mod.Content.Buffs.DeadCells
{
	// Pet buff. Icon is a placeholder (Baby Slime / light pet buff).
	public class SwarmBiterBuff : ModBuff
	{
		public override string Texture => "Terraria/Images/Buff_" + BuffID.PetBunny;

		public override void SetStaticDefaults()
		{
			Main.buffNoTimeDisplay[Type] = true;
			Main.vanityPet[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.buffTime[buffIndex] = 18000;
			int projType = ModContent.ProjectileType<SwarmBiterPet>();
			if (player.whoAmI == Main.myPlayer && player.ownedProjectileCounts[projType] <= 0)
				Projectile.NewProjectile(player.GetSource_Buff(buffIndex), player.Center, Vector2.Zero, projType, 0, 0f, player.whoAmI);
		}
	}
}
