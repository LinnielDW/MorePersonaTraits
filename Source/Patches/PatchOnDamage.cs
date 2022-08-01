using HarmonyLib;
using MorePersonaTraits.Utils;
using RimWorld;
using Verse;
// ReSharper disable UnusedMember.Local

namespace MorePersonaTraits.Patches
{
    [HarmonyPatch(typeof(Verb_MeleeAttackDamage))]
    [HarmonyPatch("ApplyMeleeDamageToTarget")]
    public static class PatchOnDamageMelee
    {
        static void Postfix(LocalTargetInfo target, Verb_MeleeAttackDamage __instance)
        {
            if (PatcherCheckUtils.hasOnHitWorker(__instance.EquipmentSource))
            {
                OnHitUtils.attemptApplyOnHitEffects(
                    PatcherCheckUtils.getOnHitWorkers(__instance.EquipmentSource),
                    target.Thing,
                    __instance.CasterPawn
                );
            }
        }
    }

    //TODO: While DamageInfosToApply is probably more accurate, I'm concerned that fast hitting weapons will be able to abuse this. This relates to OnHitEffect
    /*[HarmonyPatch(typeof(Verb_MeleeAttackDamage))]
    [HarmonyPatch("DamageInfosToApply")]
    public static class PatchOnDamageMelee
    {
        static void Postfix(ref IEnumerable<DamageInfo> __result, Verb __instance, LocalTargetInfo target)
        {
            if (PatcherCheckUtils.hasOnHitWorker(__instance.EquipmentSource))
                foreach (var x in __result)
                {
                    OnHitUtils.attemptApplyOnHitEffects(
                        PatcherCheckUtils.getOnHitWorkers(__instance.EquipmentSource),
                        target.Thing,
                        __instance.CasterPawn
                    );
                }
        }
    }*/
}