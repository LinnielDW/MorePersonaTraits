﻿using System;
using System.Collections.Generic;
using MorePersonaTraits.Utils;
using RimWorld;
using Verse;

namespace MorePersonaTraits.WorkerClasses.ItemWorkerClasses
{
    public class CompTargetEffect_TraitReroller : CompTargetEffect
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
            List<WeaponTraitDef> existingTraits = FieldRefUtils.TraitsFieldRef.Invoke(compBladelink);

            existingTraits.Clear();
            compBladelink.InitializeTraits();
        }
    }
}