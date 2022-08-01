using System.Collections.Generic;
using MorePersonaTraits.Utils;
using RimWorld;
using Verse;

namespace MorePersonaTraits.WorkerClasses.ItemWorkerClasses
{
    public class CompTargetable_SingleBladelinkTraitAddable: CompTargetable
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
                    targetInfo.Thing.TryGetComp<CompBladelinkWeapon>() != null && TargetUtils.ValidateRequiresBond(Props,targetInfo) && TraitUtils.AvailableTraits(targetInfo.Thing.TryGetComp<CompBladelinkWeapon>()).Count > 0 &&
                    BaseTargetValidator(targetInfo.Thing)
            };
        }

        public override IEnumerable<Thing> GetTargets(Thing targetChosenByPlayer = null)
        {
            yield return targetChosenByPlayer;
        }

        protected override bool PlayerChoosesTarget => true;


    }
}