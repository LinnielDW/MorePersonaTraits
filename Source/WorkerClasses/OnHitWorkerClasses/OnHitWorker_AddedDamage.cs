using System;
using Verse;

namespace MorePersonaTraits.WorkerClasses.OnHitWorkerClasses
{
    public class OnHitWorker_AddedDamage : OnHitWorker
    {
        public override void OnHitEffect(Thing hitThing, Thing originThing)
        {
            ApplyOnHitEffect(hitThing, originThing, ApplyExtraDamage);
        }

        private void ApplyExtraDamage(Thing targetThing)
        {
            throw new NotImplementedException();
        } 
    }
}