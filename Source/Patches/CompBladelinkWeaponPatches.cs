using HarmonyLib;
using MorePersonaWeaponTraits.Utils;
using RimWorld;
using Verse;
// ReSharper disable UnusedMember.Local

namespace MorePersonaWeaponTraits.Patches
{
    [HarmonyPatch(typeof(CompBladelinkWeapon))]
    [HarmonyPatch("CanAddTrait")]
    public static class CompBladelinkWeaponPatches
    {
        static void Postfix(WeaponTraitDef trait, ref bool __result, CompBladelinkWeapon __instance)
        {
            __result = __result && __instance.CanAddBondTrait(trait);
        }
    }

    [HarmonyPatch(typeof(CompBladelinkWeapon))]
    [HarmonyPatch(MethodType.Constructor)]
    public static class CompBladelinkWeaponTraitRangePatch
    {
        static void Postfix()
        {
            //TODO get this from a setting
            Traverse.Create<CompBladelinkWeapon>().Field("TraitsRange").SetValue(new IntRange(1,5));
        }
    }
}