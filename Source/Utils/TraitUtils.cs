using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using MorePersonaTraits.Extensions;
using RimWorld;
using Verse;

namespace MorePersonaTraits.Utils
{
    public static class TraitUtils
    {
        public static void InitializeTraits(this CompBladelinkWeapon compBladelink)
        {
            if (compBladelink.TraitsListForReading == null) FieldRefUtils.TraitsFieldRef.Invoke(compBladelink) = new List<WeaponTraitDef>();

            var range = AccessTools
                .FieldRefAccess<IntRange>(typeof(CompBladelinkWeapon), "TraitsRange")
                .Invoke(compBladelink);

            for (var index = 0; index < range.RandomInRange; ++index)
            {
                var availableTraits = compBladelink.AvailableTraits();

                if (!availableTraits.NullOrEmpty() && compBladelink.TraitsListForReading != null)
                    compBladelink.TraitsListForReading.Add(availableTraits.RandomElementByWeight(trait => trait.commonality));
            }
        }

        public static List<WeaponTraitDef> AvailableTraits(this CompBladelinkWeapon compBladelink)
        {
            return DefDatabase<WeaponTraitDef>.AllDefs
                .Where(possibleTrait => compBladelink.CanAddTrait(possibleTrait))
                .ToList();
        }

        public static bool HasAddableTrait(this CompBladelinkWeapon compBladelink)
        {
            return DefDatabase<WeaponTraitDef>.AllDefs.ToList().Exists(possibleTrait => compBladelink.CanAddTrait(possibleTrait));
        }

        private static bool CanAddTrait(this CompBladelinkWeapon compBladelinkWeapon, WeaponTraitDef traitToAdd)
        {
            if (!compBladelinkWeapon.TraitsListForReading.NullOrEmpty())
            {
                return !compBladelinkWeapon.TraitsListForReading.Exists(existingTrait => traitToAdd.Overlaps(existingTrait) || !compBladelinkWeapon.CanAddBondTrait(traitToAdd));
            }

            return true;
        }

        public static bool CanAddBondTrait(this CompBladelinkWeapon compBladelinkWeapon, WeaponTraitDef other)
        {
            if (compBladelinkWeapon.TraitsListForReading.Exists(trait => trait.GetModExtension<WeaponTraitDefExtension>() != null && trait.GetModExtension<WeaponTraitDefExtension>().AllowNeverBond == false) && other.defName == "NeverBond")
            {
                return false;
            }

            if (compBladelinkWeapon.TraitsListForReading.Exists(trait => trait.defName == "NeverBond") && other.GetModExtension<WeaponTraitDefExtension>() != null)
            {
                return other.GetModExtension<WeaponTraitDefExtension>().AllowNeverBond;
            }

            return true;
        }

        public static void TempLoseTraits(this CompBladelinkWeapon compBladelink)
        {
            if (compBladelink.CodedPawn != null && !compBladelink.TraitsListForReading.NullOrEmpty())
            {
                compBladelink.TraitsListForReading.ForEach(trait => trait.Worker.Notify_Unbonded(compBladelink.CodedPawn));
            }
        }

        public static void RegainTraits(this CompBladelinkWeapon compBladelink)
        {
            if (compBladelink.CodedPawn != null)
            {
                if (compBladelink.TraitsListForReading.Contains(MPT_WeaponTraitDefOf.NeverBond))
                {
                    compBladelink.UnCode();
                }
                else
                {
                    compBladelink.TraitsListForReading.ForEach(trait => trait.Worker.Notify_Bonded(compBladelink.CodedPawn));
                }
            }
        }
    }
}