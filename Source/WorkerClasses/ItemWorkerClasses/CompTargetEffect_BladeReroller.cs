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
            InitializeTraits(traits, compBladelink);
        }

        private void InitializeTraits(List<WeaponTraitDef> existingTraits, CompBladelinkWeapon compBladelink)
        {
            if (existingTraits == null) existingTraits = new List<WeaponTraitDef>();

            //TODO: Get this from a setting
            var range = AccessTools
                .FieldRefAccess<IntRange>(typeof(CompBladelinkWeapon), "TraitsRange")
                .Invoke(compBladelink);

            for (var index = 0; index < range.RandomInRange; ++index)
            {
                var availableTraits = DefDatabase<WeaponTraitDef>.AllDefs
                    .Where(possibleTrait => CanAddTrait(existingTraits, possibleTrait))
                    .ToList();

                if (!availableTraits.NullOrEmpty())
                    existingTraits.Add(availableTraits.RandomElementByWeight(x => x.commonality));
            }
        }

        private static bool CanAddTrait(List<WeaponTraitDef> existingTraits, WeaponTraitDef traitToAdd)
        {
            if (!existingTraits.NullOrEmpty())
            {
                return !existingTraits.Exists(existingTrait => traitToAdd.Overlaps(existingTrait));
            }

            return true;
        }
    }
}