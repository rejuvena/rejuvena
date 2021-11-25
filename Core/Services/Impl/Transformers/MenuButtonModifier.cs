using Microsoft.Xna.Framework;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Rejuvena.Core.Services.MenuModes;
using Rejuvena.Core.Services.Transformers;
using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using TomatoLib.Common.Utilities.Extensions;

namespace Rejuvena.Core.Services.Impl.Transformers
{
    public class MenuButtonModifier : ILTransformerMethod
    {
        private delegate List<MenuButton> Cancer(
            ref int numButtons,
            ref bool[] readonlyText,
            ref bool[] unhoverableText,
            ref bool[] loweredAlpha,
            ref int[] xOffsetPos,
            ref int[] yOffsetPos,
            ref byte[] color,
            ref float[] scale,
            ref bool[] noCenterOffset,
            ref string[] text,
            Color defaultColor,
            out Color[] buttonColor,
            out Action?[] onLeftClick,
            out Action?[] onRightClick,
            out Action?[] onHover
        );

        public override MethodInfo? MethodToTransform => typeof(Main).GetCachedMethod("DrawMenu");

        public override MethodInfo TransformingMethod => GetType().GetCachedMethod(nameof(InjectMenuButtons));

        public override bool ThreadSafe => false;

        private static void InjectMenuButtons(ILContext il)
        {
            // Goal: Imitate https://github.com/tModLoader/tModLoader/pull/1358/files#diff-e3389e7ea64819b97386e48546a4c037936825946f9d14219333bf0404292724
            ILCursor c = new(il);

            // Define local variable for our MenuButton list.
            int buttonsIndex = c.AddVariable<List<MenuButton>>();
            
            // Define local variables for out parameter stuff...
            int buttonColorIndex = c.AddVariable<Color[]>();
            int onLeftClickIndex = c.AddVariable<Action?[]>();
            int onRightClickIndex = c.AddVariable<Action?[]>();
            int onHoverIndex = c.AddVariable<Action?[]>();

            // Praise the lords for I have found this godly anchor point.
            c.GotoNext(x => x.MatchCall<Color>("get_Black"));
            c.GotoNext(x => x.MatchCall<Color>("get_Black"));
            c.GotoNext(x => x.MatchCall<Color>("get_Black"));
            c.GotoNext(x => x.MatchCall<Color>("get_Black"));
            c.GotoNext(x => x.MatchCall<Color>("get_Black"));
            c.GotoPrev(x => x.MatchLdcI4(0));
            c.GotoPrev(MoveType.After, x => x.MatchStloc(out _)); // Move out of the for-each loop.

            #region List Initialization

            c.Emit(OpCodes.Ldloca, 8); // ref numButtons int
            c.Emit(OpCodes.Ldloca, 16); // readonlyText bool[]
            c.Emit(OpCodes.Ldloca, 17); // ref unhoverableText bool[]
            c.Emit(OpCodes.Ldloca, 18); // ref loweredAlpha bool[]
            c.Emit(OpCodes.Ldloca, 19); // ref yOffsetPos int[]
            c.Emit(OpCodes.Ldloca, 20); // ref xOffsetPos int[]
            c.Emit(OpCodes.Ldloca, 21); // ref color byte[]
            c.Emit(OpCodes.Ldloca, 22); // ref scale float[]
            c.Emit(OpCodes.Ldloca, 23); // ref noCenterOffset bool[]
            c.Emit(OpCodes.Ldloca, 26); // ref text string[]
            c.Emit(OpCodes.Ldloc, 2); // defaultColor Color

            // Emit our local variables to set them!!
            c.Emit(OpCodes.Ldloca, buttonColorIndex); // out
            c.Emit(OpCodes.Ldloca, onLeftClickIndex); // out
            c.Emit(OpCodes.Ldloca, onRightClickIndex); // out
            c.Emit(OpCodes.Ldloca, onHoverIndex); // out

            // c.EmitDelegate<Cancer>(MenuModeHandler.ButtonList);
            c.Emit(OpCodes.Call, new Cancer(MenuModeHandler.ButtonList).Method);

            c.Emit(OpCodes.Stloc, buttonsIndex);

            #endregion

            #region Color Modification

            ILLabel? matchedLabel = null;

            // Jump out of the switch case.
            c.GotoNext(x => x.MatchBr(out _));
            c.GotoNext(x => x.MatchBrfalse(out _));
            c.GotoNext(x => x.MatchSwitch(out _));
            c.GotoNext(x => x.MatchBr(out _));
            c.GotoNext(x => x.MatchBr(out matchedLabel));

            c.GotoLabel(matchedLabel);

            c.Emit(OpCodes.Ldloc, buttonColorIndex);
            c.Emit(OpCodes.Ldloc, 667); // index int
            c.Emit(OpCodes.Ldelem_Any, typeof(Color));
            c.Emit(OpCodes.Stloc, 668); // Color

            #endregion
        }
    }
}