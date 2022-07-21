using System.Collections.Generic;
using System.Linq;
using MorePersonaTraits.Extensions;
using RimWorld;
using Verse;

namespace MorePersonaTraits.Utils
{
    public static class PatcherCheckUtils
    {
        public static bool hasOnHitWorker(ThingWithComps equipment)
        {
            try
            {
                return equipment.GetComp<CompBladelinkWeapon>().TraitsListForReading
                    .Exists(trait => trait.GetModExtension<WeaponTraitOnHitExtension>().OnHitWorker != null);
            }
            catch
            {
                // Log.Warning("Equipment is not a persona weapon or does not have any persona traits with on-hit effects.");
                return false;
            }
        }

        public static List<WeaponTraitOnHitExtension> getOnHitExtensions(ThingWithComps equipment)
        {
            try
            {
                return equipment.GetComp<CompBladelinkWeapon>().TraitsListForReading
                    .FindAll(trait => trait.GetModExtension<WeaponTraitOnHitExtension>().OnHitWorker != null)
                    .Select(trait => trait.GetModExtension<WeaponTraitOnHitExtension>())
                    .ToList();
            }
            catch
            {
                Log.Error("[MorePersonaTraits]: Error while getting on-hit worker list.");
                return null;
            }
        }
    }
}