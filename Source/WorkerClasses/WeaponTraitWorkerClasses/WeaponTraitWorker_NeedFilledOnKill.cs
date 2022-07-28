using RimWorld;
using Verse;

namespace MorePersonaTraits.WeaponTraitWorkerClasses
{
    public class WeaponTraitWorker_NeedFilledOnKill : WeaponTraitWorker
    {
        //TODO: implement
        public override void Notify_KilledPawn(Pawn pawn)
        {
            base.Notify_KilledPawn(pawn);
            // pawn.needs.TryGetNeed()
            // if (psychicEntropy == null)
            // {
            //     return;
            // }
            // psychicEntropy.OffsetPsyfocusDirectly(0.2f);
        }
    }
}