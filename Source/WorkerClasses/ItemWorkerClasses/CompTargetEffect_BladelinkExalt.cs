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
                existingTraits.Add(availableTraits.RandomElementByWeight(trait => trait.commonality));
            else
            {
                Log.Error("[MorePersonaTraits]: Cannot add anymore traits to this weapon. This should have been caught by the targeting class. Please let the mod author know.");
            }
        }
    }
}