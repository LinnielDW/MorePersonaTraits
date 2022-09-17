using HarmonyLib;
using MorePersonaTraits.Utils;
using RimWorld;
using Verse;
// ReSharper disable UnusedMember.Local

namespace MorePersonaTraits.Patches
{
    // Implementation directly inspired by alattalatta's bullet patch implementation from Infusion 2. 
    // I found adding a comp to every bullet to be a little overkill and didn't want to put that kind of strain on people's games.
    [HarmonyPatch(typeof(Bullet))]
    [HarmonyPatch("Impact")]
    public static class PatchOnDamageBullet
    {
        static void Postfix(Thing hitThing, Bullet __instance)
        {
            var primary = (__instance.Launcher as Pawn)?.equipment?.Primary;
            if (OnHitWorkerUtils.hasOnHitWorker(primary))
            {
                OnHitUtils.attemptApplyOnHitEffects(
                    OnHitWorkerUtils.getOnHitWorkers(primary),
                    hitThing,
                    __instance.Launcher,
                    true
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