using System.Collections.Generic;
using MorePersonaWeaponTraits.WorkerClasses.OnHitWorkerClasses;
using Verse;

namespace MorePersonaWeaponTraits.Extensions
{
    public class WeaponTraitOnHitExtension : DefModExtension
    {
        public static WeaponTraitOnHitExtension CreateInstance()
        {
            return new WeaponTraitOnHitExtension();
        }

        public List<OnHitWorker> OnHitWorkers = null;
    }
}