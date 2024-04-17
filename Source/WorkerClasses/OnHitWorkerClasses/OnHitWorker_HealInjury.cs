using System.Collections.Generic;
using Verse;

namespace MorePersonaTraits.WorkerClasses.OnHitWorkerClasses
{
    public class OnHitWorker_HealInjury : OnHitWorker
    {
        public override void OnHitEffect(Thing hitThing, Thing originThing)
        {
            ApplyOnHitEffect(hitThing, originThing, HealInjury);
        }

        private void HealInjury(Thing thing)
        {
            var injuries = new List<Hediff_Injury>();
            (thing as Pawn)?.health.hediffSet.GetHediffs(ref injuries, h => h.CanHealNaturally());

            injuries?.RandomElement().Heal(ProcMagnitude * ((Pawn) thing).HealthScale);
        }
    }
}