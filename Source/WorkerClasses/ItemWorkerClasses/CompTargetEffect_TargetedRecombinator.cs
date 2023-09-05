using System.Collections.Generic;
using MorePersonaTraits.Utils;
using RimWorld;
using Verse;

namespace MorePersonaTraits.WorkerClasses.ItemWorkerClasses;

public class CompTargetEffect_TargetedRecombinator : CompTargetEffect
{
    public Thing donorWeapon = null;
    public WeaponTraitDef donorTrait = null;


    public override void DoEffectOn(Pawn user, Thing target)
    {
        if (donorTrait == null || donorWeapon == null)
        {
            return;
        }

        var compBladelink = target.TryGetComp<CompBladelinkWeapon>();

        if (compBladelink == null)
        {
            return;
        }

        if (compBladelink.traits == null)
        {
            compBladelink.traits = new List<WeaponTraitDef>();
        }


        compBladelink.TempUnbond();

        compBladelink.traits.Add(donorTrait);

        donorWeapon.Destroy();

        compBladelink.RegainBond();
        Messages.Message("MPT_WeaponTraitGained".Translate(target.LabelShort, donorTrait.LabelCap), target, MessageTypeDefOf.NeutralEvent);
    }
}