using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Projectiles
{
	public class VoidBolt : ModProjectile
	{
		// Borrow the vanilla Enchanted Sword beam sprite so no custom art is needed yet.
		public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.EnchantedBeam;

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.EnchantedBeam);
			Projectile.aiStyle = ProjAIStyleID.Beam;
			AIType = ProjectileID.EnchantedBeam;
		}
	}
}
