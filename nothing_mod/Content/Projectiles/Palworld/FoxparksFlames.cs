using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Projectiles.Palworld;

// Huggy Fire flamethrower shot (wiki). ai[0]: 1 = Trusty (Hellfire, 10.5 px/tick). Pierces 3 targets (dies on the 4th), -15% damage per hit,
// 10 ticks static immunity, stops damaging after 54 ticks; hitbox grows from 6x6 by 14 (Trusty 38) px per side.
public class FoxparksFlames : ModProjectile
{
	public override void SetStaticDefaults()
	{
		Main.projFrames[Type] = 7;
		ProjectileID.Sets.MinionShot[Type] = true;
	}

	public override void SetDefaults()
	{
		Projectile.width = 6;
		Projectile.height = 6;
		Projectile.friendly = true;
		Projectile.DamageType = DamageClass.Summon;
		Projectile.penetrate = 4;
		Projectile.timeLeft = 60;
		Projectile.tileCollide = true;
		Projectile.ignoreWater = true;
		Projectile.usesIDStaticNPCImmunity = true;
		Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void AI()
	{
		Projectile.localAI[0]++;
		Projectile.rotation = Projectile.velocity.ToRotation();
		Projectile.frame = System.Math.Min(6, (int)(Projectile.localAI[0] * 7f / 54f));
		Projectile.scale = 0.4f + 0.8f * Projectile.localAI[0] / 54f;
		Lighting.AddLight(Projectile.Center, 1f, 0.5f, 0.1f);
		if (Projectile.localAI[0] >= 54f)
			Projectile.Kill();
		if (Projectile.wet && !Projectile.lavaWet)
			Projectile.Kill();
	}

	public override void ModifyDamageHitbox(ref Rectangle hitbox)
	{
		int grow = (int)((Projectile.ai[0] == 1f ? 38f : 14f) * System.Math.Min(1f, Projectile.localAI[0] / 53f));
		hitbox.Inflate(grow, grow);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(Projectile.ai[0] == 1f ? BuffID.OnFire3 : BuffID.OnFire, 300); // wiki: 5 s
		Projectile.damage = (int)(Projectile.damage * 0.85f); // wiki: loses 15% damage per hit
	}
}
