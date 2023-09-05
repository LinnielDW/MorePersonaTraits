using RimWorld;
using Verse;

namespace MorePersonaTraits.WorkerClasses.ThoughtWorkerClasses
{
    public class ThoughtWorker_WeaponTraitWeather : ThoughtWorker_WeaponTrait
    {
        public override ThoughtState CurrentStateInternal(Pawn p)
        {
            if (!base.CurrentStateInternal(p).Active)
            {
                return ThoughtState.Inactive;
            }

            return
                p.equipment.bondedWeapon.TryGetComp<CompBladelinkWeapon>().TraitsListForReading.Exists(trait => trait.bondedThought == def)
                && p.Map.weatherManager.curWeather != WeatherDefOf.Clear
                    ? true
                    : ThoughtState.Inactive;
        }
    }
}