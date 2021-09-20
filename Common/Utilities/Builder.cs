#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

using System;

namespace Rejuvena.Common.Utilities
{
    public static class Builder
    {
        public static T Do<T>(T instance, Action action)
        {
            action();
            return instance;
        }
    }
}