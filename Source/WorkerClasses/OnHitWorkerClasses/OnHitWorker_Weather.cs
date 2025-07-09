using RimWorld;
using Verse;

namespace MorePersonaTraits.WorkerClasses.OnHitWorkerClasses
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
            if (thing.Map.weatherManager.curWeatherAge < 1600 || thing.Map.weatherDecider.ForcedWeather != null) return;
            thing.Map.weatherDecider.StartNextWeather();
            Messages.Message("MPT_WeatherChangedBy".Translate(thing.LabelShort) , thing, MessageTypeDefOf.NeutralEvent, true);
        }
    }
}