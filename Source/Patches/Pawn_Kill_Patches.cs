using System.Linq;
using HarmonyLib;
using MorePersonaTraits.Extensions;
using RimWorld;
using Verse;

namespace MorePersonaTraits.Patches;

[HarmonyPatch(typeof(Pawn))]
[HarmonyPatch("Kill")]
public static class Pawn_Kill_Patches
{
    static void Prefix(Pawn __instance, DamageInfo? dinfo)
    {
        if (!__instance.Spawned || __instance.Destroyed || __instance.Dead)
        {
            //Log.Warning("Already dead, skipping");
            return;
        }

        var primary = (dinfo?.Instigator as Pawn)?.equipment.Primary;
        if (primary == null || dinfo.Value.Weapon != primary.def)
        {
            return;
        }

        foreach (var hediffDef in primary.TryGetComp<CompBladelinkWeapon>().TraitsListForReading.Select(trait => trait.GetModExtension<WeaponTraitOnKillExtension>()?.HediffDef).Where(hediffDef => hediffDef != null))
        {
            var hediff = HediffMaker.MakeHediff(hediffDef, __instance);
            __instance.health?.hediffSet.AddDirect(hediff);
        }
    }
}