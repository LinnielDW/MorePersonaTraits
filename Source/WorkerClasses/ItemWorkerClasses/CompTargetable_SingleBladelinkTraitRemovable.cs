using RimWorld;
using Verse;

namespace MorePersonaTraits.WorkerClasses.ItemWorkerClasses
{
    //Unused
    public class CompTargetable_SingleBladelinkTraitRemovable : CompTargetable_SingleBladelink
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
                    !targetInfo.Thing.TryGetComp<CompBladelinkWeapon>().TraitsListForReading.NullOrEmpty() &&
                    BaseTargetValidator(targetInfo.Thing)
            };
        }
    }
}