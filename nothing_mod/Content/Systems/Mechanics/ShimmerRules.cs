using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Systems.Mechanics
{
	// 1.4.5.x Shimmer rules.
	//  - Shimmer invincibility no longer applies to bosses, invasion enemies or the Torch God: tModLoader already
	//    lets bosses and invasion enemies hit shimmered players (see NPCID.Sets.CanHitPastShimmer docs); Torch God is added here.
	//  - Party Bunny becomes a Faeling when shimmered (same target as the Christmas Bunny).
	// TODO: shimmering a Slime Crown to start Slime Rain (no item/NPC transform fits; needs a custom shimmer hook).
	public class ShimmerRules : ModSystem
	{
		public override void PostSetupContent() {
			NPCID.Sets.CanHitPastShimmer[NPCID.TorchGod] = true;
			// UNVERIFIED: BunnyXmas' vanilla shimmer target is a Faeling.
			int faeling = NPCID.Sets.ShimmerTransformToNPC[NPCID.BunnyXmas];
			if (faeling >= 0)
				NPCID.Sets.ShimmerTransformToNPC[NPCID.PartyBunny] = faeling;
		}
	}
}
