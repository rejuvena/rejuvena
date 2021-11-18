using System;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Rejuvena.Core.Services.Transformers;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using TomatoLib.Common.Utilities.Extensions;

namespace Rejuvena.Core.Services.Impl.Transformers
{
    public class DrawMenuTextOverride : ILTransformerMethod
    {
        public override MethodInfo? MethodToTransform => typeof(Main).GetCachedMethod("DrawMenu");

        public override MethodInfo TransformingMethod => GetType().GetCachedMethod(nameof(ModifyVersionText));

        public override bool ThreadSafe => false;

        public static void ModifyVersionText(ILContext il)
        {
            ILCursor c = new(il);

            c.GotoNext(x => x.MatchStloc(741));

            for (int i = 0; i < 2; i++)
                c.GotoNext(x => x.MatchLdsfld<Main>("spriteBatch"));

            c.GotoNext(MoveType.After, x => x.MatchLdloc(741));
            c.Emit(OpCodes.Pop);
            c.Emit(OpCodes.Ldstr, ""); // Replace draw text with empty string (lazy).

            c.Emit(OpCodes.Ldloc, 734); // num107 (for index)
            c.Emit(OpCodes.Ldloc, 736); // x offset
            c.Emit(OpCodes.Ldloc, 741); // text to draw
            c.EmitDelegate<Action<int, int, string>>((index, xOffset, text) =>
            {
                // Only draw when the white text would draw
                if (index != 4)
                    return;

                string defaultVersText = Main.versionNumber;

                // Insert "Terraria " (with a space) before the version (i.e. "v1.4.2.3") if it hasn't been already.
                // This is compatible with any other added text by any other mod.
                if (text.Contains(defaultVersText) && !text.Contains($"Terraria {defaultVersText}"))
                    text = text.Insert(text.IndexOf(defaultVersText, StringComparison.Ordinal), "Terraria ");

                if (text.Contains(BuildInfo.BranchName))
                {
                    // In case branch name is something like "1.4".
                    int dupIndex = text.IndexOf(
                        BuildInfo.BranchName,
                        text.IndexOf(BuildInfo.BranchName, StringComparison.Ordinal) + 1,
                        StringComparison.Ordinal
                    );

                    text = text.Insert(dupIndex != -1 ? dupIndex : text.IndexOf(BuildInfo.BranchName, StringComparison.Ordinal), "\n");
                }

                if (text.Contains(BuildInfo.Purpose.ToString()))
                    text = text.Insert(text.IndexOf(BuildInfo.Purpose.ToString(), StringComparison.Ordinal), "\n");

                if (text.Contains($", built {BuildInfo.BuildDate:g}"))
                    text = text.Replace($", built {BuildInfo.BuildDate:g}", $"\nbuilt {BuildInfo.BuildDate:g}");

                DrawVersionText(xOffset, text);
            });
        }

        private static void DrawVersionText(int xOffset, string text)
        {
            // Split the text into a list of strings divided by newlines, reverse to order them properly, then convert them to an array
            string[] lines = text.Split('\n').Reverse().ToArray();

            // Draw text for each line, offsetting it on the y-axis as well
            for (int i = 0; i < lines.Length; i++)
                Utils.DrawBorderString(
                    Main.spriteBatch, lines[i],
                    new Vector2(xOffset + 10f, Main.screenHeight - 2f - 28f * (i + 1)),
                    Color.White
                );

            // Finally, draw our text at the very top
            Utils.DrawBorderString(
                Main.spriteBatch,
                $"Rejuvena {ModContent.GetInstance<Rejuvena>().Version}",
                new Vector2(
                    xOffset + 10f,
                    Main.screenHeight - 28f - 2f - FontAssets.MouseText.Value.MeasureString(text).Y
                ),
                Color.Goldenrod
            );
        }
    }
}