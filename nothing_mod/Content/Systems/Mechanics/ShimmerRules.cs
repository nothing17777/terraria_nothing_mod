using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Systems.Mechanics
{
	// 1.4.5.x Shimmer rules.
	//  - Shimmer invincibility no longer applies to bosses, invasion enemies or the Torch God: tModLoader already
	//    lets bosses and invasion enemies hit shimmered players (see NPCID.Sets.CanHitPastShimmer docs); Torch God is added here.
	//  - Party Bunny becomes a Faeling when shimmered (same target as the Christmas Bunny).
	//  - Slime Crown thrown into Shimmer starts a Slime Rain (SlimeCrownShimmer below).
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

	// UNVERIFIED: the Slime Crown is consumed (one per rain); the changelog only says shimmering it starts Slime Rain.
	// UNVERIFIED: Main.StartSlimeRain(bool announce) handles the world sync itself; the extra WorldData send is harmless.
	public class SlimeCrownShimmer : GlobalItem
	{
		public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type == ItemID.SlimeCrown;

		public override void Update(Item item, ref float gravity, ref float maxFallSpeed) {
			if (!item.shimmerWet || Main.netMode == NetmodeID.MultiplayerClient || item.stack <= 0)
				return;
			if (!Main.slimeRain)
				Main.StartSlimeRain(true);
			item.stack--;
			if (item.stack <= 0)
				item.TurnToAir();
			if (Main.netMode == NetmodeID.Server) {
				NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item.whoAmI);
				NetMessage.SendData(MessageID.WorldData);
			}
		}
	}
}
