using System;
using RimWorld;
using Verse;

namespace MorePersonaTraits.OnHitWorkerClasses
{
    public class OnHitWorker_ApplyNeed : OnHitWorker
    {
        public NeedDef NeedDef = null;

        public override void OnHitEffect(Thing hitThing, Thing originThing)
        {
            OnHitEffect(hitThing, originThing, ApplyNeedToPawn(NeedDef, ProcMagnitude));
        }

        private Func<NeedDef, float, Action<Pawn>> ApplyNeedToPawn = (needDef, magnitude) => (pawn) =>
        {
            var need = pawn.needs.TryGetNeed(needDef);
            if (need != null)
            {
                need.CurLevel += need.MaxLevel * magnitude;
            }
        };
    }
}