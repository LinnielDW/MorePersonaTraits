using MorePersonaWeaponTraits.Utils;
using RimWorld;
using Verse;

namespace MorePersonaWeaponTraits.WorkerClasses.OnHitWorkerClasses
{
    public class OnHitWorker_SpawnFilth : OnHitWorker
    {
        public ThingDef Filth = null;

        public override void OnHitEffect(Thing hitThing, Thing originThing)
        {
            ApplyOnHitEffect(hitThing, originThing, ApplyFilthToPawn);
        }

        private void ApplyFilthToPawn(Pawn pawn)
        {
            OnHitUtils.makeFilth(pawn.Position, pawn.Map, Filth);
        }
    }
}