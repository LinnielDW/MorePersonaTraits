using MorePersonaTraits.Extensions;
using RimWorld;
using Verse;

namespace MorePersonaTraits.OnHitWorkerClasses
{
    public class OnHitWorker
    {
        public float ProcChance = 0f;
        public float ProcMagnitude = 0f;
        public bool TargetSelf = false;
        
        //todo add basedamage to arguements
        public virtual void OnHitEffect(Thing hitThing, Thing originThing)
        {
        }
    }
}