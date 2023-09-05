using MorePersonaTraits.Utils;
using RimWorld;
using Verse;

namespace MorePersonaTraits.WorkerClasses.ItemWorkerClasses;

public class CompTargetEffect_TargetedAnnul : CompTargetEffect
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

        if (compBladelink.traits.NullOrEmpty())
        {
            WeaponTraitUtils.InitializeTraits(compBladelink);
        }
        else
        {
            Messages.Message("MPT_WeaponTraitLost".Translate(target.LabelShort, targetTrait.LabelCap), target, MessageTypeDefOf.NeutralEvent);
            compBladelink.traits.Remove(targetTrait);
            if (compBladelink.traits.NullOrEmpty()) WeaponTraitUtils.InitializeTraits(compBladelink);
        }

        compBladelink.RegainBond();
    }
}