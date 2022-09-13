using RimWorld;
using Verse;

namespace MorePersonaTraits.WorkerClasses.OnHitWorkerClasses
{
    public class OnHitWorker_ApplyThought : OnHitWorker
    {
        public ThoughtDef ThoughtDef = null;

        public override void OnHitEffect(Thing hitThing, Thing originThing)
        {
            ApplyOnHitEffect(hitThing, originThing, ApplyThoughtToPawn);
        }

        private void ApplyThoughtToPawn(Thing thing)
        {
            (thing as Pawn)?.needs?.mood?.thoughts.memories.TryGainMemory(ThoughtDef);
        }
    }
}