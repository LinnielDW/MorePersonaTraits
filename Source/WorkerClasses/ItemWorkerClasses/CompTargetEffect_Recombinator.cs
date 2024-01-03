using System.Collections.Generic;
using MorePersonaTraits.Utils;
using RimWorld;
using Verse;

namespace MorePersonaTraits.WorkerClasses.ItemWorkerClasses;

public class CompTargetEffect_Recombinator : CompTargetEffect
{
    public Thing DonorWeapon = null;


    public override void DoEffectOn(Pawn user, Thing target)
    {
        if (DonorWeapon == null)
        {
            return;
        }

        var compBladelink = target.TryGetComp<CompBladelinkWeapon>();
        if (compBladelink == null)
        {
            return;
        }

        compBladelink.TempUnbond();

        var traitPool = new List<WeaponTraitDef>();
        traitPool.AddRange(DonorWeapon.TryGetComp<CompBladelinkWeapon>().TraitsListForReading);
        traitPool.AddRange(compBladelink.TraitsListForReading);

        compBladelink.TraitsListForReading.Clear();
        for (var i = 0; i < FieldRefUtils.IntRangeFieldRef.Invoke().RandomInRange; i++)
        {
            if (compBladelink.CanAddAnyTraitFromList(traitPool))
            {
                compBladelink.TraitsListForReading.Add(compBladelink.AddableTraitsFromList(traitPool).RandomElementByWeight(trait => trait.commonality));
            }
            else
            {
                if (compBladelink.TraitsListForReading.Count < FieldRefUtils.IntRangeFieldRef.Invoke().min)
                {
                    var availableTraits = compBladelink.AvailableTraits();
                    if (!availableTraits.NullOrEmpty())
                    {
                        var traitToAdd = availableTraits.RandomElementByWeight(trait => trait.commonality);
                        compBladelink.TraitsListForReading.Add(traitToAdd);
                    }
                    else
                    {
                        Log.Warning("No new addable traits were found to add to this weapon. Skipping");
                    }
                }
            }
        }

        DonorWeapon.Destroy();

        compBladelink.RegainBond();
        Messages.Message("MPT_WeaponTraitsRecombinated".Translate(target.LabelShort), target, MessageTypeDefOf.NeutralEvent);
    }
}