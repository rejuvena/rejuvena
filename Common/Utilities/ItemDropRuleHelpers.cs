using Rejuvena.Content.DropRules;
using Terraria.GameContent.ItemDropRules;

namespace Rejuvena.Common.Utilities
{
    /// <summary>
    ///     Provides numerous methods to aid in dealing with <see cref="IItemDropRule"/>-related fiascoes.
    /// </summary>
    public static class ItemDropRuleHelpers
    {
        /// <summary>
        ///     Returns the <paramref name="dropRule"/> instance wrapped in a newly-created <see cref="UndroppableDropRule"/> instance.
        /// </summary>
        /// <param name="dropRule">The original drop rule, which is displayed.</param>
        /// <returns>An <see cref="UndroppableDropRule"/> instance that displays the same information as the original <see cref="IItemDropRule"/> instance.</returns>
        /// <remarks>
        ///     This allows for you to add drop rules to visually display in the bestiary while handling drops manually elsewhere.
        /// </remarks>
        public static UndroppableDropRule AsUndroppable(this IItemDropRule dropRule) => new(dropRule);
    }
}