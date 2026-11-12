using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Systems
{
	// Every whip (vanilla and modded) keeps swinging while the attack button is held.
	public class WhipsAutoSwing : GlobalItem
	{
		public override void SetDefaults(Item item)
		{
			if (item.shoot > ProjectileID.None && ProjectileID.Sets.IsAWhip[item.shoot])
				item.autoReuse = true;
		}
	}
}
