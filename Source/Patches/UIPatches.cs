using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            if (!__instance.TraitsListForReading.NullOrEmpty())
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("Stat_Thing_PersonaWeaponTrait_Desc".Translate());
                stringBuilder.AppendLine();
                for (int i = 0; i < __instance.TraitsListForReading.Count; i++)
                {
                    stringBuilder.AppendLine(__instance.TraitsListForReading[i].LabelCap + ": " +
                                             __instance.TraitsListForReading[i].description);
                    if (i < __instance.TraitsListForReading.Count - 1)
                    {
                        stringBuilder.AppendLine();
                    }
                }

                yield return new StatDrawEntry(
                    __instance.parent.def.IsMeleeWeapon
                        ? StatCategoryDefOf.Weapon_Melee
                        : StatCategoryDefOf.Weapon_Ranged,
                    "Stat_Thing_PersonaWeaponTrait_Label".Translate(),
                    (from x in __instance.TraitsListForReading select x.label).ToCommaList(false, false)
                    .CapitalizeFirst(),
                    stringBuilder.ToString(),
                    1104,
                    null,
                    from traitDef in __instance.TraitsListForReading select new Dialog_InfoCard.Hyperlink(traitDef),
                    false
                );
            }

            foreach (var value in values)
            {
                if (value.LabelCap != "Stat_Thing_PersonaWeaponTrait_Label".Translate().CapitalizeFirst())
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