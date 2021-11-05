using Rejuvena.Common.Systems.Deprecation;
using Terraria.ID;
using TomatoLib;

namespace Rejuvena
{
    public class Rejuvena : TomatoMod
    {
        public override void PostSetupContent()
        {
            base.PostSetupContent();
        
            ItemDeprecator.Instance.Remove(ItemID.FirstFractal);
        }
    }
}