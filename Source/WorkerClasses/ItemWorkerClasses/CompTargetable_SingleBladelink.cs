using System.Collections.Generic;
using RimWorld;
using Verse;

namespace MorePersonaTraits.WorkerClasses.ItemWorkerClasses
{
    public class CompTargetable_SingleBladelink : CompTargetable
    {
        protected override TargetingParameters GetTargetingParameters()
        {
            return new TargetingParameters
            {
                canTargetPawns = false,
                canTargetBuildings = false,
                canTargetItems = true,
                mapObjectTargetsMustBeAutoAttackable = false,
                validator = x => x.Thing.TryGetComp<CompBladelinkWeapon>() != null && x.Thing.TryGetComp<CompBladelinkWeapon>().CodedPawn != null && BaseTargetValidator(x.Thing)
            };
        }

        public override IEnumerable<Thing> GetTargets(Thing targetChosenByPlayer = null)
        {
            yield return targetChosenByPlayer;
        }

        protected override bool PlayerChoosesTarget
        {
            get
            {
                return true;
            }
        }
    }
}