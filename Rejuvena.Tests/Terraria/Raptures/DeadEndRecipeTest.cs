#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Rejuvena.Common.DataStructures;
using Rejuvena.Common.Raptures;
using Terraria.ID;

namespace Rejuvena.Tests.Terraria.Raptures
{
    public static class DeadEndRecipeTest
    {
        public static List<int> BlacklistedItems { get; } = new();

        public static Dictionary<int, bool> ResolvedItems { get; } = new();

        /// <summary>
        ///     Check for recipes not yet associated with a rapture.
        /// </summary>
        [Test]
        public static void CheckForDeadEndRecipes()
        {
            for (int i = 0; i < ItemID.Count; i++)
                ResolvedItems[i] = BlacklistedItems.Contains(i);

            foreach (Type type in typeof(Rejuvena).Assembly.GetTypes())
            {
                if (!typeof(IRapture).IsAssignableFrom(type)) 
                    continue;

                if (type.IsAbstract)
                {
                    Console.WriteLine($"Found abstract or interface rapture of type: {type.FullName}");
                    continue;
                }

                if (type.GetConstructor(Array.Empty<Type>()) is null)
                {
                    Console.WriteLine($"Found rapture type requiring custom construction: {type.FullName}");
                    continue;
                }

                Console.WriteLine($"Found rapture of type: {type.FullName}");

                IRapture rapture = (IRapture) Activator.CreateInstance(type);

                if (rapture is null)
                {
                    Console.WriteLine($"Unable to activate rapture of type: {type.FullName}");
                    continue;
                }

                foreach (ItemCollectionProfile profile in rapture.AssociatedItems)
                foreach (int item in profile.ItemData.Select(x => x.Item1))
                    ResolvedItems[item] = true;
            }

            for (int i = 0; i < ItemID.Count; i++)
                Console.WriteLine($"Item \"{ItemID.Search.GetName(i)}\" ({i}) in rapture: {ResolvedItems[i]}");
        }
    }
}