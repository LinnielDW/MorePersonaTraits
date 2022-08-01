using System.Collections.Generic;
using HarmonyLib;
using MorePersonaTraits.Utils;
using RimWorld;
using Verse;

namespace MorePersonaTraits.WorkerClasses.ItemWorkerClasses
{
    public class CompTargetEffect_BladelinkAnnul : CompTargetEffect
    {
        public override void DoEffectOn(Pawn user, Thing target)
        {
            var compBladelink = target.TryGetComp<CompBladelinkWeapon>();
            List<WeaponTraitDef> existingTraits = AccessTools
                .FieldRefAccess<List<WeaponTraitDef>>(typeof(CompBladelinkWeapon), "traits")
                .Invoke(compBladelink);


            if (compBladelink == null)
            {
                return;
            }

            if (existingTraits.NullOrEmpty())
            {
                TraitUtils.InitializeTraits(compBladelink);
                // Log.Error("[MorePersonaTraits]: No traits to remove on this weapon. This should have been caught by the targeting class. Please let the mod author know.");
            }
            else
            {
                existingTraits.RemoveAt(Rand.Range(0, existingTraits.Count - 1));
                if (existingTraits.NullOrEmpty()) TraitUtils.InitializeTraits(compBladelink);
            }
        }
    }
}