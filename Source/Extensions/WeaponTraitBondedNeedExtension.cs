using MorePersonaTraits.WorkerClasses.BondedWorkerClasses;
using Verse;

namespace MorePersonaTraits.Extensions
{
    public class WeaponTraitBondedNeedExtension : DefModExtension
    {
        public static WeaponTraitBondedNeedExtension CreateInstance()
        {
            return new WeaponTraitBondedNeedExtension();
        }

        public BondedWorker BondedWorker = null;
    }
}