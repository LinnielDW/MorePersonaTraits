using MorePersonaTraits.OnHitWorkerClasses;
using Verse;

namespace MorePersonaTraits.Templates
{
    public class OnHitWorker_Template : OnHitWorker
    {
        public override void OnHitEffect(Thing hitThing, Thing originThing)
        {
            Log.Warning("OnHitWorker_Template on-hit effect has been activated.");
        }
    }
}