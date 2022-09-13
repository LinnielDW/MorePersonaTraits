using System;
using Verse;

namespace MorePersonaTraits.WorkerClasses.OnHitWorkerClasses
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
            //Workaround needed due to severity being clamped by minSeverity 
            if (ProcMagnitude < 0)
            {
                (thing as Pawn)?.health.hediffSet.GetFirstHediffOfDef(HediffDef)?.Heal(Math.Abs(ProcMagnitude));
            }
            else
            {
                var hediff = HediffMaker.MakeHediff(HediffDef, thing as Pawn);
                hediff.Severity = ProcMagnitude;
                (thing as Pawn)?.health.AddHediff(hediff);
            }

        }
    }
}