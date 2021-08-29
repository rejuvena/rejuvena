using System.Collections.Generic;
using Rejuvena.Assets;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Rejuvena.Content.NPCs
{
    /// <summary>
    ///     Abstract base class shared between all <see cref="Rejuvena"/> NPCs.
    /// </summary>
    public abstract class RejuvenaNPC : ModNPC
    {
        public override string Texture => FallbackAsset.GetFallbackAsset(GetType(), base.Texture).Path;

        public virtual NPCID.Sets.NPCBestiaryDrawModifiers BestiaryDrawModifiers
        {
            get => NPCID.Sets.NPCBestiaryDrawOffset[Type];

            set => NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
        }

        public virtual IEnumerable<IItemDropRule> DropData { get; protected set; }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            base.ModifyNPCLoot(npcLoot);

            if (DropData is null) 
                return;

            foreach (IItemDropRule dropRule in DropData)
                npcLoot.Add(dropRule);
        }
    }
}