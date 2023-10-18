using RimWorld;
using Verse;

namespace MorePersonaTraits.Extensions
{
    [DefOf]
    public static class MPT_NeedDefOf
    {
        public static NeedDef Beauty;
        public static NeedDef Comfort;
        public static NeedDef Outdoors;
        // public static NeedDef DrugsDesire;

        static MPT_NeedDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(MPT_NeedDefOf));
        }
    }
    
    [DefOf]
    public static class MPT_WeaponTraitDefOf
    {
        public static WeaponTraitDef NeverBond;
        public static TraitDef MPT_BladeWhisperer;

        static MPT_WeaponTraitDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(MPT_WeaponTraitDefOf));
        }
    }

    [DefOf]
    public static class MPT_InvisibilityHediffDefOf
    {
        public static HediffDef PsychicInvisibility;

        static MPT_InvisibilityHediffDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(MPT_InvisibilityHediffDefOf));
        }
    }
}