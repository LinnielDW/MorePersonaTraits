using System;
using System.Collections.Generic;
using HarmonyLib;
using MorePersonaTraits.Utils;
using RimWorld;
using Verse;

namespace MorePersonaTraits.WorkerClasses.ItemWorkerClasses
{
    public class CompTargetEffect_BladeReroller : CompTargetEffect
    {
        public override void DoEffectOn(Pawn user, Thing target)
        {
            try
            {
                CompBladelinkWeapon compBladelink = target.TryGetComp<CompBladelinkWeapon>();

                if (compBladelink == null) return;

                RerollTraits(compBladelink);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
            }
        }

        private void RerollTraits(CompBladelinkWeapon compBladelink)
        {
            List<WeaponTraitDef> traits = AccessTools
                .FieldRefAccess<List<WeaponTraitDef>>(typeof(CompBladelinkWeapon), "traits")
                .Invoke(compBladelink);

            traits.Clear();
            TraitUtils.InitializeTraits(traits, compBladelink);
        }
    }
}