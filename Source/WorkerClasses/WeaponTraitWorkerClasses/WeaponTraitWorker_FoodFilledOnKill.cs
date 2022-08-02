using RimWorld;
using Verse;

namespace MorePersonaTraits.WorkerClasses.WeaponTraitWorkerClasses
{
    public static class NeedFillUtil
    {
        public static void FillNeed(NeedDef Need, Pawn pawn)
        {
            var x = pawn.needs.TryGetNeed(Need);
            if (x == null)
            {
                return;
            }

            x.CurLevel = x.MaxLevel;
        }
    }

    public class WeaponTraitWorker_FoodFilledOnKill : WeaponTraitWorker
    {
        public override void Notify_KilledPawn(Pawn pawn)
        {
            base.Notify_KilledPawn(pawn);
            NeedFillUtil.FillNeed(NeedDefOf.Food, pawn);
        }
    }
}