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
            // OnHitEffect(hitThing, originThing, delegate {  });
        }

        public virtual void OnHitEffect(Thing hitThing, Thing originThing, Action<Pawn> apply)
        {
            if (RequiresBothLiving && !(OnHitUtils.IsLivingPawn(hitThing) && OnHitUtils.IsLivingPawn(originThing)))
            {
                return;
            }

            if (TargetSelf)
            {
                if (OnHitUtils.IsLivingPawn(originThing))
                {
                    apply(originThing as Pawn);
                }
            }
            else
            {
                if (OnHitUtils.IsLivingPawn(hitThing))
                {
                    apply(hitThing as Pawn);
                }
            }
        }
    }
}