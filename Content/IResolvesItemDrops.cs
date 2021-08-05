using Terraria.GameContent.ItemDropRules;

namespace Rejuvena.Content
{
    public interface IResolvesItemDrops
    {
        /// <summary>
        ///     Intercept the item drop resolver. Return false to prevent the cycle from running further.
        /// </summary>
        /// <param name="info">Drop attempt info, including game modes and the NPC.</param>
        /// <returns>True or false, depending on if the drop is cancelled.</returns>
        bool InterceptDropResolver(ref DropAttemptInfo info);
    }
}