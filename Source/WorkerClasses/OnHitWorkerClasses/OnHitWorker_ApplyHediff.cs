using Verse;

namespace MorePersonaWeaponTraits.WorkerClasses.OnHitWorkerClasses
{
    public class OnHitWorker_ApplyHediff : OnHitWorker
    {
        public HediffDef HediffDef = null;

        public override void OnHitEffect(Thing hitThing, Thing originThing)
        {
            ApplyOnHitEffect(hitThing, originThing, ApplyHediff);
        }

        private void ApplyHediff(Thing thing)
        {
            var hediff = HediffMaker.MakeHediff(HediffDef, thing as Pawn);
            hediff.Severity = ProcMagnitude;
            (thing as Pawn)?.health.AddHediff(hediff);
        }
    }
}