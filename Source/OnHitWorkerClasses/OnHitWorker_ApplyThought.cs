using RimWorld;
using Verse;

namespace MorePersonaTraits.OnHitWorkerClasses
{
    public class OnHitWorker_ApplyThought : OnHitWorker
    {
        public ThoughtDef ThoughtDef = null;

        public override void OnHitEffect(Thing hitThing, Thing originThing)
        {
            OnHitEffect(hitThing, originThing, ApplyThoughtToPawn);
        }

        private void ApplyThoughtToPawn(Pawn pawn)
        {
            pawn.needs?.mood?.thoughts.memories.TryGainMemory(ThoughtDef);
        }
    }
}