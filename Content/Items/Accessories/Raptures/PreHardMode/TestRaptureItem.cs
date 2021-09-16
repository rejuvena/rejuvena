#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

using Rejuvena.Assets;
using Rejuvena.Content.Raptures.Tests;
using Terraria.ModLoader;

namespace Rejuvena.Content.Items.Accessories.Raptures.PreHardMode
{
#if DEBUG
    [Autoload]
#else
    [Autoload(false)]
#endif
    [FallbackAsset(FallbackAssetType.Default)]
    public class TestRaptureItem : RaptureItem<InheritanceRaptureTest>
    {
    }
}