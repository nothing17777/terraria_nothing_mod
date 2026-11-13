using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Projectiles
{
    public class EventideSpark : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.CultistIsResistantTo[Type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 16;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 150;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
        }

        public override void AI()
        {
            Projectile.ai[1]++;
            // ai[0] stores the target index plus one, with zero meaning no target.
            int targetIndex = (int)Projectile.ai[0] - 1;
            NPC target = targetIndex >= 0 && targetIndex < Main.maxNPCs
                ? Main.npc[targetIndex] : null;

            bool validTarget = IsValidTarget(target);
            // Let the owner's simulation pick targets and synchronize that choice.
            if (Projectile.owner == Main.myPlayer && Projectile.ai[1] >= 18f &&
                (!validTarget || Projectile.ai[1] % 12f == 0f))
            {
                NPC nearest = null;
                float nearestDistance = 480f * 480f;
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC candidate = Main.npc[i];
                    if (!IsValidTarget(candidate))
                        continue;
                    float distance = Vector2.DistanceSquared(Projectile.Center, candidate.Center);
                    if (distance < nearestDistance)
                    {
                        nearestDistance = distance;
                        nearest = candidate;
                    }
                }

                float selectedTarget = nearest == null ? 0f : nearest.whoAmI + 1;
                if (Projectile.ai[0] != selectedTarget)
                {
                    Projectile.ai[0] = selectedTarget;
                    Projectile.netUpdate = true;
                }
                target = nearest;
                validTarget = target != null;
            }

            if (Projectile.ai[1] >= 18f && validTarget)
            {
                Vector2 desiredVelocity = (target.Center - Projectile.Center)
                    .SafeNormalize(Vector2.UnitY) * 10f;
                Projectile.velocity = (Projectile.velocity * 17f + desiredVelocity) / 18f;
            }

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            if (!Main.dedServ)
            {
                Lighting.AddLight(Projectile.Center, 0.35f, 0.12f, 0.55f);
                if (Projectile.ai[1] % 3f == 0f)
                {
                    Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width,
                        Projectile.height, DustID.PurpleTorch);
                    dust.noGravity = true;
                    dust.velocity = -Projectile.velocity * 0.15f;
                    dust.scale = 0.9f;
                }
            }
        }

        private bool IsValidTarget(NPC npc)
        {
            return npc != null && npc.CanBeChasedBy(Projectile)
                && Vector2.DistanceSquared(Projectile.Center, npc.Center) <= 480f * 480f
                && Collision.CanHitLine(Projectile.position, Projectile.width,
                    Projectile.height, npc.position, npc.width, npc.height);
        }

        public override Color? GetAlpha(Color lightColor) => Color.White;

        public override void OnKill(int timeLeft)
        {
            if (Main.dedServ)
                return;
            for (int i = 0; i < 8; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width,
                    Projectile.height, DustID.PurpleTorch);
                dust.noGravity = true;
                dust.velocity *= 1.5f;
            }
        }
    }
}
