#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

using System.Collections.Generic;
using TomatoLib.Common.Utilities.ItemCollections;

namespace Rejuvena.Common.Raptures
{
    /// <summary>
    ///     Bare-bones rapture.
    /// </summary>
    public interface IRapture
    {
        /// <summary>
        ///     All raptures this rapture inherits the effects of.
        /// </summary>
        IEnumerable<IRapture> InheritedRaptures { get; }

        /// <summary>
        ///     All items associated with this rapture.
        /// </summary>
        IEnumerable<ItemCollectionProfile> AssociatedItems { get; }
    }
}