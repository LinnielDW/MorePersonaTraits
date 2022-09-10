using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
using MorePersonaWeaponTraits.Extensions;
using MorePersonaWeaponTraits.WorkerClasses.OnHitWorkerClasses;
using RimWorld;
using Verse;

// ReSharper disable UnusedMember.Local

namespace MorePersonaWeaponTraits.Patches
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
                Gen.YieldSingle(new Dialog_InfoCard.Hyperlink(__instance.CodedPawn))
            );

            foreach (var statDrawEntry in HyperlinkedTraitStatDrawEntries(__instance)) yield return statDrawEntry;

            foreach (var existingEntry in values)
            {
                if (existingEntry.LabelCap != "Stat_Thing_PersonaWeaponTrait_Label".Translate().CapitalizeFirst())
                    yield return existingEntry;
            }
        }

        private static IEnumerable<StatDrawEntry> HyperlinkedTraitStatDrawEntries(CompBladelinkWeapon __instance)
        {
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
                    (from trait in __instance.TraitsListForReading select trait.label).ToCommaList()
                    .CapitalizeFirst(),
                    stringBuilder.ToString(),
                    1104,
                    null,
                    from traitDef in __instance.TraitsListForReading select new Dialog_InfoCard.Hyperlink(traitDef)
                );
            }
        }
    }

    [HarmonyPatch(typeof(Def))]
    [HarmonyPatch("SpecialDisplayStats")]
    public static class PatchWeaponTraitDefSpecialDisplayStats
    {
        static IEnumerable<StatDrawEntry> Postfix(IEnumerable<StatDrawEntry> values, Def __instance)
        {
            var statEntries = new List<StatDrawEntry>();
            if (__instance is WeaponTraitDef traitDef)
            {
                if(traitDef.equippedStatOffsets != null)
                {
                    foreach (var equippedOffset in traitDef.equippedStatOffsets)
                    {
                        statEntries.Add(
                            new StatDrawEntry(
                                StatCategoryDefOf.EquippedStatOffsets,
                                equippedOffset.stat.label,
                                equippedOffset.ValueToStringAsOffset,
                                equippedOffset.stat.description,
                                6010
                            )
                        );
                    }
                }

                if (traitDef.GetModExtension<WeaponTraitOnHitExtension>() != null && traitDef.GetModExtension<WeaponTraitOnHitExtension>().OnHitWorkers != null)
                {
                    foreach (var worker in traitDef.GetModExtension<WeaponTraitOnHitExtension>().OnHitWorkers)
                    {
                        statEntries.Add(new StatDrawEntry(
                            MPT_StatCategoryDefOf.MPT_OnHitEffects,
                            workerLabel(worker),
                            ReportText(worker),
                            ReportText(worker),
                            6010
                        ));
                    }
                }

                // if (traitDef.exclusionTags != null)
                // {
                //     statEntries.Add(new StatDrawEntry(
                //         StatCategoryDefOf.Weapon,
                //         "ExclusionTags",
                //         traitDef.exclusionTags.ToCommaList(),
                //         "List of exclusion tags:\n" + traitDef.exclusionTags.ToCommaList(),
                //         6010
                //     ));
                // }

                if (traitDef.bondedHediffs != null)
                {
                    foreach (var hediff in traitDef.bondedHediffs)
                    {
                        statEntries.AddRange(hediff.SpecialDisplayStats(StatRequest.ForEmpty()));
                    }
                }
            }

            statEntries.AddRange(values);
            return statEntries;
        }

        private static TaggedString ReportText(OnHitWorker worker)
        {
            return "MPT_OnHitDesc".Translate(
                worker.ProcChance.ToStringPercent(),
                workerEffect(worker),
                worker.TargetSelf ? "MPT_TargetSelf".Translate() : "MPT_TargetTarget".Translate(),
                Math.Abs(worker.ProcMagnitude) > 0f ? "MPT_MagnitudeDesc".Translate(worker.ProcMagnitude.ToStringPercent()) : TaggedString.Empty
            );
        }

        private static string workerEffect(OnHitWorker worker)
        {
            switch(worker)
            {
                case OnHitWorker_ApplyHediff onHit:
                    return "MPT_ApplyHediffDesc".Translate(onHit.HediffDef.LabelCap);
                case OnHitWorker_ApplyNeed onHit:
                    return onHit.ProcMagnitude > 0f ? "MPT_NeedOffsetTypeUpDesc".Translate(onHit.NeedDef.LabelCap) : "MPT_NeedOffsetTypeDownDesc".Translate(onHit.NeedDef.LabelCap);
                case OnHitWorker_ApplyThought onHit:
                    return "MPT_GiveThoughDesc".Translate(onHit.ThoughtDef.Label, onHit.ThoughtDef.stackLimit > 1 ? "MPT_ThoughtStackDesc".Translate(onHit.ThoughtDef.stackLimit) : TaggedString.Empty);
                default:
                    return "";
            }
        }

        private static string workerLabel(OnHitWorker worker)
        {
            switch (worker)
            {
                case OnHitWorker_ApplyHediff onHit:
                    return onHit.HediffDef.label;
                case OnHitWorker_ApplyNeed onHit:
                    return onHit.NeedDef.label;
                case OnHitWorker_ApplyThought onHit:
                    return onHit.ThoughtDef.Label;
                default:
                    return "";
            }
        }
    }


    [HarmonyPatch(typeof(Pawn))]
    [HarmonyPatch("SpecialDisplayStats")]
    public static class PatchPawnSpecialDisplayStats
    {
        static IEnumerable<StatDrawEntry> Postfix(IEnumerable<StatDrawEntry> values, Pawn __instance)
        {
            if (__instance.equipment?.bondedWeapon != null)
            {
                yield return new StatDrawEntry(
                    StatCategoryDefOf.Weapon,
                    "MPT_BondedTo".Translate(),
                    __instance.equipment.bondedWeapon.Label,
                    "MPT_BondedPawnDesc".Translate(),
                    6010,
                    null,
                    Gen.YieldSingle(new Dialog_InfoCard.Hyperlink(__instance.equipment.bondedWeapon))
                );
            }

            foreach (var value in values)
            {
                yield return value;
            }
        }
    }
}