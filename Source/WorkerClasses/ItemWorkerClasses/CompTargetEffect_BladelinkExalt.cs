using System.Collections.Generic;
using MorePersonaTraits.Utils;
using RimWorld;
using Verse;

namespace MorePersonaTraits.WorkerClasses.ItemWorkerClasses
{
    public class CompTargetEffect_BladelinkExalt : CompTargetEffect
    {
        public override void DoEffectOn(Pawn user, Thing target)
        {
            var compBladelink = target.TryGetComp<CompBladelinkWeapon>();
            var existingTraits = target.TryGetComp<CompBladelinkWeapon>().TraitsListForReading;
            if (compBladelink == null)
            {
                return;
            }

            if (existingTraits == null)
            {
                existingTraits = new List<WeaponTraitDef>();
            }

            var availableTraits = compBladelink.AvailableTraits();

            if (!availableTraits.NullOrEmpty())
            {
                compBladelink.TempUnbond();

                var traitToAdd = availableTraits.RandomElementByWeight(trait => trait.commonality);
                existingTraits.Add(traitToAdd);

                compBladelink.RegainBond();

                Messages.Message("MPT_WeaponTraitGained".Translate(target.LabelShort, traitToAdd.LabelCap), target, MessageTypeDefOf.NeutralEvent);
            }
            else
            {
                Log.Warning("No new addable traits were found to add to this weapon. Skipping");
            }
        }
    }
}