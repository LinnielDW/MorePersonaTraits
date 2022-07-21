using HarmonyLib;
using MorePersonaTraits.Utils;
using RimWorld;
using Verse;

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
                OnHitUtils.applyOnHitEffects(
                    PatcherCheckUtils.getOnHitExtensions(__instance.EquipmentSource),
                    target.Thing,
                    __instance.CasterPawn
                );
            }
        }
    }
}