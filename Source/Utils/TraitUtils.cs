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
        public static void InitializeTraits(CompBladelinkWeapon compBladelink)
        {
            if (compBladelink.TraitsListForReading == null) compBladelink.TraitsListForReading?.Clear();

            //TODO: Get this from a setting
            var range = AccessTools
                .FieldRefAccess<IntRange>(typeof(CompBladelinkWeapon), "TraitsRange")
                .Invoke(compBladelink);
            // var range = new IntRange(5, 7);

            for (var index = 0; index < range.RandomInRange; ++index)
            {
                var availableTraits = AvailableTraits(compBladelink);

                if (!availableTraits.NullOrEmpty() && compBladelink.TraitsListForReading != null)
                    compBladelink.TraitsListForReading.Add(availableTraits.RandomElementByWeight(trait => trait.commonality));
            }
        }

        public static List<WeaponTraitDef> AvailableTraits(CompBladelinkWeapon compBladelink)
        {
            return DefDatabase<WeaponTraitDef>.AllDefs
                .Where(possibleTrait => CanAddTrait(possibleTrait, compBladelink))
                .ToList();
        }

        public static bool HasAddableTrait(CompBladelinkWeapon compBladelink)
        {
            return DefDatabase<WeaponTraitDef>.AllDefs.ToList().Exists(possibleTrait => CanAddTrait(possibleTrait, compBladelink));
        }

        private static bool CanAddTrait(WeaponTraitDef traitToAdd, CompBladelinkWeapon compBladelinkWeapon)
        {
            if (!compBladelinkWeapon.TraitsListForReading.NullOrEmpty())
            {
                return !compBladelinkWeapon.TraitsListForReading.Exists(existingTrait => traitToAdd.Overlaps(existingTrait) || !CanAddBondTrait(traitToAdd, compBladelinkWeapon.TraitsListForReading));
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