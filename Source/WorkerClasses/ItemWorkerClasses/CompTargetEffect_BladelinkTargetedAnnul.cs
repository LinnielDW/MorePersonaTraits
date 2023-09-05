using MorePersonaTraits.Utils;
using RimWorld;
using Verse;

namespace MorePersonaTraits.WorkerClasses.ItemWorkerClasses;

public class CompTargetEffect_BladelinkTargetedAnnul : CompTargetEffect
{
    public WeaponTraitDef targetTrait = null;

    public override void DoEffectOn(Pawn user, Thing target)
    {
        if (targetTrait == null)
        {
            return;
        }

        var compBladelink = target.TryGetComp<CompBladelinkWeapon>();
        if (compBladelink == null)
        {
            return;
        }

        compBladelink.TempUnbond();

        if (compBladelink.TraitsListForReading.NullOrEmpty())
        {
            WeaponTraitUtils.InitializeTraits(compBladelink);
        }
        else
        {
            Messages.Message("MPT_WeaponTraitLost".Translate(target.LabelShort, targetTrait.LabelCap), target, MessageTypeDefOf.NeutralEvent);
            compBladelink.TraitsListForReading.Remove(targetTrait);
            if (compBladelink.TraitsListForReading.NullOrEmpty()) WeaponTraitUtils.InitializeTraits(compBladelink);
        }

        compBladelink.RegainBond();
    }
}