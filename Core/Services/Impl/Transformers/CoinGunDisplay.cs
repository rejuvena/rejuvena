using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Rejuvena.Core.Services.Transformers;
using Terraria;
using Terraria.ID;
using TomatoLib.Common.Utilities.Extensions;

namespace Rejuvena.Core.Services.Impl.Transformers
{
    public class CoinGunDisplay : ILTransformerMethod
    {
        public static List<int> Items = new();

        public override MethodInfo? MethodToTransform =>
            typeof(Main).GetCachedMethod("MouseText_DrawItemTooltip_GetLinesInfo");

        public override MethodInfo TransformingMethod => GetType().GetCachedMethod(nameof(SwapCoinGun));

        public override void Load()
        {
            base.Load();

            Items.Add(ItemID.CoinGun);
        }

        public override void Unload()
        {
            base.Unload();

            Items.Clear();
        }

        public static void SwapCoinGun(ILContext il)
        { 
            /*
             * Original block:
             * 	IL_0155: ldsfld class Terraria.Player[] Terraria.Main::player
	         *  IL_015a: ldsfld int32 Terraria.Main::myPlayer
	         *  IL_015f: ldelem.ref
	         *  IL_0160: ldc.i4 905
	         *  IL_0165: callvirt instance bool Terraria.Player::HasItem(int32)
	         *  IL_016a: br.s IL_016d
             *
             * Pops callvirt opcode and replaces it with our own delegate.
             *
             * Goal:
             *  Remove a hardcoded check for the Coin Gun in exchange for a generalized check for any item ID in CoinStatDisplay
             *  This allows other items to enable tooltips for coin damage and other stuff normally limited to the coin gun
             */

            ILCursor c = new(il);
            
            c.TryGotoNext(x => x.MatchCallvirt<Player>("HasItem"));
            c.Index++;
            c.Emit(OpCodes.Pop);
            c.EmitDelegate<Func<bool>>(() => Items.Any(x => Main.LocalPlayer.HasItem(x)));
        }
    }
}