using System;
using System.Collections.Generic;
using MorePersonaWeaponTraits.Utils;
using RimWorld;
using Verse;

namespace MorePersonaWeaponTraits.WorkerClasses.ItemWorkerClasses
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
            List<WeaponTraitDef> existingTraits = FieldRefUtils.TraitsFieldRef.Invoke(compBladelink);

            compBladelink.TempUnbond();
            
            existingTraits.Clear();
            compBladelink.InitializeTraits();
            
            compBladelink.RegainBond();
            
            Messages.Message("MPT_WeaponTraitsRerolled".Translate(compBladelink.parent.LabelShort), compBladelink.parent, MessageTypeDefOf.NeutralEvent);
        }
    }
}