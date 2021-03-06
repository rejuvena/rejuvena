#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

namespace Rejuvena.Common.Utilities
{
    public static class ArrayUtilities
    {
        public static T[] AsArray<T>(this T item) => new[] {item};
    }
}