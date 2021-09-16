#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

using System.Collections.Generic;
using Rejuvena.Common.DataStructures;

namespace Rejuvena.Common.Raptures
{
    public interface IRapture
    {
        IEnumerable<IRapture> InheritedRaptures { get; }

        IEnumerable<ItemCollectionProfile> AssociatedItems { get; }
    }
}