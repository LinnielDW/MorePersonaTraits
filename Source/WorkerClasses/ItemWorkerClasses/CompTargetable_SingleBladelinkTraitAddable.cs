using MorePersonaTraits.Utils;
using RimWorld;
using Verse;
using static MorePersonaTraits.Settings.MorePersonaTraitsSettings;

namespace MorePersonaTraits.WorkerClasses.ItemWorkerClasses
{
    public class CompTargetable_SingleBladelinkTraitAddable: CompTargetable_SingleBladelink
    {
        protected override TargetingParameters GetTargetingParameters()
        {
            return new TargetingParameters
            {
                canTargetPawns = false,
                canTargetBuildings = false,
                canTargetItems = true,
                mapObjectTargetsMustBeAutoAttackable = false,
                validator = targetInfo =>
                    ValidateRequiresBond(targetInfo) &&
                    targetInfo.Thing.TryGetComp<CompBladelinkWeapon>().HasAddableTrait() &&
                    targetInfo.Thing.TryGetComp<CompBladelinkWeapon>().TraitsListForReading.Count < absoluteMaxTraits && 
                    BaseTargetValidator(targetInfo.Thing)
            };
        }
    }
}