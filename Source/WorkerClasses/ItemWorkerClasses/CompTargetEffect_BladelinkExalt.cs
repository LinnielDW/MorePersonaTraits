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

            var availableTraits = TraitUtils.AvailableTraits(compBladelink);

            if (!availableTraits.NullOrEmpty())
                existingTraits.Add(availableTraits.RandomElementByWeight(x => x.commonality));
            else
            {
                //TODO: give alert that no more traits can be added and don't consume the item
            }
        }
    }
}