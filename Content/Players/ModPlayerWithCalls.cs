#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

using Terraria.ModLoader;
using TomatoLib;
using TomatoLib.Core.Interfaces.Compatibility.Calls;

namespace Rejuvena.Content.Players
{
    /// <summary>
    ///     <see cref="RejuvenaPlayer"/> that can handle <see cref="Mod.Call"/><c>s</c>.
    /// </summary>
    public abstract class ModPlayerWithCalls : RejuvenaPlayer, ICallHandler
    {
        public abstract string Accessor { get; }

        public abstract object Action(Mod mod, params object[] args);

        public override void Load()
        {
            base.Load();

            if (Mod is not TomatoMod tomatoMod)
                return;

            tomatoMod.CallHandler.AddCaller(this);
        }
    }
}