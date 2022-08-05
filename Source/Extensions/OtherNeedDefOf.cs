using RimWorld;

namespace MorePersonaTraits.Extensions
{
    [DefOf]
    public static class OtherNeedDefOf
    {
        public static NeedDef Beauty;
        public static NeedDef Comfort;
        public static NeedDef Outdoors;
        // public static NeedDef DrugsDesire;

        static OtherNeedDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(OtherNeedDefOf));
        }
    }
    
    [DefOf]
    public static class MPT_WeaponTraitDefOf
    {
        public static WeaponTraitDef NeverBond;

        static MPT_WeaponTraitDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(MPT_WeaponTraitDefOf));
        }
    }
    
}