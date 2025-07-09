using System.Linq;
using HarmonyLib;
using MorePersonaTraits.Extensions;
using RimWorld;
using Verse;

namespace MorePersonaTraits.Patches;

//TODO: refactor this to just spawn the items from here without using HediffComp_OnDeathSpawn. I don't know why I chose to do it like this.

[HarmonyPatch(typeof(Pawn))]
[HarmonyPatch("Kill")]
public static class Pawn_Kill_Patches
{
    static void Prefix(Pawn __instance, DamageInfo? dinfo)
    {
        if (!__instance.Spawned || __instance.Destroyed || __instance.Dead || dinfo == null)
        {
            //Log.Warning("Already dead, skipping");
            return;
        }

        var primary = (dinfo.Value.Instigator as Pawn)?.equipment?.Primary;
        if (primary?.TryGetComp<CompBladelinkWeapon>() == null || dinfo.Value.Weapon != primary.def)
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