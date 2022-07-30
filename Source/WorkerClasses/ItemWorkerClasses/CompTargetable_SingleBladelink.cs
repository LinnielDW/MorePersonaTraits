using System.Collections.Generic;
using RimWorld;
using Verse;

namespace MorePersonaTraits.WorkerClasses.ItemWorkerClasses
{
    public class CompTargetable_SingleBladelink : CompTargetable
    {
        public CompProperties_TargetableSingleBladelink Props
        {
            get
            {
                return (CompProperties_TargetableSingleBladelink) props;
            }
        }
        protected override TargetingParameters GetTargetingParameters()
        {
            return new TargetingParameters
            {
                canTargetPawns = false,
                canTargetBuildings = false,
                canTargetItems = true,
                mapObjectTargetsMustBeAutoAttackable = false,
                validator = x => x.Thing.TryGetComp<CompBladelinkWeapon>() != null && ValidateBondRequires(x) && BaseTargetValidator(x.Thing)
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

        private bool ValidateBondRequires(TargetInfo x)
        {
            if (Props.RequiresBond) return x.Thing.TryGetComp<CompBladelinkWeapon>().CodedPawn != null;
            return true;
        }
    }
}