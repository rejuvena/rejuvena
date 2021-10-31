#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

using System;
using JetBrains.Annotations;

namespace Rejuvena.Common.Utilities
{
    public static class Builder
    {
        public static T Do<T>([NotNull] T instance, [NotNull] Action action)
        {
            action();
            return instance;
        }
    }
}