using MorePersonaWeaponTraits.WorkerClasses.OnHitWorkerClasses;
using Verse;

namespace MorePersonaWeaponTraits.Templates
{
    public class OnHitWorker_Template : OnHitWorker
    {
        public override void OnHitEffect(Thing hitThing, Thing originThing)
        {
            OnHitEffect(hitThing, originThing, DebugApply);
        }

        private void DebugApply(Pawn thing)
        {
            Log.Warning("OnHitWorker_Template on-hit effect has been activated on target: " + thing.Name);
        }
    }
}