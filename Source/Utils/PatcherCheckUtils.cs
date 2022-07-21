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
                    .Exists(trait =>
                        trait.GetModExtension<MorePersonaTraitsWeaponTraitExtension>().OnHitWorker != null);
            }
            catch
            {
                // Log.Warning("Persona Weapon does not have any traits with on-hit effects.");
                return false;
            }
        }

        public static List<MorePersonaTraitsWeaponTraitExtension> getOnHitExtensions(ThingWithComps equipment)
        {
            try
            {
                return equipment.GetComp<CompBladelinkWeapon>().TraitsListForReading
                    .FindAll(
                        trait => trait.GetModExtension<MorePersonaTraitsWeaponTraitExtension>().OnHitWorker != null)
                    .Select(trait => trait.GetModExtension<MorePersonaTraitsWeaponTraitExtension>())
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