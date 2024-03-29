﻿using MorePersonaTraits.WorkerClasses.OnHitWorkerClasses;
using Verse;

namespace MorePersonaTraits.Templates
{
    public class OnHitWorker_Template : OnHitWorker
    {
        public override void OnHitEffect(Thing hitThing, Thing originThing)
        {
            ApplyOnHitEffect(hitThing, originThing, DebugApply);
        }

        private void DebugApply(Thing thing)
        {
            Log.Warning("OnHitWorker_Template on-hit effect has been activated on target: " + thing.Label);
        }
    }
}