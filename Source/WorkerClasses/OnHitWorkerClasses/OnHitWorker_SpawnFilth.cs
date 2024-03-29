﻿using MorePersonaTraits.Utils;
using Verse;

namespace MorePersonaTraits.WorkerClasses.OnHitWorkerClasses
{
    public class OnHitWorker_SpawnFilth : OnHitWorker
    {
        public ThingDef Filth = null;

        public override void OnHitEffect(Thing hitThing, Thing originThing)
        {
            ApplyOnHitEffect(hitThing, originThing, ApplyFilth);
        }

        private void ApplyFilth(Thing thing)
        {
            OnHitUtils.makeFilth(thing.Position, thing.Map, Filth);
        }
    }
}