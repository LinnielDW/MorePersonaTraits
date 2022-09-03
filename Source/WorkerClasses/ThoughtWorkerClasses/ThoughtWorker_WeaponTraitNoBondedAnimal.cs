﻿using RimWorld;
using Verse;

namespace MorePersonaWeaponTraits.WorkerClasses.ThoughtWorkerClasses
{
    public class ThoughtWorker_WeaponTraitNoBondedAnimal : ThoughtWorker_WeaponTrait
    {
        protected override ThoughtState CurrentStateInternal(Pawn p)
        {
            if (!base.CurrentStateInternal(p).Active)
            {
                return ThoughtState.Inactive;
            }

            return
                p.equipment.bondedWeapon.TryGetComp<CompBladelinkWeapon>().TraitsListForReading.Exists(trait => trait.bondedThought == def)
                && !p.relations.DirectRelations.Exists(relation => relation.def == PawnRelationDefOf.Bond && relation.otherPawn.playerSettings.RespectedMaster == p)
                    ? true
                    : ThoughtState.Inactive;
        }
        
    }
}