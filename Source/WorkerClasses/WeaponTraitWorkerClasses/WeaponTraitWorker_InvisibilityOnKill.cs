using MorePersonaTraits.Extensions;
using RimWorld;
using Verse;

namespace MorePersonaTraits.WorkerClasses.WeaponTraitWorkerClasses
{
    public class WeaponTraitWorker_InvisibilityOnKill : WeaponTraitWorker
    {
        public override void Notify_KilledPawn(Pawn pawn)
        {
            base.Notify_KilledPawn(pawn);
            pawn.health.AddHediff(MPT_InvisibilityHediffDefOf.PsychicInvisibility);
            pawn.health.hediffSet.GetFirstHediffOfDef(MPT_InvisibilityHediffDefOf.PsychicInvisibility)
                .TryGetComp<HediffComp_Disappears>().ticksToDisappear = 60 * def.GetModExtension<WeaponTraitDefExtension>().DurationSeconds;
        }
    }
}