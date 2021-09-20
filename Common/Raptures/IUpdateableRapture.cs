#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

namespace Rejuvena.Common.Raptures
{
    /// <summary>
    ///     Simple wrapper with an updatable entity attached.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IUpdateableRapture<in T> : IRapture
    {
        void Update(T updateableEntity);
    }
}