using System.Reflection;
using Rejuvena.Core.Services.Transformers;
using Terraria;
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

        public override bool ThreadSafe => false;

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

            if (Main.menuMode < 1000000)
                return;

            offY = 210;
            spacing = 42;
            numButtons = 1;
            buttonVerticalSpacing[0] = 18;
            buttonScales[0] = 1f;
            buttonNames[0] = "null";
        }
    }
}