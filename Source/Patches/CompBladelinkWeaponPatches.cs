﻿using HarmonyLib;
using MorePersonaTraits.Utils;
using RimWorld;
using Verse;
using static MorePersonaTraits.Settings.MorePersonaTraitsSettings;

// ReSharper disable UnusedMember.Local

namespace MorePersonaTraits.Patches
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
            Traverse.Create<CompBladelinkWeapon>().Field("TraitsRange").SetValue(new IntRange(minTraits, maxTraits));
        }
    }
    
    //TODO: make defs disableable
}