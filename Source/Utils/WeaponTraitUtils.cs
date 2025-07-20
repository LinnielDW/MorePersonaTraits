using System.Collections.Generic;
using System.Linq;
using MorePersonaTraits.Extensions;
using RimWorld;
using Verse;
using static MorePersonaTraits.Settings.MorePersonaTraitsSpawnsSettings;

namespace MorePersonaTraits.Utils
{
    public static class WeaponTraitUtils
    {
        //TODO: this should be migrated to a transpiler patch
        public static void InitializeTraits(this CompBladelinkWeapon compBladelink)
        {
            var range = FieldRefUtils.IntRangeFieldRef.Invoke(compBladelink);

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
                .Where(possibleTrait => compBladelink.CanAddTrait(possibleTrait) && WeaponTraitSpawnSettings.TryGetValue(possibleTrait.defName, true))
                .ToList();
        }

        public static bool HasAddableTrait(this CompBladelinkWeapon compBladelink)
        {
            return DefDatabase<WeaponTraitDef>.AllDefs.ToList().Exists(possibleTrait => compBladelink.CanAddTrait(possibleTrait));
        }

        public static bool CanAddTrait(this CompBladelinkWeapon compBladelinkWeapon, WeaponTraitDef traitToAdd)
        {
            if (traitToAdd.weaponCategory != WeaponCategoryDefOf.BladeLink)
            {
                return false;
            }
            if (!compBladelinkWeapon.TraitsListForReading.NullOrEmpty())
            {
                return !compBladelinkWeapon.TraitsListForReading.Exists(existingTrait => traitToAdd.Overlaps(existingTrait) || !compBladelinkWeapon.CanAddBondTrait(traitToAdd));
            }

            return true;
        }
        public static bool CanAddAnyTraitFromList(this CompBladelinkWeapon compBladelinkWeapon, List<WeaponTraitDef> traitList)
        {
            return compBladelinkWeapon.TraitsListForReading.NullOrEmpty() || traitList.Any(compBladelinkWeapon.CanAddTrait);
        }
        
        public static IEnumerable<WeaponTraitDef> AddableTraitsFromList(this CompBladelinkWeapon compBladelinkWeapon, IEnumerable<WeaponTraitDef> traitList)
        {
            return traitList.Where(compBladelinkWeapon.CanAddTrait);
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

        public static void TempUnbond(this CompBladelinkWeapon compBladelink)
        {
            if (compBladelink.CodedPawn != null && !compBladelink.TraitsListForReading.NullOrEmpty())
            {
                compBladelink.TraitsListForReading.ForEach(trait => trait.Worker.Notify_Unbonded(compBladelink.CodedPawn));
            }
        }

        public static void RegainBond(this CompBladelinkWeapon compBladelink)
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