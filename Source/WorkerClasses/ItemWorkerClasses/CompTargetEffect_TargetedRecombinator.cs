using System.Collections.Generic;
using MorePersonaTraits.Utils;
using RimWorld;
using Verse;

namespace MorePersonaTraits.WorkerClasses.ItemWorkerClasses;

public class CompTargetEffect_TargetedRecombinator : CompTargetEffect
{
    public Thing DonorWeapon = null;
    public WeaponTraitDef DonorTrait = null;


    public override void DoEffectOn(Pawn user, Thing target)
    {
        if (DonorTrait == null || DonorWeapon == null)
        {
            return;
        }

        var compBladelink = target.TryGetComp<CompBladelinkWeapon>();
        if (compBladelink == null)
        {
            return;
        }

        compBladelink.TempUnbond();
        compBladelink.TraitsListForReading.Add(DonorTrait);
        DonorWeapon.Destroy();

        compBladelink.RegainBond();
        Messages.Message("MPT_WeaponTraitGained".Translate(target.LabelShort, DonorTrait.LabelCap), target, MessageTypeDefOf.NeutralEvent);
    }
}