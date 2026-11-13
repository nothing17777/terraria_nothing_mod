using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace nothing_mod.Content.Mounts
{
	// Shared Chillet mount setup. Wiki (Chillet): 33 mph max speed in 1.85 s, jump reach 7.85 tiles (jump duration 6, jump speed 8.01), +10% damage while mounted,
	// Rocket Slam dash (see ChilletPlayer). Art is the vanilla Bunny mount sheet reused as a placeholder (no Chillet mount art exists in the game files),
	// so every frame/offset below is UNVERIFIED and may need tuning in-game. Trusty speeds are UNVERIFIED (wiki: "increased max speed, jump reach").
	public abstract class ChilletMountBase : ModMount
	{
		protected abstract int MountBuffType { get; }
		protected virtual float RunSpeed => 6.5f; // 33 mph / 5.11 mph per px-per-tick (derived, UNVERIFIED conversion)
		protected virtual int JumpHeight => 6;
		protected virtual float JumpSpeed => 8.01f;
		public virtual bool Trusty => false;

		public override void SetStaticDefaults()
		{
			MountData.buff = MountBuffType;
			MountData.heightBoost = 14;
			MountData.fallDamage = 0.5f;
			MountData.runSpeed = RunSpeed;
			MountData.dashSpeed = RunSpeed;
			MountData.flightTimeMax = 0;
			MountData.fatigueMax = 0;
			MountData.jumpHeight = JumpHeight;
			MountData.acceleration = RunSpeed / (1.85f * 60f); // reaches max speed in 1.85 s
			MountData.jumpSpeed = JumpSpeed;
			MountData.blockExtraJumps = false;
			MountData.totalFrames = 7;
			int[] offsets = new int[MountData.totalFrames];
			for (int i = 0; i < offsets.Length; i++)
				offsets[i] = 14;
			MountData.playerYOffsets = offsets;
			MountData.xOffset = 0;
			MountData.yOffset = 0;
			MountData.bodyFrame = 3;
			MountData.playerHeadOffset = 18;
			MountData.standingFrameCount = 1;
			MountData.standingFrameDelay = 12;
			MountData.standingFrameStart = 0;
			MountData.runningFrameCount = 6;
			MountData.runningFrameDelay = 30;
			MountData.runningFrameStart = 1;
			MountData.flyingFrameCount = 0;
			MountData.flyingFrameDelay = 0;
			MountData.flyingFrameStart = 0;
			MountData.inAirFrameCount = 1;
			MountData.inAirFrameDelay = 12;
			MountData.inAirFrameStart = 2;
			MountData.idleFrameCount = 0;
			MountData.idleFrameDelay = 0;
			MountData.idleFrameStart = 0;
			MountData.idleFrameLoop = false;
			MountData.swimFrameCount = MountData.inAirFrameCount;
			MountData.swimFrameDelay = MountData.inAirFrameDelay;
			MountData.swimFrameStart = MountData.inAirFrameStart;
			if (Main.netMode != Terraria.ID.NetmodeID.Server)
			{
				MountData.textureWidth = MountData.backTexture.Width();
				MountData.textureHeight = MountData.backTexture.Height();
			}
		}
	}
}
