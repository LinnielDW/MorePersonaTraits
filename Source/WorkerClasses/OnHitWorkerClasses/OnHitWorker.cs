using System;
using MorePersonaWeaponTraits.Utils;
using Verse;

namespace MorePersonaWeaponTraits.WorkerClasses.OnHitWorkerClasses
{
    public abstract class OnHitWorker
    {
        public float ProcChance = 0f;
        public float ProcMagnitude = 0f;
        public bool TargetSelf = false;
        public bool RequiresBothLiving = false;
        public bool RequiresBio = false;

        //todo add basedamage to arguements so severity can be scaled by damage x magnitude
        public abstract void OnHitEffect(Thing hitThing, Thing originThing);

        public void ApplyOnHitEffect(Thing hitThing, Thing originThing, Action<Thing> apply)
        {
            if (RequiresBothLiving && !(OnHitUtils.IsLivingPawn(hitThing) && OnHitUtils.IsLivingPawn(originThing)))
            {
                return;
            }

            Thing target = TargetSelf ? originThing : hitThing;
            if (RequiresBio && !OnHitUtils.IsBiological(target))
            {
                return;
            }

            if (OnHitUtils.ThingExists(target))
            {
                apply(target);
            }
        }
    }
}