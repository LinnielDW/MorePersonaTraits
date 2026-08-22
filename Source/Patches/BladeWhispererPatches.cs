using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using MorePersonaTraits.Extensions;
using MorePersonaTraits.Settings;
using MorePersonaTraits.Utils;
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

        // CompUniqueWeapon is excluded because it scribes a "traits" node of its own: a
        // coexisting bladelink comp would save a second sibling node with the same name,
        // and both comps would read back the first one (issue #16).
        if (__instance.def.HasComp(typeof(CompBladelinkWeapon)) || __instance.def.HasComp(typeof(CompBiocodable)) || __instance.def.HasComp(typeof(CompUniqueWeapon)) || __instance.TryGetComp<CompBladelinkWeapon>() != null)
        {
            return;
        }

        CompBladelinkWeapon thingComp = new CompBladelinkWeapon();
        try
        {
            thingComp.parent = __instance;
            // Runtime-created comps never get props assigned (InitializeComps only does that
            // for def-borne comps), and vanilla CompBiocodable.Notify_Equipped dereferences
            // Props on every equip - a props-less comp NREs and makes the weapon
            // permanently unequippable (issue #16). Mirror the vanilla persona weapon defs.
            thingComp.props = new CompProperties_BladelinkWeapon { biocodeOnEquip = true };
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
            var list = thingComp.AvailableTraits().Where(trait => !trait.neverBond);
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
        if (thingWithComps.def.HasComp(typeof(CompBladelinkWeapon)) || thingWithComps.def.HasComp(typeof(CompBiocodable)) || thingWithComps.def.HasComp(typeof(CompUniqueWeapon)) || thingWithComps.TryGetComp<CompBladelinkWeapon>() != null)
        {
            return;
        }

        // A saved whispered bond is always biocoded (CodeFor runs on equip, and the comp is
        // removed on unequip), and Scribe_Values only writes "biocoded" when it is true - so
        // this node identifies exactly our own saved comp. The previous check,
        // EnterNode("traits"), false-positived on any other comp that scribes a list named
        // "traits" - e.g. Odyssey's CompUniqueWeapon - grafting a broken bladelink comp onto
        // every unique weapon on load (issue #16).
        if (Scribe.EnterNode("biocoded"))
        {
            try
            {
                CompBladelinkWeapon thingComp = new CompBladelinkWeapon();
                thingComp.parent = thingWithComps;
                thingComp.props = new CompProperties_BladelinkWeapon { biocodeOnEquip = true };

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