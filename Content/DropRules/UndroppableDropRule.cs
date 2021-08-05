using System.Collections.Generic;
using Terraria.GameContent.ItemDropRules;
using Terraria.Localization;

namespace Rejuvena.Content.DropRules
{
    /// <summary>
    ///     <see cref="IItemDropRule"/> wrapper that takes an instance of an <see cref="IItemDropRule"/> and makes it so it can never drop. <br />
    ///     This is useful for custom item spawning.
    /// </summary>
    public class UndroppableDropRule : IItemDropRule, IItemDropRuleCondition
    {
        /// <summary>
        ///     The wrapped <see cref="IItemDropRule"/>.
        /// </summary>
        public IItemDropRule DropRule { get; }

        /// <summary>
        ///     Creates a wrapped instead of an <see cref="IItemDropRule"/> that does not drop.
        /// </summary>
        /// <param name="dropRule"></param>
        public UndroppableDropRule(IItemDropRule dropRule)
        {
            DropRule = dropRule;
        }

        public bool CanDrop(DropAttemptInfo info) => false;

        public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo) =>
            DropRule.ReportDroprates(drops, ratesInfo);

        public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info) => DropRule.TryDroppingItem(info);

        public List<IItemDropRuleChainAttempt> ChainedRules => DropRule.ChainedRules;

        public bool CanShowItemDropInUI()
        {
            if (DropRule is IItemDropRuleCondition condition)
                return condition.CanShowItemDropInUI();

            return true;
        }

        public string GetConditionDescription()
        {
            string display = "";

            if (DropRule is IItemDropRuleCondition condition)
                display += condition.GetConditionDescription();

            return display + $"\n{Language.GetTextValue("Mods.Rejuvena.DropRule.UndroppableModification")}";
        }
    }
}