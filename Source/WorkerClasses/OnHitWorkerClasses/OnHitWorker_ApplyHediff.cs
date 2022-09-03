using Verse;

namespace MorePersonaWeaponTraits.WorkerClasses.OnHitWorkerClasses
{
    public class OnHitWorker_ApplyHediff : OnHitWorker
    {
        public HediffDef HediffDef = null;

        public override void OnHitEffect(Thing hitThing, Thing originThing)
        {
            ApplyOnHitEffect(hitThing, originThing, ApplyHediffToPawn);
        }

        private void ApplyHediffToPawn(Pawn pawn)
        {
            var hediff = HediffMaker.MakeHediff(HediffDef, pawn);
            hediff.Severity = ProcMagnitude;
            pawn.health.AddHediff(hediff);
        }
    }
}