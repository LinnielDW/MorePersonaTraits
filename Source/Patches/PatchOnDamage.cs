using HarmonyLib;
using MorePersonaTraits.Utils;
using RimWorld;
using Verse;

namespace MorePersonaTraits.Patches
{
    // Implementation for proj impact patch originally inspired by alattalatta's bullet patch implementation from Infusion 2,
    // but has undergone refactoring over the years.
    [HarmonyPatch(typeof(Projectile))]
    [HarmonyPatch("Impact")]
    public static class PatchOnDamageBullet
    {
        static void Postfix(Thing hitThing, Projectile __instance)
        {
            var primary = (__instance.Launcher as Pawn)?.equipment?.Primary;
            //if projectile has no source OR projectile is not from the equipped weapon
            if (primary == null || primary.def != __instance.EquipmentDef)
            {
                return;
            }

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
            if (__instance.EquipmentSource == null) return;

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
}
