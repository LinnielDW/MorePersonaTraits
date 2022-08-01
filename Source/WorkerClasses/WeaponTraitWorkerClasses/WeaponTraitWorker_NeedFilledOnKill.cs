using RimWorld;
using Verse;

namespace MorePersonaTraits.WorkerClasses.WeaponTraitWorkerClasses
{
    public class WeaponTraitWorker_NeedFilledOnKill : WeaponTraitWorker
    {
        //TODO: test
        public NeedDef NeedDef = null;

        public override void Notify_KilledPawn(Pawn pawn)
        {
            base.Notify_KilledPawn(pawn);
            var x = pawn.needs.TryGetNeed(NeedDef);
            if (x == null)
            {
                return;
            }

            x.CurLevel = x.MaxLevel;
        }
    }
}