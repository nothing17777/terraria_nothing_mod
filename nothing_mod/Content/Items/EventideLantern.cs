using Microsoft.Xna.Framework;
using nothing_mod.Content.Projectiles;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items
{
    public class EventideLantern : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 38;
            Item.damage = 17;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 12;
            Item.useTime = 32;
            Item.useAnimation = 32;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 2f;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item20;
            Item.value = Item.buyPrice(gold: 2);
            Item.rare = ItemRarityID.Orange;
            Item.shoot = ModContent.ProjectileType<EventideSpark>();
            Item.shootSpeed = 8f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source,
            Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // Only the using player creates sparks; projectile sync handles other clients.
            if (player.whoAmI == Main.myPlayer)
            {
                for (int i = -1; i <= 1; i++)
                {
                    Vector2 sparkVelocity = velocity.RotatedBy(MathHelper.ToRadians(18f * i));
                    Projectile.NewProjectile(source, position, sparkVelocity, type,
                        damage, knockback, player.whoAmI);
                }
            }
            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.HellstoneBar, 12)
                .AddIngredient(ItemID.FallenStar, 5)
                .AddIngredient(ItemID.Firefly, 10)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
