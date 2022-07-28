using System.Collections.Generic;
using HarmonyLib;
using RimWorld;
using Verse;

namespace MorePersonaTraits.Patches
{
    [HarmonyPatch(typeof(CompBladelinkWeapon))]
    [HarmonyPatch("SpecialDisplayStats")]
    public static class PatchBladelinkSpecialDisplayStats
    {
        static IEnumerable<StatDrawEntry> Postfix(IEnumerable<StatDrawEntry> values, CompBladelinkWeapon __instance)
        {
            yield return new StatDrawEntry(
                StatCategoryDefOf.BasicsNonPawn,
                "MPT_BondedTo".Translate(),
                __instance.CodedPawn != null ? __instance.CodedPawn.NameFullColored : "Nobody".Translate(),
                "MPT_BondedPawnDesc".Translate(),
                6010,
                null,
                Gen.YieldSingle(new Dialog_InfoCard.Hyperlink(__instance.CodedPawn, -1)),
                false
            );
            foreach (var value in values)
            {
                yield return value;
            }
        }
    }

    [HarmonyPatch(typeof(Pawn))]
    [HarmonyPatch("SpecialDisplayStats")]
    public static class PatchPawnSpecialDisplayStats
    {
        static IEnumerable<StatDrawEntry> Postfix(IEnumerable<StatDrawEntry> values, Pawn __instance)
        {
            if (__instance?.equipment?.bondedWeapon != null)
            {
                yield return new StatDrawEntry(
                    StatCategoryDefOf.Weapon,
                    "MPT_BondedTo".Translate(),
                    __instance.equipment.bondedWeapon.Label,
                    "MPT_BondedPawnDesc".Translate(),
                    6010,
                    null,
                    Gen.YieldSingle(new Dialog_InfoCard.Hyperlink(__instance.equipment.bondedWeapon, -1)),
                    false
                );
            }

            foreach (var value in values)
            {
                yield return value;
            }
        }
    }
}