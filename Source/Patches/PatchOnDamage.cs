using HarmonyLib;
using MorePersonaWeaponTraits.Utils;
using RimWorld;
using Verse;
// ReSharper disable UnusedMember.Local

namespace MorePersonaWeaponTraits.Patches
{
    //Implementation directly inspired by alattalatta's bullet patch implementation in Infusion 2. 
    //Not sure if will use it, but will keep it around for now.
    [HarmonyPatch(typeof(Bullet))]
    [HarmonyPatch("Impact")]
    public static class PatchOnDamageBullet
    {
        static void Postfix(Thing hitThing, Bullet __instance)
        {
            //TODO: check if this works on proj weapons
            var primary = ((Pawn) __instance.Launcher).equipment.Primary;
            if (PatcherCheckUtils.hasOnHitWorker(primary))
            {
                OnHitUtils.attemptApplyOnHitEffects(
                    PatcherCheckUtils.getOnHitWorkers(__instance),
                    hitThing,
                    __instance.Launcher
                );
            }
        }
    }

    [HarmonyPatch(typeof(Verb_MeleeAttackDamage))]
    [HarmonyPatch("ApplyMeleeDamageToTarget")]
    public static class PatchOnDamageMelee
    {
        static void Postfix(LocalTargetInfo target, Verb_MeleeAttackDamage __instance)
        {
            if (OnHitWorkerUtils.hasOnHitWorker(__instance.EquipmentSource))
            {
                OnHitUtils.attemptApplyOnHitEffects(
                    OnHitWorkerUtils.getOnHitWorkers(__instance.EquipmentSource),
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
            if (OnHitWorkerUtils.hasOnHitWorker(__instance.EquipmentSource))
                foreach (var x in __result)
                {
                    OnHitUtils.attemptApplyOnHitEffects(
                        OnHitWorkerUtils.getOnHitWorkers(__instance.EquipmentSource),
                        target.Thing,
                        __instance.CasterPawn
                    );
                }
        }
    }*/
}