using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

namespace MorePersonaTraits.WorkerClasses.ItemWorkerClasses
{
    public class CompTargetEffect_BladeReroller : CompTargetEffect
    {
        public override void DoEffectOn(Pawn user, Thing target)
        {
            try
            {
                CompBladelinkWeapon compBladelink = target.TryGetComp<CompBladelinkWeapon>();

                if (compBladelink == null) return;

                RerollTraits(compBladelink);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
            }
        }

        private void RerollTraits(CompBladelinkWeapon compBladelink)
        {
            List<WeaponTraitDef> traits = AccessTools
                .FieldRefAccess<List<WeaponTraitDef>>(typeof(CompBladelinkWeapon), "traits")
                .Invoke(compBladelink);

            traits.Clear();
            InitializeTraits(traits);
        }

        private void InitializeTraits(List<WeaponTraitDef> existingTraits)
        {
            if (existingTraits == null) existingTraits = new List<WeaponTraitDef>();

            //TODO: Get this from a setting
            int randomInRange = new IntRange(1, 2).RandomInRange;

            for (var index = 0; index < randomInRange; ++index)
            {
                var availableTraits = DefDatabase<WeaponTraitDef>.AllDefs
                    .Where(possibleTrait => CanAddTrait(existingTraits, possibleTrait))
                    .ToList();

                if (!availableTraits.NullOrEmpty())
                    existingTraits.Add(availableTraits.RandomElementByWeight(x => x.commonality));
            }
        }

        private bool CanAddTrait(List<WeaponTraitDef> existingTraits, WeaponTraitDef traitToAdd)
        {
            if (!existingTraits.NullOrEmpty())
            {
                return existingTraits.Exists(existingTrait => traitToAdd.Overlaps(existingTrait));
            }

            return true;
        }
    }
}