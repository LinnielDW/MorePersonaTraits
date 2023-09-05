using System.Collections.Generic;
using MorePersonaTraits.Utils;
using RimWorld;
using Verse;
using static MorePersonaTraits.Settings.MorePersonaTraitsSettings;

namespace MorePersonaTraits.WorkerClasses.ItemWorkerClasses
{
    public class CompTargetable_SingleBladelinkTraitAddable: CompTargetable
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
                    targetInfo.Thing.TryGetComp<CompBladelinkWeapon>() != null && 
                    TargetUtils.ValidateRequiresBond(Props,targetInfo) && 
                    targetInfo.Thing.TryGetComp<CompBladelinkWeapon>().HasAddableTrait() &&
                    targetInfo.Thing.TryGetComp<CompBladelinkWeapon>().TraitsListForReading.Count < absoluteMaxTraits && 
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