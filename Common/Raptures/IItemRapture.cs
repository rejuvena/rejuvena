#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

using Terraria;
using Terraria.ModLoader;

namespace Rejuvena.Common.Raptures
{
    /// <summary>
    ///     Item-focused rapture.
    /// </summary>
    public interface IItemRapture : IUpdateableRapture<ModItem>
    {
        void UpdateInWorld(ModItem modItem, ref float gravity, ref float maxFallSpeed);

        void UpdateInInventory(ModItem modItem, Player player);

        void UpdateWhenEquipped(ModItem modItem, Player player);

        void UpdateAsAccessory(ModItem modItem, Player player, bool hideVisual);
    }
}