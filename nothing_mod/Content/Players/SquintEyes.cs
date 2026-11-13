using System.Reflection;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace nothing_mod.Content.Players
{
	// 1.4.5.7: players squint while using a magic weapon without enough mana (ManaRework.Squinting).
	// Overrides the vanilla eye frame just before the draw layers run.
	// EyeFrameToShow has a non-public setter, so it is set through reflection.
	// UNVERIFIED: that vanilla reads PlayerEyeHelper.EyeFrameToShow in its eye layer after ModifyDrawInfo.
	public class SquintEyes : ModPlayer
	{
		private static readonly MethodInfo SetEyeFrame = typeof(PlayerEyeHelper).GetProperty(nameof(PlayerEyeHelper.EyeFrameToShow))?.GetSetMethod(true);

		public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo) {
			if (Player.dead || !Player.GetModPlayer<ManaRework>().Squinting)
				return;
			SetEyeFrame?.Invoke(Player.eyeHelper, new object[] { (int)PlayerEyeHelper.EyeFrame.EyeHalfClosed });
		}
	}
}
