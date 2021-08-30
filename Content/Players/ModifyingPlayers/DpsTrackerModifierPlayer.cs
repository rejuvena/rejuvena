#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Terraria;
using Terraria.ModLoader.IO;

namespace Rejuvena.Content.Players.ModifyingPlayers
{
    [UsedImplicitly]
    public class DpsTrackerModifierPlayer : RejuvenaPlayer
    {
        public List<(int, uint)> DamageCollection { get; protected set; }

        public override void Initialize()
        {
            base.Initialize();

            DamageCollection = new List<(int, uint)>();
        }

        public override void Load(TagCompound tag)
        {
            base.Load(tag);

            On.Terraria.Player.getDPS += DpsHijacker;
        }

        public override void OnHitAnythingWithDamage(int damage, float knockback, bool crit)
        {
            base.OnHitAnythingWithDamage(damage, knockback, crit);

            DamageCollection.Add((damage, Main.GameUpdateCount));
        }

        protected static int DpsHijacker(On.Terraria.Player.orig_getDPS orig, Player self)
        {
            DpsTrackerModifierPlayer modifierPlayer = self.GetModPlayer<DpsTrackerModifierPlayer>();

            for (int i = 0; i < modifierPlayer.DamageCollection.Count; i++)
                if (modifierPlayer.DamageCollection[i].Item2 < Main.GameUpdateCount - 60)
                    modifierPlayer.DamageCollection.RemoveAt(i);

            return modifierPlayer.DamageCollection.Sum(x => x.Item1);
        }
    }
}