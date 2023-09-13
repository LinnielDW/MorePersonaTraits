using Verse;

namespace MorePersonaTraits.WorkerClasses.OnHitWorkerClasses
{
    public class OnHitWorker_AddedDamage : OnHitWorker
    {
        public DamageDef DamageDef = null;

        public override void OnHitEffect(Thing hitThing, Thing originThing)
        {
            ApplyOnHitEffect(hitThing, originThing, delegate(Thing thing) { ApplyExtraDamage(originThing, thing); });
        }

        private void ApplyExtraDamage(Thing originThing, Thing targetThing)
        {
            if (targetThing == null || targetThing.Destroyed || originThing is not Pawn originPawn)
            {
                return;
            }

            var originVerb = originPawn.equipment?.PrimaryEq.PrimaryVerb;
            if (originVerb == null)
            {
                return;
            }

            var damageInfo = new DamageInfo(DamageDef, ProcMagnitude, originVerb.verbProps.AdjustedArmorPenetration(originVerb, originPawn), -1f, originPawn, null, originPawn.equipment.Primary.def);
            damageInfo.SetBodyRegion(BodyPartHeight.Undefined, BodyPartDepth.Outside);
            damageInfo.SetWeaponBodyPartGroup(originVerb.verbProps.AdjustedLinkedBodyPartsGroup(originVerb.tool));
            if (originVerb.HediffCompSource != null)
            {
                damageInfo.SetWeaponHediff(originVerb.HediffCompSource.Def);
            }

            damageInfo.SetAngle((targetThing.Position - originPawn.Position).ToVector3());
            targetThing.TakeDamage(damageInfo);
        }
    }
}