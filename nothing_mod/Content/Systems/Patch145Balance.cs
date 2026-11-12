using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Systems
{
	// Backports 1.4.5.x balance changes to existing 1.4.4.9 items.
	// Source: terraria.wiki.gg 1.4.5.0 and 1.4.5.7 patch notes; the final (latest) value is used
	// when a later patch changed it again. Only plain stat changes are done here; behavior changes
	// (new projectile effects, debuffs, mana rework) need separate work.
	public class Patch145Balance : GlobalItem
	{
		public override void SetDefaults(Item item)
		{
			// 1.4.5.0: summon weapons no longer cost mana (vanilla items only).
			if (item.type < ItemID.Count && item.damage > 0 && item.mana > 0 && item.DamageType.CountsAsClass(DamageClass.Summon))
				item.mana = 0;

			switch (item.type)
			{
				// --- Melee ---
				case ItemID.FormatC:           item.damage = 40; item.crit += 20; break;   // 1.4.5.7
				case ItemID.Kraken:            item.damage = 120; break;                    // 1.4.5.7
				case ItemID.Chik:              item.damage = 34; break;                     // 1.4.5.7
				case ItemID.Amarok:            item.damage = 47; break;                     // 1.4.5.7
				case ItemID.HelFire:           item.damage = 43; break;                     // 1.4.5.7
				case ItemID.ChlorophyteSaber:  item.damage = 80; break;                     // 1.4.5.7
				case ItemID.ShadowFlameKnife:  item.damage = 43; break;                     // 1.4.5.0
				case ItemID.PaladinsHammer:    item.damage = 100; break;                    // 1.4.5.0

				// --- Ranged ---
				case ItemID.Marrow:            item.damage = 40; break;                     // 1.4.5.7
				case ItemID.HallowedRepeater:  item.damage = 53; item.useTime = 16; item.useAnimation = 16; break; // 1.4.5.7
				case ItemID.CursedBullet:      item.damage = 15; break;                     // 1.4.5.7
				case ItemID.FrostburnArrow:    item.damage = 9; break;                      // 1.4.5.7
				case ItemID.SniperRifle:       item.damage = 200; break;                    // 1.4.5.0
				case ItemID.ElfMelter:         item.damage = 57; break;                     // 1.4.5.0
				case ItemID.DD2BetsyBow:        item.damage = 38; break;                     // 1.4.5.0
				case ItemID.PiranhaGun:        item.damage = 40; break;                     // 1.4.5.0
				case ItemID.TacticalShotgun:   item.damage = 36; break;                     // 1.4.5.0
				case ItemID.CandyCornRifle:    item.damage = 60; break;                     // 1.4.5.0
				case ItemID.JackOLanternLauncher: item.damage = 85; break;                  // 1.4.5.0
				case ItemID.ChainGun:          item.damage = 38; break;                     // 1.4.5.0

				// --- Magic ---
				case ItemID.UnholyTrident:     item.damage = 150; item.ArmorPenetration += 25; break; // 1.4.5.7
				case ItemID.FlowerofFire:      item.mana = 9; break;                        // 1.4.5.7
				case ItemID.LeafBlower:        item.useTime = 5; item.useAnimation = 15; break; // 1.4.5.7: 3 leaves per 15-frame use (values from reference port; the whirl-in-circles leaf behavior is not ported)
				case ItemID.ShadowbeamStaff:   item.knockBack = 6f; break;                  // 1.4.5.7
				case ItemID.ThunderStaff:     item.mana = 6; item.damage = 24; break;      // 1.4.5.0 / 1.4.5.4
				case ItemID.MagicDagger:       item.damage = 30; break;                     // 1.4.5.0
				case ItemID.SpaceGun:          item.damage = 20; break;                     // 1.4.5.0
				case ItemID.CrystalStorm:      item.damage = 35; break;                     // 1.4.5.0

				// --- Summon ---
				case ItemID.BoneWhip:         item.damage = 29; break;                     // 1.4.5.0
				case ItemID.ScytheWhip:       item.damage = 110; break;                    // 1.4.5.0
				case ItemID.RainbowWhip:      item.damage = 170; break;                    // 1.4.5.0
				case ItemID.DeadlySphereStaff: item.damage = 55; break;                     // 1.4.5.0
				case ItemID.MaceWhip:         item.damage = 160; break;                    // 1.4.5.0 Morning Star (tag crit change not ported)
				case ItemID.XenoStaff:        item.useTime = 36; item.useAnimation = 36; break; // 1.4.5.7 -12.5% attack speed
				case ItemID.StormTigerStaff:  item.knockBack = 2.5f; break;                // 1.4.5.7 Desert Tiger Staff
				case ItemID.AbigailsFlower:   item.knockBack = 2f; break;                  // 1.4.5.0 (now has knockback)
				case ItemID.SharpTears:       item.damage = 40; break;                     // 1.4.5.7
				case ItemID.BookofSkulls:     item.useTime = 32; item.useAnimation = 32; break; // 1.4.5.7
				case ItemID.MagicalHarp:      item.knockBack = 1.5f; break;                // 1.4.5.7
				case ItemID.SkyFracture:      item.shootSpeed = 8.75f; break;              // 1.4.5.0 +25% projectile speed
				case ItemID.CrystalSerpent:   item.shootSpeed = 8.5f; break;               // 1.4.5.0 ~+31% initial speed
				case ItemID.BatBat:           item.useTime = 40; item.useAnimation = 40; break; // 1.4.5.0 +12.5% attack speed
				case ItemID.Gladius:          item.useTime = 6; item.useAnimation = 18; break; // 1.4.5.0 ~+17% attack speed
				case ItemID.AntlionClaw:      item.useTime = 15; item.useAnimation = 15; item.crit = 7; break; // 1.4.5.0 Mandible Blade (throw mechanic not ported)
				case ItemID.StylistKilLaKillScissorsIWish: item.useTime = 13; item.useAnimation = 13; break; // 1.4.5.0 -8% attack speed (throw/bleed not ported)
				case ItemID.TaxCollectorsStickOfDoom: item.damage = 40; item.value = 100000; break; // 1.4.5.0 Classy Cane (coin drop on hit not ported)
				case ItemID.Musket:           item.crit = 8; break;                        // 1.4.5.0
				case ItemID.FlintlockPistol:  item.crit = 6; item.shootSpeed = 6f; break;  // 1.4.5.0
				case ItemID.Revolver:         item.knockBack = 4.5f; item.value = 150000; break; // 1.4.5.0 (crit removal skipped: paired with a ramp-up mechanic)
				case ItemID.Harpoon:          item.shootSpeed = 15f; break;                // 1.4.5.0 ~+36% projectile speed
				case ItemID.AleThrowingGlove: item.damage = 32; item.useTime = 32; item.useAnimation = 32; item.knockBack = 4f; item.shootSpeed = 11f; item.value = 12500; break; // 1.4.5.0/1.4.5.7 Ale Tosser
				case ItemID.Gradient:         item.rare = ItemRarityID.LightRed; break;    // 1.4.5.7 orange -> red tier (ref port: rarity 4)

				// --- Equipment and consumables ---
				case ItemID.DeadMansSweater:   item.defense += 4; break;                    // 1.4.5.0
				case ItemID.AnkhShield:        item.defense = 5; break;                     // 1.4.5.7
				case ItemID.MagicCuffs:        item.defense = 2; break;                     // 1.4.5.7
				case ItemID.CelestialCuffs:    item.defense = 2; break;                     // 1.4.5.7
				case ItemID.BottledWater:      item.healLife = 30; break;                   // 1.4.5.0 (Potion Sickness 45s: Patch145Items)
				case ItemID.PumpkinPie:        item.buffType = BuffID.WellFed; break;       // 1.4.5.0 Well Fed instead of Plenty Satisfied, duration unchanged
				// --- Prices (item.value is the buy price; sell = value / 5) ---
				case ItemID.Katana:            item.value = 50000; break;                   // 1.4.5.0 buy 10g -> 5g
				case ItemID.BirdieRattle:      item.value = 500000; break;                  // 1.4.5.0 buy 1p -> 50g
				case ItemID.BambooLeaf:        item.value = 750000; break;                  // 1.4.5.0 buy 1p -> 75g
				case ItemID.BlandWhip:         item.value = 50000; break;                   // 1.4.5.7 Leather Whip buy 10g -> 5g
				case ItemID.LavaSkull:         item.value = 375000; break;                  // 1.4.5.0 Magma Skull sell 2g50s -> 7g50s
				case ItemID.MoltenSkullRose:   item.value = 450000; break;                  // 1.4.5.0 sell 5g -> 9g
				case ItemID.LivingLoom:        item.value = 200; break;                     // 1.4.5.0 sell 2g -> 40c
				case ItemID.SuperManaPotion:   item.healMana = 400; break;                  // 1.4.5.7
			}
		}
	}
}
