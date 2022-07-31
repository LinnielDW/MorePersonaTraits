using HarmonyLib;
using MorePersonaTraits.Utils;
using RimWorld;

namespace MorePersonaTraits.Patches
{
    [HarmonyPatch(typeof(CompBladelinkWeapon))]
    [HarmonyPatch("CanAddTrait")]
    public static class CompBladelinkWeaponPatches
    {
        static void Postfix(WeaponTraitDef trait, ref bool __result, CompBladelinkWeapon __instance)
        {
            __result = __result && TraitUtils.CanAddBondTrait(trait, __instance.TraitsListForReading);
        }
    }
}