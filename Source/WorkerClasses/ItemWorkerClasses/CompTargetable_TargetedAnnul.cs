using System.Collections.Generic;
using MorePersonaTraits.Utils;
using RimWorld;
using Verse;

namespace MorePersonaTraits.WorkerClasses.ItemWorkerClasses;

public class CompTargetable_TargetedAnnul : CompTargetable_SingleBladelink
{

    public override bool SelectedUseOption(Pawn p)
    {
        if (this.PlayerChoosesTarget)
        {
            Find.Targeter.BeginTargeting(this.GetTargetingParameters(), delegate(LocalTargetInfo t)
            {
                this.Target() = t.Thing;
                Find.WindowStack.Add(new FloatMenu(FloatMenuOptions(p, t.Thing)));
            }, p, null, null);
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
                var comp = parent.TryGetComp<CompTargetEffect_TargetedAnnul>();
                if (comp != null)
                {
                    comp.targetTrait = trait;
                    this.parent.GetComp<CompUsable>().TryStartUseJob(p, donor, false);
                }
            }));
        }

        return list;
    }
}