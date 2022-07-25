using System;
using System.Collections.Generic;
using MorePersonaTraits.OnHitWorkerClasses;
using RimWorld;
using Verse;

namespace MorePersonaTraits.Extensions
{
    public class WeaponTraitOnHitExtension : DefModExtension
    {
        public List<OnHitWorker> OnHitWorkers = null;
    }
}