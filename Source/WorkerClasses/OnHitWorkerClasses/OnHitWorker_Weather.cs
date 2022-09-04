using Verse;

namespace MorePersonaWeaponTraits.WorkerClasses.OnHitWorkerClasses
{
    public class OnHitWorker_Weather : OnHitWorker
    {
        public override void OnHitEffect(Thing hitThing, Thing originThing)
        {
            ApplyOnHitEffect(hitThing, originThing, TryMakeWeather);
        }

        //TODO: move this to a util
        private void TryMakeWeather(Thing thing)
        {
            if (thing.Map.weatherManager.curWeatherAge < 1600) return;
            thing.Map.weatherDecider.StartNextWeather();
        }
    }
}