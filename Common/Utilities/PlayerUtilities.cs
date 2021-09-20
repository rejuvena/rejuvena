#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

using Terraria;

namespace Rejuvena.Common.Utilities
{
    public static class PlayerUtilities
    {
        /// <summary>
        ///     Cancel the usage of an item currently in use.
        /// </summary>
        public static void CancelItemUsage(this Player player) =>
            player.itemAnimation = player.itemTime = player.reuseDelay = 0;
    }
}