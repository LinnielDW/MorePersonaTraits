using System;
using HarmonyLib;
using MorePersonaTraits.Extensions;
using RimWorld;
using Verse;

namespace MorePersonaTraits.Patches;

[HarmonyPatch(typeof(ThingWithComps))]
[HarmonyPatch("Notify_Equipped")]
public static class BladeWhisperer_Notify_Equipped_Patch
{
    static void Postfix(Pawn pawn, ThingWithComps __instance)
    {
        if (pawn.story.traits.HasTrait(MPT_WeaponTraitDefOf.MPT_BladeWhisperer) && pawn is { equipment.bondedWeapon: null } && !__instance.def.IsRangedWeapon)
        {
            if (__instance.def.HasComp(typeof(CompBladelinkWeapon)))
            {
                return;
            }

            if (__instance.GetComp<CompBladelinkWeapon>() == null)
            {
                CompBladelinkWeapon thingComp = new CompBladelinkWeapon();
                try
                {
                    thingComp.parent = __instance;
                    AccessTools.Method("CompBladelinkWeapon:InitializeTraits").Invoke(thingComp, null);
                    __instance.AllComps.Add(thingComp);
                    thingComp.CodeFor(pawn);
                }
                catch (Exception ex)
                {
                    Log.Error("Could not instantiate or initialize a bladelink: " + ex);
                    __instance.AllComps.Remove(thingComp);
                }
            }
        }
    }
}

[HarmonyPatch(typeof(ThingWithComps))]
[HarmonyPatch("Notify_Unequipped")]
public static class BladeWhisperer_Notify_Unequipped_Patch
{
    static void Postfix(Pawn pawn, ThingWithComps __instance)
    {
        if (pawn != null
            && pawn.story.traits.HasTrait(MPT_WeaponTraitDefOf.MPT_BladeWhisperer)
            && !__instance.def.HasComp(typeof(CompBladelinkWeapon))
            && __instance.GetComp<CompBladelinkWeapon>() != null)
        {
            var bladelinkComp = __instance.GetComp<CompBladelinkWeapon>();
            bladelinkComp.UnCode();
            __instance.AllComps.Remove(bladelinkComp);
        }
    }
}

[HarmonyPatch(typeof(ThingWithComps))]
[HarmonyPatch("InitializeComps")]
public static class BladeWhisperer_InitializeComps_Patch
{
    static void Postfix(ThingWithComps __instance)
    {
        if (Scribe.EnterNode("traits"))
        {
            try
            {
                if (!__instance.def.HasComp(typeof(CompBladelinkWeapon)) && __instance.TryGetComp<CompBladelinkWeapon>() == null)
                {
                    CompBladelinkWeapon thingComp = new CompBladelinkWeapon();
                    thingComp.parent = __instance;

                    __instance.AllComps.Add(thingComp);
                }
            }
            finally
            {
                Scribe.ExitNode();
            }
        }
    }
}