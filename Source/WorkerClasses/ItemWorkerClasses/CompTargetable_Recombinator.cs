using MorePersonaTraits.Utils;
using RimWorld;
using Verse;

namespace MorePersonaTraits.WorkerClasses.ItemWorkerClasses;

public class CompTargetable_Recombinator : CompTargetable_SingleBladelink
{
    public override bool SelectedUseOption(Pawn p)
    {
        if (PlayerChoosesTarget)
        {
            Find.Targeter.BeginTargeting(
                this.GetTargetingParameters(),
                delegate(LocalTargetInfo t)
                {
                    var comp = parent.TryGetComp<CompTargetEffect_Recombinator>();
                    if (comp != null)
                    {
                        comp.DonorWeapon = t.Thing;

                        PickRecipient(p);
                    }
                },
                null,
                null,
                p,
                actionWhenFinished: FieldRefUtils.NullifyOnGuiAction,
                onGuiAction: delegate { OnGuiAction("MPT_SelectDonorWeapon"); }
            );

            return true;
        }

        return false;
    }

    private void PickRecipient(Pawn p)
    {
        Find.Targeter.BeginTargeting(
            GetTargetingParameters(),
            delegate(LocalTargetInfo t)
            {
                this.Target() = t.Thing;
                var comp = parent.TryGetComp<CompTargetEffect_Recombinator>();
                if (comp != null)
                {
                    parent.GetComp<CompUsable>().TryStartUseJob(p, t.Thing);
                }
            },
            null,
            null,
            p,
            actionWhenFinished: FieldRefUtils.NullifyOnGuiAction,
            onGuiAction: delegate { OnGuiAction(); }
        );
    }
}