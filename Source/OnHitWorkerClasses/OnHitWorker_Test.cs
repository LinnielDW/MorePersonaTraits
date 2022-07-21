using Verse;

namespace MorePersonaTraits.OnHitWorkerClasses
{
    public class OnHitWorker_Test : OnHitWorker
    {
        public override void OnHitEffect(Thing hitThing, Thing originThing)
        {
            Log.Warning("OnHitWorker_Test successfully gotten into");
        }
    }
}