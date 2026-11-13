using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Buffs.Palworld;
using nothing_mod.Content.Projectiles.NewWeapons;

namespace nothing_mod.Content.Projectiles.Palworld
{
	// Foxparks minion (wiki): no contact damage; shoots fireballs. Hovers instead of walking; no petting yet.
	public class FoxparksMinion : ModProjectile
	{

		public override void SetStaticDefaults()
		{
			Main.projPet[Type] = true;
			ProjectileID.Sets.MinionSacrificable[Type] = true;
			ProjectileID.Sets.MinionTargettingFeature[Type] = true;
		}

		public override void SetDefaults()
		{
			Projectile.width = 24;
			Projectile.height = 24;
			Projectile.friendly = false; // damage comes from its fireballs / flames
			Projectile.minion = true;
			Projectile.minionSlots = 0f;
			Projectile.DamageType = DamageClass.Summon;
			Projectile.penetrate = -1;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 30;
		}

		public override bool? CanCutTiles() => false;
		public override bool MinionContactDamage() => false; // wiki: no contact damage

		public override void AI()
		{
			Player owner = Main.player[Projectile.owner];
			if (!SimpleMinionAI.Upkeep(Projectile, owner, ModContent.BuffType<FoxparksBuff>()))
				return;
			FoxparksAI.Run(Projectile, owner, false);
		}

		public override bool PreDraw(ref Color lightColor) => FoxparksAI.Draw(Projectile);
	}
}
