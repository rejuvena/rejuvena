using Terraria;
using Terraria.ModLoader;

namespace Rejuvena.Content.Players
{
    /// <summary>
    ///     Base <see cref="ModPlayer"/> class for <see cref="Rejuvena"/>.
    /// </summary>
    public abstract class RejuvenaPlayer : ModPlayer
    {
        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            base.OnHitNPC(item, target, damage, knockback, crit);

            OnHitAnythingWithDamage(damage, knockback, crit);
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            base.OnHitNPCWithProj(proj, target, damage, knockback, crit);

            OnHitAnythingWithDamage(damage, knockback, crit);

        }

        public override void OnHitPvp(Item item, Player target, int damage, bool crit)
        {
            base.OnHitPvp(item, target, damage, crit);

            OnHitAnythingWithDamage(damage, null, crit);
        }

        public override void OnHitPvpWithProj(Projectile proj, Player target, int damage, bool crit)
        {
            base.OnHitPvpWithProj(proj, target, damage, crit);

            OnHitAnythingWithDamage(damage, null, crit);
        }

        public virtual void OnHitAnythingWithDamage(int damage, float? knockback, bool crit)
        {
        }
    }
}