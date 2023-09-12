using System.Collections.Generic;
using MorePersonaTraits.Utils;
using RimWorld;
using Verse;

namespace MorePersonaTraits.WorkerClasses.ItemWorkerClasses;

public class CompTargetable_TargetedRecombinator : CompTargetable_SingleBladelink
{
    public override bool SelectedUseOption(Pawn p)
    {
        if (this.PlayerChoosesTarget)
        {
            Find.Targeter.BeginTargeting(
                this.GetTargetingParameters(),
                delegate(LocalTargetInfo t) { Find.WindowStack.Add(new FloatMenu(FloatMenuOptions(p, t.Thing))); },
                null,
                null,
                p,
                onGuiAction: _ => OnGuiAction("MPT_SelectDonorWeapon"));

            return true;
        }

        return false;
    }

    private List<FloatMenuOption> FloatMenuOptions(Pawn p, Thing donor)
    {
        var list = new List<FloatMenuOption>();
        foreach (var trait in donor.TryGetComp<CompBladelinkWeapon>().TraitsListForReading)
        {
            list.Add(new FloatMenuOption(trait.label, delegate
            {
                var comp = parent.TryGetComp<CompTargetEffect_TargetedRecombinator>();
                if (comp != null)
                {
                    comp.DonorWeapon = donor;
                    comp.DonorTrait = trait;

                    PickRecipient(p, trait);
                }
            }));
        }

        return list;
    }

    private void PickRecipient(Pawn p, WeaponTraitDef donorTrait)
    {
        Find.Targeter.BeginTargeting(this.GetTargetingParameters(), delegate(LocalTargetInfo t)
            {
                this.Target() = t.Thing;
                var comp = parent.TryGetComp<CompTargetEffect_TargetedRecombinator>();
                if (comp != null)
                {
                    if (!t.Thing.TryGetComp<CompBladelinkWeapon>().CanAddTrait(donorTrait))
                    {
                        Messages.Message("MPT_WeaponCannotGainTrait".Translate(t.Thing.LabelShort, donorTrait.LabelCap), t.Thing, MessageTypeDefOf.NeutralEvent);
                        return;
                    }

                    this.parent.GetComp<CompUsable>().TryStartUseJob(p, t.Thing, false);
                }
            },
            null,
            null,
            p,
            onGuiAction: _ => OnGuiAction()
        );
    }
}