using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rejuvena.Content.Components.Generic;
using Rejuvena.Content.Components.TitleLinkButtons;
using Rejuvena.Core.Services.Transformers;
using Terraria;
using Terraria.DataStructures;
using TomatoLib.Common.Utilities.Extensions;

namespace Rejuvena.Core.Services.Impl.Transformers
{
    public class TitleLinkDrawManipulator : DetourTransformerMethod
    {
        public override MethodInfo? MethodToTransform => typeof(TitleLinkButton).GetCachedMethod("Draw");

        public override MethodInfo TransformingMethod => GetType().GetCachedMethod(nameof(SaveDrawnTooltip));

        public static readonly List<string> KeysThisFrame = new();

        private static void SaveDrawnTooltip(
            TitleLinkButton self,
            SpriteBatch spriteBatch,
            Vector2 anchorPosition
        )
        {
            #region Framing Cache

            if (KeysThisFrame.Contains(self.TooltipTextKey))
            {
                KeysThisFrame.Clear();
                DrawMenuTextOverride.HoveredSocialsText = null;
            }
            
            KeysThisFrame.Add(self.TooltipTextKey);

            #endregion

            #region Drawing

            IColoredLinkButton? colorProvider = self as IColoredLinkButton;
            Color drawColor = colorProvider?.GetDrawColor() ?? Color.White;
            
            Rectangle imageFrame = self.Image.Frame();
            
            if (self.FrameWhenNotSelected.HasValue) 
                imageFrame = self.FrameWhenNotSelected.Value;
            
            Vector2 frameSize = imageFrame.Size();
            Vector2 boundingCoord = anchorPosition - frameSize / 2f;
            bool hovering = false;
            
            if (Main.MouseScreen.Between(boundingCoord, boundingCoord + frameSize))
            {
                Main.LocalPlayer.mouseInterface = true;
                hovering = true;
                // typeof(TitleLinkButton).GetCachedMethod("DrawTooltip").Invoke(self, null);
                typeof(TitleLinkButton).GetCachedMethod("TryClicking").Invoke(self, null);
            }
            
            Rectangle? framingRectangle = hovering ? self.FrameWehnSelected : self.FrameWhenNotSelected;
            Rectangle frame = self.Image.Frame();
            
            if (framingRectangle.HasValue) 
                frame = framingRectangle.Value;
            
            Texture2D texture = self.Image.Value;
            spriteBatch.Draw(texture, anchorPosition, frame, drawColor, 0f, frame.Size() / 2f, 1f, SpriteEffects.None, 0f);

            if (!hovering)
                return;
            
            DrawMenuTextOverride.HoveredSocialsText = self.TooltipTextKey;
            DrawMenuTextOverride.HoveredSocialsColor = colorProvider?.GetHoverColor() ?? Color.Yellow;

            #endregion
        }
    }
}