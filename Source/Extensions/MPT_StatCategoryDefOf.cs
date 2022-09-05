using RimWorld;

namespace MorePersonaWeaponTraits.Extensions
{
    [DefOf]
    public static class MPT_StatCategoryDefOf
    {
        static MPT_StatCategoryDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(MPT_StatCategoryDefOf));
        }

        public static StatCategoryDef MPT_OnHitEffects;
    }
}