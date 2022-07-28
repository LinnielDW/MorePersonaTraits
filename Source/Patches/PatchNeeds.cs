using HarmonyLib;
using MorePersonaTraits.Extensions;
using RimWorld;
using Verse;

namespace MorePersonaTraits.Patches
{
    
    //TODO: Chemical_Any is hardlinked to the DrugDesire trait. Requires a transpiler to actually go in and change these checks. 
    
    // [HarmonyPatch(typeof(Need_Chemical_Any))]
    // [HarmonyPatch("get_TraitDrugDesire")]
    // public static class PatchNeedChemicalAny
    // {
    //     static void Postfix(ref Trait __result, Pawn ___pawn)
    //     {
    //         if (__result == null && PawnHasBondedExtension(___pawn))
    //         {
    //             __result = Trait();
    //         }
    //     }
    //
    //     
    // }
}