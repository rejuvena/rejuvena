using Terraria;

namespace Rejuvena.Content.Players.AccessoryHandlers
{
    public class WhirlwindPlayer : RejuvenaPlayer
    {
        #region ModPlayer

        public bool Whirlwind;

        // Allow modifying the chance easily for other items in the future
        public int InXChance = -1;

        public override void Load()
        {
            base.Load();

            On.Terraria.NPC.StrikeNPC += ModifyHitDirection;
        }

        public override void ResetEffects()
        {
            base.ResetEffects();

            Whirlwind = false;
        }

        private static double ModifyHitDirection(On.Terraria.NPC.orig_StrikeNPC orig, NPC self, int damage,
            float knockback, int hitDirection, bool crit, bool noEffect, bool fromNet)
        {
            WhirlwindPlayer whirlwind = Main.LocalPlayer.GetModPlayer<WhirlwindPlayer>();

            if (whirlwind.Whirlwind && hitDirection != 0 &&
                Main.rand.NextBool(whirlwind.InXChance == -1 ? 10 : whirlwind.InXChance))
                hitDirection *= -1; // flip sign

            return orig(self, damage, knockback, hitDirection, crit, noEffect, fromNet);
        }

        #endregion
    }
}