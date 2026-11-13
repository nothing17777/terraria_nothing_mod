using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Projectiles.Palworld;

namespace nothing_mod.Content.Buffs.Palworld
{
	// Pet buff. 
	public class TrustyDigtoiseBuff : ModBuff
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
			int projType = ModContent.ProjectileType<TrustyDigtoisePet>();
			if (player.whoAmI == Main.myPlayer && player.ownedProjectileCounts[projType] <= 0)
				Projectile.NewProjectile(player.GetSource_Buff(buffIndex), player.Center, Vector2.Zero, projType, 0, 0f, player.whoAmI);
		}
	}
}
