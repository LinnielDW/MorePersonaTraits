using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using MorePersonaTraits.Extensions;
using MorePersonaTraits.Settings;
using RimWorld;
using Verse;

namespace MorePersonaTraits.Patches;

[HarmonyPatch(typeof(ThingWithComps))]
[HarmonyPatch("Notify_Equipped")]
public static class BladeWhisperer_Notify_Equipped_Patch
{
    static void Postfix(Pawn pawn, ThingWithComps __instance)
    {
        if (pawn == null || pawn.story?.traits.HasTrait(MPT_WeaponTraitDefOf.MPT_BladeWhisperer) != true || pawn.equipment.bondedWeapon != null || !__instance.def.IsMeleeWeapon)
        {
            return;
        }

        if (__instance.def.HasComp(typeof(CompBladelinkWeapon)) || __instance.def.HasComp(typeof(CompBiocodable)) || __instance.TryGetComp<CompBladelinkWeapon>() != null)
        {
            return;
        }

        CompBladelinkWeapon thingComp = new CompBladelinkWeapon();
        try
        {
            thingComp.parent = __instance;
            InitializeSingleTrait(thingComp);
            __instance.AllComps.Add(thingComp);

            thingComp.CodeFor(pawn);
        }
        catch (Exception ex)
        {
            Log.Error("Could not instantiate or initialize a bladelink: " + ex);
            __instance.AllComps.Remove(thingComp);
        }
    }

    private static void InitializeSingleTrait(CompBladelinkWeapon thingComp)
    {
        thingComp.TraitsListForReading.Clear();
        Rand.PushState(thingComp.parent.HashOffset());
        {
            var list = DefDatabase<WeaponTraitDef>.AllDefsListForReading.Where(x => !x.neverBond);
            thingComp.TraitsListForReading.Add(list.RandomElementByWeight(x => x.commonality));
        }
        Rand.PopState();
    }
}

[HarmonyPatch(typeof(CompBladelinkWeapon))]
[HarmonyPatch("CodeFor")]
public static class CompBladelinkWeapon_CodeFor_Patch
{
    
    static MethodInfo isColonistPlayerControlledMethodInfo = AccessTools.Method(typeof(Pawn), "get_IsColonistPlayerControlled");
    static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        var codeInstructions = instructions.ToList();
        for (var i = 0; i < codeInstructions.Count; i++)
        {
            yield return codeInstructions[i];
            if (i > 0 && codeInstructions[i-1].opcode == OpCodes.Callvirt && codeInstructions[i-1].operand == isColonistPlayerControlledMethodInfo)
            {
                yield return new CodeInstruction(OpCodes.Ldarg_0);
                yield return new CodeInstruction(OpCodes.Ldarg_1);
                yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(CompBladelinkWeapon_CodeFor_Patch), "ShouldSendBoundLetter"));
                yield return codeInstructions[i];
            }
            
        }
    }

    static bool ShouldSendBoundLetter(CompBladelinkWeapon comp, Pawn pawn)
    {
        var shouldSendLetter = comp.parent.def.HasComp<CompBladelinkWeapon>() && MorePersonaTraitsSettings.showBoundLetterForBladeWhisperer;

        if (!shouldSendLetter && MorePersonaTraitsSettings.showBoundMessageInsteadForBladeWhisperer)
        {
            Messages.Message("LetterBladelinkWeaponBondedLabel".Translate(pawn.Named("PAWN"), comp.Named("WEAPON")), pawn, MessageTypeDefOf.PositiveEvent);
        }

        return shouldSendLetter;
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
        if (thingWithComps.def.HasComp(typeof(CompBladelinkWeapon)) || thingWithComps.def.HasComp(typeof(CompBiocodable)) || thingWithComps.TryGetComp<CompBladelinkWeapon>() != null)
        {
            return;
        }

        if (Scribe.EnterNode("traits"))
        {
            try
            {
                CompBladelinkWeapon thingComp = new CompBladelinkWeapon();
                thingComp.parent = thingWithComps;

                thingWithComps.AllComps.Add(thingComp);
            }
            catch (Exception ex)
            {
                Log.Error("Could not instantiate or initialize a bladelink: " + ex);
                thingWithComps.AllComps.Remove(thingWithComps.TryGetComp<CompBladelinkWeapon>());
            }
            finally
            {
                Scribe.ExitNode();
            }
        }
    }
}