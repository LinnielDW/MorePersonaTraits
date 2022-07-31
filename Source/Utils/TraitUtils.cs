using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using MorePersonaTraits.Extensions;
using RimWorld;
using Verse;

namespace MorePersonaTraits.Utils
{
    public class TraitUtils
    {
        public static void InitializeTraits(List<WeaponTraitDef> existingTraits, CompBladelinkWeapon compBladelink)
        {
            if (compBladelink.TraitsListForReading == null) existingTraits = new List<WeaponTraitDef>();

            //TODO: Get this from a setting
            var range = AccessTools
                .FieldRefAccess<IntRange>(typeof(CompBladelinkWeapon), "TraitsRange")
                .Invoke(compBladelink);
            // var range = new IntRange(5, 7);

            for (var index = 0; index < range.RandomInRange; ++index)
            {
                var availableTraits = DefDatabase<WeaponTraitDef>.AllDefs
                    .Where(possibleTrait => CanAddTrait(existingTraits, possibleTrait, compBladelink))
                    .ToList();

                if (!availableTraits.NullOrEmpty())
                    existingTraits.Add(availableTraits.RandomElementByWeight(x => x.commonality));
            }
        }

        private static bool CanAddTrait(List<WeaponTraitDef> existingTraits, WeaponTraitDef traitToAdd, CompBladelinkWeapon compBladelinkWeapon)
        {
            if (!existingTraits.NullOrEmpty())
            {
                return !existingTraits.Exists(existingTrait => traitToAdd.Overlaps(existingTrait) || !CanAddBondTrait(traitToAdd, compBladelinkWeapon.TraitsListForReading));
            }

            return true;
        }

        public static bool CanAddBondTrait(WeaponTraitDef other, List<WeaponTraitDef> traitsListForReading)
        {
            if (traitsListForReading.Exists(trait => trait.GetModExtension<WeaponTraitDefExtension>() != null && trait.GetModExtension<WeaponTraitDefExtension>().AllowNeverBond == false) && other.defName == "NeverBond")
            {
                return false;
            }

            if (traitsListForReading.Exists(trait => trait.defName == "NeverBond") && other.GetModExtension<WeaponTraitDefExtension>() != null)
            {
                return other.GetModExtension<WeaponTraitDefExtension>().AllowNeverBond;
            }

            return true;
        }
    }
}