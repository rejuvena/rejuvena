using Rejuvena.Content;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace Rejuvena.Core.CoreSystems.ItemDropping
{
    public class ItemDropResolverInterceptor : ModSystem
    {
        public override void Load()
        {
            base.Load();

            On.Terraria.GameContent.ItemDropRules.ItemDropResolver.TryDropping += DropInterceptor;
        }

        public override void Unload()
        {
            base.Unload();

            On.Terraria.GameContent.ItemDropRules.ItemDropResolver.TryDropping -= DropInterceptor;
        }

        private static void DropInterceptor(On.Terraria.GameContent.ItemDropRules.ItemDropResolver.orig_TryDropping orig, ItemDropResolver self, DropAttemptInfo info)
        {
            bool callOrig = true;

            if (info.npc.ModNPC is IResolvesItemDrops dropInterceptor)
                callOrig = dropInterceptor.InterceptDropResolver(ref info);

            if (callOrig)
                orig(self, info);
        }
    }
}