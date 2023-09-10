using System;
using System.Collections.Generic;
using MorePersonaTraits.Utils;
using RimWorld;
using Verse;

namespace MorePersonaTraits.WorkerClasses.ItemWorkerClasses
{
    public class CompTargetEffect_BladelinkChaos : CompTargetEffect
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

            compBladelink.TempUnbond();
            
            compBladelink.TraitsListForReading.Clear();
            compBladelink.InitializeTraits();
            
            compBladelink.RegainBond();
            
            Messages.Message("MPT_WeaponTraitsRerolled".Translate(compBladelink.parent.LabelShort), compBladelink.parent, MessageTypeDefOf.NeutralEvent);
        }
    }
}