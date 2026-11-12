# Terraria mod: nothing_mod

tModLoader mod (v2026.9.3.1, Terraria 1.4.4.9, .NET 8 SDK, Apple Silicon Mac). Display name "nothing's mod". Played with a friend, so keep content multiplayer-safe.

## Layout
- Mod source: `nothing_mod/` (this folder is the real location, on the T7 drive).
- `~/Library/Application Support/Terraria/tModLoader/ModSources/nothing_mod` is a **symlink** to it. tModLoader only builds from ModSources, so do not delete the symlink.
- `tModLoader.targets` in this folder is a symlink to the one in ModSources. `nothing_mod.csproj` imports `..\tModLoader.targets`, so it must stay here.
- If the T7 drive is unplugged, the mod disappears from tModLoader.

## Build and test
- The user builds in-game: Workshop > Develop Mods > **Build + Reload**. Then test in a world.
- Claude can check that the C# compiles (no `error CS` lines, `bin/Debug/net8.0/nothing_mod.dll` produced):
  `export PATH="$PATH:/usr/local/share/dotnet"; cd nothing_mod && dotnet build -c Debug 2>&1 | grep -E "error CS|warning CS"`
- The command-line build still ends with an FNA3D "Unable to load shared library" error at the pack step. That is an environment limit, not a code error. Only the in-game build produces the playable `.tmod`.
- `/voidblade` in chat gives the Void Blade (`Content/Commands/GiveBladeCommand.cs`). Vanilla has no `/give`.

## How to add content
Namespace is `nothing_mod.<folder path>`, one class per file, file name = class name.

- **Item:** `Content/Items/<Name>.cs`, class extends `ModItem`, set stats in `SetDefaults()`, recipe in `AddRecipes()`. Pattern: `Content/Items/VoidBlade.cs`.
- **Projectile:** `Content/Projectiles/<Name>.cs`, extends `ModProjectile`. Link it from an item with `Item.shoot = ModContent.ProjectileType<Name>();`. Pattern: `Content/Projectiles/VoidBolt.cs` (borrows a vanilla sprite through `Texture`).
- **Chat command:** `Content/Commands/<Name>.cs`, extends `ModCommand`.
- **Sprites:** put `<ClassName>.png` next to the `.cs` file with the same name. An item without a png fails to load unless `Texture` points at a vanilla sprite (`"Terraria/Images/Item_" + ItemID.X` or `Projectile_` for projectiles).
- **Names and tooltips:** `Localization/en-US_Mods.nothing_mod.hjson`, under `Items: { <ClassName>: { DisplayName / Tooltip } }`. tModLoader auto-adds missing entries after a build.
- NPCs, bosses, armor, accessories, tiles and buffs follow the same one-class-per-file pattern. Check ExampleMod before writing them: https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
- Never guess tModLoader API names. Verify against ExampleMod or the wiki, and mark anything unverified.

## Multiplayer
Host and friend need the same tModLoader version and the same mod version. To share, send the built `.tmod` from `~/Library/Application Support/Terraria/tModLoader/Mods/`, or publish to the Steam Workshop.

## 1.4.5 backport (in progress)
Goal: bring Terraria 1.4.5.x content into this 1.4.4.9 mod. Source of truth is terraria.wiki.gg patch pages 1.4.5.0 to 1.4.5.8 and each item's own wiki page. Never invent stats: mark guesses `// UNVERIFIED`; unimplemented mechanics are marked `// TODO`.
- `Content/Systems/Patch145Balance.cs`: numeric balance changes to existing items.
- `Content/Players/ChestCraftingPlayer.cs`: crafting uses chests within 25 tiles (ModPlayer.AddMaterialsForCrafting).
- New content: `Content/Items/{Whips,NewWeapons,Accessories,Armor}`, with matching Projectiles/Buffs folders.
- Item icons are the real wiki images. Swing/projectile art, armor equip sheets and Rainbow Glowstick (animated gif) are still placeholders.
- terraria.wiki.gg blocks curl (403 bot check). Images were fetched from inside Chrome (Claude in Chrome) and saved through a temporary localhost receiver, which needs Chrome's local-network "Allow" prompt.
- Not started: mana rework, Bleeding on enemies, crafting search bar, banner menu, Dead Cells / Palworld content, drops and shop sources for new items.

## Current content
- Void Blade: melee sword, 35 damage, fires a Void Bolt. Recipe: 8 Iron Bar + 3 Fallen Star at an Anvil.
- Void Bolt: projectile using the vanilla Enchanted Beam sprite as a placeholder.
- Sergeant United Shield tiers (`Content/Items/NewWeapons/SergeantShield*.cs`, recipes for the vanilla middle tier in `Content/Systems/SergeantShieldRecipes.cs`): -2 (20 dmg), -1 (40), vanilla (80), +1 (160), +2 (300). All hand-crafted, no bench. Behavior is cloned from the vanilla item. Parry is TODO. Not yet compile-checked.
- Blade Staff tiers (`Content/Items/NewWeapons/BladeStaff*.cs`, vanilla middle tier recipes in `Content/Systems/BladeStaffRecipes.cs`): -2 (2 dmg), -1 (4), vanilla (6), +1 (10), +2 (20). Hand-crafted. Uses `ItemID.Smolstar` (UNVERIFIED internal name).
- Frozen Wings tiers (`Content/Items/Accessories/FrozenWings*.cs`, vanilla middle tier recipes in `Content/Systems/FrozenWingsRecipes.cs`): flight 0.42 / 1.67 / 2.17 (vanilla) / 2.67 / 3 s. Flight time applied in `Content/Players/FrozenWingsPlayer.cs` (UNVERIFIED that `wingTimeMax` override in PostUpdateEquips sticks). Hand-crafted. Not yet compile-checked.
- Item art is placeholder only. Real sprites need to come from the user.
