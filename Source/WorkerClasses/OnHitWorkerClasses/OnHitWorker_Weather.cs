using Verse;

namespace MorePersonaWeaponTraits.WorkerClasses.OnHitWorkerClasses
{
    public class OnHitWorker_Weather : OnHitWorker
    {
        public override void OnHitEffect(Thing hitThing, Thing originThing)
        {
            ApplyOnHitEffect(hitThing, originThing, MakeWeather);
        }

        //TODO: move this to a util
        private void MakeWeather(Thing thing)
        {
            thing.Map.weatherDecider.StartNextWeather();
        }
    }
}