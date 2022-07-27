using System.Collections.Generic;
using MorePersonaTraits.OnHitWorkerClasses;
using Verse;

namespace MorePersonaTraits.Utils
{
    public static class OnHitUtils
    {
        public static void attemptApplyOnHitEffects(List<OnHitWorker> onHitWorkers, Thing targetThing,
            Thing originThing)
        {
            float rand = Rand.Value;
            foreach (var onHitWorker in onHitWorkers)
            {
                if (rand <= onHitWorker.ProcChance)
                {
                    // Log.Warning("Onhit has proc'ed. Executing OnHit effect");
                    onHitWorker.OnHitEffect(targetThing, originThing);
                }
                else
                {
                    // Log.Warning("Onhit proc missed. Skipping onhit effect.");
                }
            }
        }
        
        
        public static bool IsLivingPawn(Thing thing)
        {
            return thing != null && !thing.Destroyed && thing is Pawn && !(thing as Pawn).Dead;
        }
    }
}