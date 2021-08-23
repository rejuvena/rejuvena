using Terraria;
using Terraria.ModLoader;
using TomatoLib.Core.Compatibility.Calls;

namespace Rejuvena.Content.Players.AccessoryHandlers
{
    public class WhirlwindPlayer : ModPlayerWithCalls
    {
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

        public override string Accessor => "WhirlwindPlayer";

        public override object Action(Mod mod, params object[] args)
        {
            DefaultModCaller.AssertArguments(args, typeof(string));

            switch (((string) args[0]).ToLower())
            {
                // ReSharper disable once StringLiteralTypo
                case "setchance" when args[1] is int chance:
                    InXChance = chance;
                    break;

                // ReSharper disable once StringLiteralTypo
                case "setenabled" when args[1] is bool enabled:
                    Whirlwind = enabled;
                    break;
            }

            return null;
        }
    }
}