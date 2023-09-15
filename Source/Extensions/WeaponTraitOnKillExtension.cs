using Verse;

namespace MorePersonaTraits.Extensions
{
    public class WeaponTraitOnKillExtension : DefModExtension
    {
        
        public static WeaponTraitOnKillExtension CreateInstance()
        {
            return new WeaponTraitOnKillExtension();
        }

        //Weapon trait workers don't have access to the target while doing on-kill effects, so instead we add a hediff to the target, and have the hediff do what we want
        public HediffDef HediffDef = null;
    }
}