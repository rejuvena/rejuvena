using System;
using System.Reflection;
using Rejuvena.Core.Services.MenuModes;
using Rejuvena.Core.Services.Transformers;
using Terraria;
using Terraria.ModLoader;
using TomatoLib.Common.Utilities.Extensions;

namespace Rejuvena.Core.Services.Impl.Transformers
{
    public class MenuModeTransformer : DetourTransformerMethod
    {
        private delegate void Orig(
            Main main,
            int selectedMenu,
            string[] buttonNames,
            float[] buttonScales,
            int[] buttonVerticalSpacing,
            ref int offY,
            ref int spacing,
            ref int numButtons,
            ref bool backButtonDown
        );

        public override MethodInfo? MethodToTransform => typeof(Main).Assembly.GetType(
            "Terraria.ModLoader.UI.Interface"
        ).GetCachedMethod("ModLoaderMenus");

        public override MethodInfo TransformingMethod => GetType().GetCachedMethod(nameof(AppendModdedMenus));

        private static void AppendModdedMenus(
            Orig orig,
            Main main,
            int selectedMenu,
            string[] buttonNames,
            float[] buttonScales,
            int[] buttonVerticalSpacing,
            ref int offY,
            ref int spacing,
            ref int numButtons,
            ref bool backButtonDown
        )
        {
            orig(
                main,
                selectedMenu,
                buttonNames,
                buttonScales,
                buttonVerticalSpacing,
                ref offY,
                ref spacing,
                ref numButtons,
                ref backButtonDown
            );

            ModContent.GetInstance<Rejuvena>().GetService<MenuModeHandler>().DrawMenus(
                main,
                selectedMenu,
                buttonNames,
                buttonScales,
                buttonVerticalSpacing,
                ref offY,
                ref spacing,
                ref numButtons,
                ref backButtonDown
            );
        }
    }
}