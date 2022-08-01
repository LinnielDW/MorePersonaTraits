using RimWorld;
using Verse;

namespace MorePersonaTraits.WorkerClasses.ItemWorkerClasses
{
    public class CompTargetEffect_BladeDelinker : CompTargetEffect
    {
        public override void DoEffectOn(Pawn user, Thing target)
        {
            var compBladelink = target.TryGetComp<CompBladelinkWeapon>();
            if (compBladelink == null)
            {
                return;
            }

            compBladelink.UnCode();
        }
    }
}