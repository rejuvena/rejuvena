#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

using System;
using System.Reflection;
using Rejuvena.Content.Items;
using Terraria;
using Terraria.ModLoader;
using TomatoLib.Common.Utilities.Extensions;

namespace Rejuvena.Content.Recipes
{
    public class RaptureRecipe : GlobalRecipe
    {
        public override void OnCraft(Item item, Recipe recipe)
        {
            base.OnCraft(item, recipe);

            // Can't use pattern matching here because of C#'s strict generic type safety.
            Type modItemBase = item.ModItem?.GetType().BaseType;
            if (modItemBase == null || !typeof(RaptureItem<>).IsAssignableFrom(modItemBase.GetGenericTypeDefinition()))
                return;

            // Turn this rapture to air, create a new one on the player and initiate the spawn sequence.
            // Spawn the new item first to use data before it's tossed out.
            Item rapture = Main.item[Item.NewItem(Main.LocalPlayer.Hitbox, item.type, prefixGiven: item.prefix)];
            item.TurnToAir();
            
            Type genericRaptureType = rapture.ModItem.GetType();
            PropertyInfo spawnStatusProperty = genericRaptureType.GetCachedProperty("SpawnStatus");

            spawnStatusProperty?.SetValue(rapture.ModItem, RaptureSpawnStatus.OnCraft);
        }
    }
}