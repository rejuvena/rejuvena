using Terraria.GameContent.ItemDropRules;
using Terraria.Localization;

namespace Rejuvena.Content.DropRules.Conditions
{
    /// <summary>
    ///     Implementation of <see cref="IItemDropRuleCondition"/> that is supplied with a condition description. <br />
    ///     This description highlights the static functionality of undroppable drop rules. <br />
    /// </summary>
    /// <remarks>
    ///     This condition will not actually prevent drop rules from dropping. See <see cref="UndroppableDropRule"/> for that.
    /// </remarks>
    public class UndroppableConditionDescription :  IItemDropRuleCondition
    {
        public string GetConditionDescription() => Language.GetTextValue("Mods.Rejuvena.DropRule.UndroppableModification");

        public bool CanDrop(DropAttemptInfo info) => true;

        public bool CanShowItemDropInUI() => true;
    }
}