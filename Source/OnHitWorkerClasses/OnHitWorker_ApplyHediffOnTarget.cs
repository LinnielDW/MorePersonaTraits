using Verse;

namespace MorePersonaTraits.OnHitWorkerClasses
{
    public class OnHitWorker_ApplyHediffOnTarget : OnHitWorker
    {
        public override void OnHitEffect(Thing hitThing, Thing originThing)
        {
            if (hitThing != null && hitThing is Pawn hitPawn)
            {
                var hediffOnPawn = hitPawn.health?.hediffSet?.GetFirstHediffOfDef(WeaponTraitOnHitExtension.TargetThingHediff);

                if (hediffOnPawn != null)
                {
                    hediffOnPawn.Severity += WeaponTraitOnHitExtension.ProcMagnitude;
                }
                else
                {
                    var hediff = HediffMaker.MakeHediff(WeaponTraitOnHitExtension.TargetThingHediff, hitPawn);
                    hediff.Severity = WeaponTraitOnHitExtension.ProcMagnitude;
                    hitPawn.health.AddHediff(hediff);
                }
            }
        }
    }
}