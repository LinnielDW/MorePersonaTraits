using System.Collections.Generic;
using MorePersonaTraits.Utils;
using RimWorld;
using Verse;

namespace MorePersonaTraits.WorkerClasses.ItemWorkerClasses
{
    //Unused
    public class CompTargetable_SingleBladelinkTraitRemovable : CompTargetable
    {
        public override TargetingParameters GetTargetingParameters()
        {
            return new TargetingParameters
            {
                canTargetPawns = false,
                canTargetBuildings = false,
                canTargetItems = true,
                mapObjectTargetsMustBeAutoAttackable = false,
                validator = targetInfo =>
                    targetInfo.Thing.TryGetComp<CompBladelinkWeapon>() != null && TargetUtils.ValidateRequiresBond(Props, targetInfo) && !targetInfo.Thing.TryGetComp<CompBladelinkWeapon>().TraitsListForReading.NullOrEmpty() &&
                    BaseTargetValidator(targetInfo.Thing)
            };
        }

        public override IEnumerable<Thing> GetTargets(Thing targetChosenByPlayer = null)
        {
            yield return targetChosenByPlayer;
        }

        public override bool PlayerChoosesTarget => true;
    }
}