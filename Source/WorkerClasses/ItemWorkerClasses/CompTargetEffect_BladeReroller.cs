using System;
using System.Collections.Generic;
using System.Reflection;
using RimWorld;
using Verse;

namespace MorePersonaTraits.WorkerClasses.ItemWorkerClasses
{
    public class CompTargetEffect_BladeReroller : CompTargetEffect
    {
        public override void DoEffectOn(Pawn user, Thing target)
        {
            var compBladelink = target.TryGetComp<CompBladelinkWeapon>();
            if (compBladelink == null)
            {
                return;
            }

            compBladelink.TraitsListForReading.Clear();
            target.TryGetComp<CompBladelinkWeapon>().PostPostMake();
        }
    }
}