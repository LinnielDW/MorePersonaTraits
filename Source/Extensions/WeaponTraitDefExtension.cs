using Verse;

namespace MorePersonaWeaponTraits.Extensions
{
    public class WeaponTraitDefExtension : DefModExtension
    {
        public static WeaponTraitDefExtension CreateInstance()
        {
            return new WeaponTraitDefExtension();
        }

        public bool AllowNeverBond = true;
    }
}