using System.Collections.Generic;
using Rejuvena.Content.DropRules.Conditions;
using Terraria.GameContent.ItemDropRules;

namespace Rejuvena.Content.DropRules
{
    /// <summary>
    ///     <see cref="IItemDropRule"/> implementation that takes an instance of another <see cref="IItemDropRule"/> and makes it so it can never drop. <br />
    ///     This is useful for custom item spawning, as the drops will show on the bestiary without being handled.
    /// </summary>
    public class UndroppableDropRule : IItemDropRule
    {
        /// <summary>
        ///     The stored <see cref="IItemDropRule"/>.
        /// </summary>
        public IItemDropRule DropRule { get; }

        public readonly IItemDropRuleCondition UndroppableCondition = new UndroppableConditionDescription();

        /// <summary>
        ///     Creates an <see cref="IItemDropRule"/> that does not drop.
        /// </summary>
        /// <param name="dropRule">The drop rule to actually display.</param>
        public UndroppableDropRule(IItemDropRule dropRule)
        {
            DropRule = dropRule;
        }

        public bool CanDrop(DropAttemptInfo info) => false;

        public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
        {
            ratesInfo.AddCondition(UndroppableCondition);
            DropRule.ReportDroprates(drops, ratesInfo);
        }

        public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info) => DropRule.TryDroppingItem(info);

        public List<IItemDropRuleChainAttempt> ChainedRules => DropRule.ChainedRules;
    }
}