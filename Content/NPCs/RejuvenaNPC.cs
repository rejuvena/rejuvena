﻿using System;
using Microsoft.Xna.Framework.Graphics;
using Rejuvena.Assets;
using ReLogic.Content;
using Terraria.ModLoader;

namespace Rejuvena.Content.NPCs
{
    /// <summary>
    ///     Abstract base class shared between all <see cref="Rejuvena"/> NPCs.
    /// </summary>
    public abstract class RejuvenaNPC : ModNPC, IModContent
    {
        public override string Texture
        {
            get
            {
                if (ModContent.RequestIfExists<Texture2D>(base.Texture, out _, AssetRequestMode.ImmediateLoad))
                {
                    Mod.Logger.Debug($"[Loading] Texture exists: {base.Texture}, no fallback texture needed.");
                    return base.Texture;
                }

                try
                {
                    return GetFallbackAsset().Path;
                }
                catch (Exception e)
                {
                    // don't care enough about the stack trace
                    switch (e)
                    {
                        case NullReferenceException:
                            throw new NullReferenceException($"[Loading] {e.Message}, normal asset: {base.Texture}");

                        case NotImplementedException:
                            throw new NotImplementedException($"[Loading] {e.Message}, normal asset: {base.Texture}");
                    }

                    throw;
                }
            }
        }

        public FallbackAsset GetFallbackAsset() => 
            throw new NotImplementedException("There are no fallback NPC assets.");
    }
}