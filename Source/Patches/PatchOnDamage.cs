using HarmonyLib;
using MorePersonaTraits.Utils;
using RimWorld;
using Verse;

namespace MorePersonaTraits.Patches
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
                OnHitUtils.applyOnHitEffects(
                    PatcherCheckUtils.getOnHitExtensions(__instance),
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