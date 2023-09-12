using System.Collections.Generic;
using MorePersonaTraits.Utils;
using RimWorld;
using Verse;

namespace MorePersonaTraits.WorkerClasses.ItemWorkerClasses
{
    public class CompTargetable_SingleBladelink : CompTargetable
    {
        protected static void OnGuiAction(string tooltip = "MPT_SelectTargetWeapon")
        {
            if (Find.Targeter.IsTargeting)
            {
                GenUI.DrawMouseAttachment(TexCommand.Attack, tooltip.Translate());
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
                validator = targetInfo =>
                    ValidateRequiresBond(targetInfo) &&
                    BaseTargetValidator(targetInfo.Thing)
            };
        }

        protected bool ValidateRequiresBond(TargetInfo targetInfo)
        {
            return targetInfo.Thing.TryGetComp<CompBladelinkWeapon>() != null && 
                   TargetUtils.ValidateRequiresBond(Props,targetInfo);
        }

        public override IEnumerable<Thing> GetTargets(Thing targetChosenByPlayer = null)
        {
            yield return targetChosenByPlayer;
        }

        protected override bool PlayerChoosesTarget => true;
    }
}