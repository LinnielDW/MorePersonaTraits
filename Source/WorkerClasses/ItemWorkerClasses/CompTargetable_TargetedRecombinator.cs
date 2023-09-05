using System.Collections.Generic;
using MorePersonaTraits.Utils;
using RimWorld;
using Verse;

namespace MorePersonaTraits.WorkerClasses.ItemWorkerClasses;

public class CompTargetable_TargetedRecombinator : CompTargetable_SingleBladelink
{
    private Thing donor;
    private WeaponTraitDef donorTrait;


    public override bool SelectedUseOption(Pawn p)
    {
        if (this.PlayerChoosesTarget)
        {
            Find.Targeter.BeginTargeting(this.GetTargetingParameters(), delegate(LocalTargetInfo t)
            {
                
                donor = t.Thing;
                var list = new List<FloatMenuOption>();
                foreach (var trait in donor.TryGetComp<CompBladelinkWeapon>().TraitsListForReading)
                {
                    list.Add(new FloatMenuOption(trait.label, delegate
                    {
                        var comp = parent.TryGetComp<CompTargetEffect_TargetedRecombinator>();
                        if (comp != null)
                        {
                            comp.donorWeapon = donor;
                            
                            donorTrait = trait;
                            comp.donorTrait = donorTrait;

                            PickRecipient(p);
                        }
                    }));
                }

                Find.WindowStack.Add(new FloatMenu(list));
            }, p, null, null);
            return true;
        }

        target = null;
        return false;
    }

    private void PickRecipient(Pawn p)
    {
        Find.Targeter.BeginTargeting(this.GetTargetingParameters(), delegate(LocalTargetInfo t)
        {
            target = t.Thing;
            var comp = parent.TryGetComp<CompTargetEffect_TargetedRecombinator>();
            if (comp != null)
            {
                if (!target.TryGetComp<CompBladelinkWeapon>().CanAddTrait(donorTrait))
                {
                    Messages.Message("MPT_WeaponCannotGainTrait".Translate(target.LabelShort, donorTrait.LabelCap), target, MessageTypeDefOf.NeutralEvent);
                    return;
                }

                this.parent.GetComp<CompUsable>().TryStartUseJob(p, this.target, false);
            }
        }, p, null, null);
    }
}