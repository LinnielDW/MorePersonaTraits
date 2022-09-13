using System;
using Verse;

namespace MorePersonaTraits.WorkerClasses.OnHitWorkerClasses
{
    public class OnHitWorker_ApplyStun : OnHitWorker
    {
        public float StunDuration = 2f;

        public override void OnHitEffect(Thing hitThing, Thing originThing)
        {
            ApplyOnHitEffect(hitThing, originThing, ApplyStun(originThing));
        }

        private Action<Thing> ApplyStun(Thing originThing)
        {
            return p => (p as Pawn)?.stances.stunner.StunFor(StunDuration.SecondsToTicks(), originThing);
        }
    }
}