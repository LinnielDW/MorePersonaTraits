using System.Collections.Generic;
using System.Linq;
using MorePersonaWeaponTraits.Extensions;
using MorePersonaWeaponTraits.WorkerClasses.OnHitWorkerClasses;
using RimWorld;
using Verse;

namespace MorePersonaWeaponTraits.Utils
{
    public static class OnHitWorkerUtils
    {
        public static bool hasOnHitWorker(ThingWithComps equipment)
        {
            try
            {
                return equipment.GetComp<CompBladelinkWeapon>().TraitsListForReading
                    .Exists(trait => trait.GetModExtension<WeaponTraitOnHitExtension>().OnHitWorkers != null);
            }
            catch
            {
                // Log.Warning("Equipment is not a persona weapon or does not have any persona traits with on-hit effects.");
                return false;
            }
        }

        public static List<OnHitWorker> getOnHitWorkers(ThingWithComps equipment)
        {
            try
            {
                return equipment.GetComp<CompBladelinkWeapon>().TraitsListForReading
                    .FindAll(trait => trait.GetModExtension<WeaponTraitOnHitExtension>()?.OnHitWorkers != null)
                    .SelectMany(trait => trait.GetModExtension<WeaponTraitOnHitExtension>().OnHitWorkers)
                    .ToList();
            }
            catch
            {
                Log.Error("[MorePersonaWeaponTraits]: Error while getting on-hit worker list.");
                return null;
            }
        }
    }
}