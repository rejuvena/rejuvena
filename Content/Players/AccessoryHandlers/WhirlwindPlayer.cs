﻿using Terraria;
using NPC = On.Terraria.NPC;

namespace Rejuvena.Content.Players.AccessoryHandlers
{
    /// <summary>
    ///     Handles reversing knock-back.
    /// </summary>
    public class WhirlwindPlayer : RejuvenaPlayer
    {
        public bool Whirlwind;

        // Allow modifying the chance easily for other items in the future
        public int InXChance = -1;

        public override void Load()
        {
            base.Load();

            NPC.StrikeNPC += ModifyHitDirection;
        }

        public override void ResetEffects()
        {
            base.ResetEffects();

            Whirlwind = false;
        }

        private static double ModifyHitDirection(NPC.orig_StrikeNPC orig, Terraria.NPC self, int damage,
            float knockback, int hitDirection, bool crit, bool noEffect, bool fromNet)
        {
            WhirlwindPlayer whirlwind = Main.LocalPlayer.GetModPlayer<WhirlwindPlayer>();

            if (whirlwind.Whirlwind && hitDirection != 0 && 
                Main.rand.NextBool(whirlwind.InXChance == -1 ? 10 : whirlwind.InXChance))
                hitDirection *= -1; // flip sign

            return orig(self, damage, knockback, hitDirection, crit, noEffect, fromNet);
        }
    }
}