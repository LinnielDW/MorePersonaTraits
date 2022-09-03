using System;
using MorePersonaWeaponTraits.Utils;
using Verse;

namespace MorePersonaWeaponTraits.WorkerClasses.OnHitWorkerClasses
{
    public class OnHitWorker
    {
        public float ProcChance = 0f;
        public float ProcMagnitude = 0f;
        public bool TargetSelf = false;
        public bool RequiresBothLiving = false;

        //todo add basedamage to arguements so severity can be scaled by damage x magnitude
        public virtual void OnHitEffect(Thing hitThing, Thing originThing)
        {
            throw new NotImplementedException();
        }

        public void ApplyOnHitEffect(Thing hitThing, Thing originThing, Action<Thing> apply)
        {
            if (RequiresBothLiving && !(OnHitUtils.IsLivingPawn(hitThing) && OnHitUtils.IsLivingPawn(originThing)))
            {
                return;
            }

            if (TargetSelf)
            {
                if (OnHitUtils.ThingExists(originThing))
                {
                    apply(originThing);
                }
            }
            else
            {
                if (OnHitUtils.ThingExists(hitThing))
                {
                    apply(hitThing);
                }
            }
        }
    }
}