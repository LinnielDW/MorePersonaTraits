using System.Collections.Generic;
using RimWorld;
using Verse;

namespace MorePersonaTraits.WorkerClasses.ItemWorkerClasses;

public class CompTargetable_SingleBladelinkTargetedAnnul : CompTargetable_SingleBladelink
{
    private Thing target;


    public override bool SelectedUseOption(Pawn p)
    {
        if (this.PlayerChoosesTarget)
        {
            Find.Targeter.BeginTargeting(this.GetTargetingParameters(), delegate(LocalTargetInfo t)
            {
                target = t.Thing;
                Find.WindowStack.Add(new FloatMenu(GetOptions(p)));
            }, p, null, null);
            return true;
        }

        this.target = null;
        return false;
    }

    private List<FloatMenuOption> GetOptions(Pawn p)
    {
        var list = new List<FloatMenuOption>();
        foreach (var trait in target.TryGetComp<CompBladelinkWeapon>().TraitsListForReading)
        {
            list.Add(new FloatMenuOption(trait.label, delegate
            {
                var comp = parent.TryGetComp<CompTargetEffect_BladelinkTargetedAnnul>();
                if (comp != null)
                {
                    comp.targetTrait = trait;
                    this.parent.GetComp<CompUsable>().TryStartUseJob(p, this.target, false);
                }
            }));
        }

        return list;
    }
}