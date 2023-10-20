using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
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
            if (__instance.def.HasComp(typeof(CompBladelinkWeapon)) || __instance.TryGetComp<CompBladelinkWeapon>() != null)
            {
                return;
            }

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

[HarmonyPatch(typeof(ThingWithComps))]
[HarmonyPatch("Notify_Unequipped")]
public static class BladeWhisperer_Notify_Unequipped_Patch
{
    static void Postfix(Pawn pawn, ThingWithComps __instance)
    {
        if (pawn.story?.traits.HasTrait(MPT_WeaponTraitDefOf.MPT_BladeWhisperer) == true
            && !__instance.def.HasComp(typeof(CompBladelinkWeapon))
            && __instance.TryGetComp<CompBladelinkWeapon>() != null)
        {
            var bladelinkComp = __instance.GetComp<CompBladelinkWeapon>();
            bladelinkComp.UnCode();
            __instance.AllComps.Remove(bladelinkComp);
        }
    }
}

[HarmonyPatch(typeof(ThingWithComps))]
[HarmonyPatch("ExposeData")]
public static class BladeWhisperer_ExposeData_Patch
{
    static MethodInfo initializeCompsMethodInfo = AccessTools.Method(typeof(ThingWithComps), "InitializeComps");

    static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        foreach (var c in instructions)
        {
            yield return c;
            if (c.opcode == OpCodes.Call && c.operand == initializeCompsMethodInfo)
            {
                yield return new CodeInstruction(OpCodes.Ldarg_0);
                yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(BladeWhisperer_ExposeData_Patch), "AddBladelinkComp"));
            }
        }
    }

    static void AddBladelinkComp(ThingWithComps thingWithComps)
    {
        if (Scribe.EnterNode("traits"))
        {
            try
            {
                if (!thingWithComps.def.HasComp(typeof(CompBladelinkWeapon)) && thingWithComps.TryGetComp<CompBladelinkWeapon>() == null)
                {
                    CompBladelinkWeapon thingComp = new CompBladelinkWeapon();
                    thingComp.parent = thingWithComps;

                    thingWithComps.AllComps.Add(thingComp);
                }
            }
            finally
            {
                Scribe.ExitNode();
            }
        }
    }
}