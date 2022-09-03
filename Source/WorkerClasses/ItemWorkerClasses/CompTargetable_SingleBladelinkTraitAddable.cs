﻿using System.Collections.Generic;
using MorePersonaWeaponTraits.Utils;
using RimWorld;
using Verse;

namespace MorePersonaWeaponTraits.WorkerClasses.ItemWorkerClasses
{
    public class CompTargetable_SingleBladelinkTraitAddable: CompTargetable
    {
        protected override TargetingParameters GetTargetingParameters()
        {
            return new TargetingParameters
            {
                canTargetPawns = false,
                canTargetBuildings = false,
                canTargetItems = true,
                mapObjectTargetsMustBeAutoAttackable = false,
                validator = targetInfo =>
                    targetInfo.Thing.TryGetComp<CompBladelinkWeapon>() != null && TargetUtils.ValidateRequiresBond(Props,targetInfo) && targetInfo.Thing.TryGetComp<CompBladelinkWeapon>().HasAddableTrait() &&
                    BaseTargetValidator(targetInfo.Thing)
            };
        }

        public override IEnumerable<Thing> GetTargets(Thing targetChosenByPlayer = null)
        {
            yield return targetChosenByPlayer;
        }

        protected override bool PlayerChoosesTarget => true;


    }
}