using Terraria;
using Terraria.ModLoader;
using TomatoLib.Core.Implementation.Compatibility.Calls;
using NPC = On.Terraria.NPC;

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

        public override string Accessor => "WhirlwindPlayer";

        public override object Action(Mod mod, params object[] args)
        {
            DefaultModCaller.AssertArguments(args, typeof(Player), typeof(string));
            Player player = args[0] as Player;

            switch (((string) args[1]).ToLower())
            {
                // ReSharper disable once StringLiteralTypo
                case "setchance" when args[2] is int chance:
                    player!.GetModPlayer<WhirlwindPlayer>().InXChance = chance;
                    break;

                // ReSharper disable once StringLiteralTypo
                case "setenabled" when args[2] is bool enabled:
                    player!.GetModPlayer<WhirlwindPlayer>().Whirlwind = enabled;
                    break;
            }

            return null;
        }
    }
}