using System;
using RimWorld;
using Verse;

namespace MorePersonaWeaponTraits.WorkerClasses.OnHitWorkerClasses
{
    public class OnHitWorker_ApplyNeed : OnHitWorker
    {
        public NeedDef NeedDef = null;

        public override void OnHitEffect(Thing hitThing, Thing originThing)
        {
            ApplyOnHitEffect(hitThing, originThing, ApplyNeedToPawn);
        }

        private void ApplyNeedToPawn(Thing pawn)
        {
            var need = (pawn as Pawn)?.needs?.TryGetNeed(NeedDef);
            if (need != null)
            {
                need.CurLevel += need.MaxLevel * ProcMagnitude;
            }
        } 
    }
}