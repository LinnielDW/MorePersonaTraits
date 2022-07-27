using System;
using System.Collections.Generic;
using MorePersonaTraits.OnHitWorkerClasses;
using RimWorld;
using Verse;

namespace MorePersonaTraits.Extensions
{
    public class WeaponTraitBondedNeedExtension : DefModExtension
    {
        public static WeaponTraitBondedNeedExtension CreateInstance()
        {
            return new WeaponTraitBondedNeedExtension();
        }

        // public List<OnHitWorker> OnHitWorkers = null;
    }
}