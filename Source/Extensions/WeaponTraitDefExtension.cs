using Verse;

namespace MorePersonaTraits.Extensions
{
    public class WeaponTraitDefExtension : DefModExtension
    {
        public static WeaponTraitDefExtension CreateInstance()
        {
            return new WeaponTraitDefExtension();
        }

        public bool AllowNeverBond = true;
        public int DurationSeconds = 0;
    }
}