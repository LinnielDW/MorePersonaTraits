using System;
using Verse;

namespace MorePersonaWeaponTraits.WorkerClasses.OnHitWorkerClasses
{
    public class OnHitWorker_AddedDamage : OnHitWorker
    {
        public override void OnHitEffect(Thing hitThing, Thing originThing)
        {
            ApplyOnHitEffect(hitThing, originThing, ApplyExtraDamage(originThing));
        }

        private Func<Thing, Action<Thing>> ApplyExtraDamage = (bla) => (pawn) =>
        {
            //TODO: implement this
        };
    }
}