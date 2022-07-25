using Verse;

namespace MorePersonaTraits.OnHitWorkerClasses
{
    public class OnHitWorker_ApplyHediff : OnHitWorker
    {
        public HediffDef HediffDef = null;

        public override void OnHitEffect(Thing hitThing, Thing originThing)
        {
            if (TargetSelf)
            {
                if (PawnValid(originThing))
                {
                    ApplyHediffToPawn(originThing as Pawn);
                }
            }
            else
            {
                if (PawnValid(hitThing))
                {
                    ApplyHediffToPawn(hitThing as Pawn);
                }
            }
        }

        private bool PawnValid(Thing thing)
        {
            return thing != null && thing is Pawn;
        }

        private void ApplyHediffToPawn(Pawn pawn)
        {
            var hediff = HediffMaker.MakeHediff(HediffDef, pawn);
            hediff.Severity = ProcMagnitude;
            pawn.health.AddHediff(hediff);
        }
    }
}