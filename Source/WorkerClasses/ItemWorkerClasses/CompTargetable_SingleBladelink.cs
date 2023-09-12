using System;
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
            GenUI.DrawMouseAttachment(TexCommand.Attack, tooltip.Translate());
        }


        //This is basically a copy of CompTargetable.SelectedUseOption but is extended with an onGuiAction
        public override bool SelectedUseOption(Pawn p)
        {
            if (PlayerChoosesTarget)
            {
                Find.Targeter.BeginTargeting(
                    GetTargetingParameters(), delegate(LocalTargetInfo t)
                    {
                        this.Target() = t.Thing;
                        parent.GetComp<CompUsable>().TryStartUseJob(p, (LocalTargetInfo)t.Thing);
                    },
                    null,
                    null,
                    p,
                    actionWhenFinished: FieldRefUtils.NullifyOnGuiAction,
                    onGuiAction: delegate { OnGuiAction(); }
                );
                return true;
            }

            this.Target() = null;
            return false;
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
                   TargetUtils.ValidateRequiresBond(Props, targetInfo);
        }

        public override IEnumerable<Thing> GetTargets(Thing targetChosenByPlayer = null)
        {
            yield return targetChosenByPlayer;
        }

        protected override bool PlayerChoosesTarget => true;
    }
}