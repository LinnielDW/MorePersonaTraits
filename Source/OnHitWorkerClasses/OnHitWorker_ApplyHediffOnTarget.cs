using Verse;

namespace MorePersonaTraits.OnHitWorkerClasses
{
    public class OnHitWorker_ApplyHediffOnTarget : OnHitWorker
    {
        public override void OnHitEffect(Thing hitThing, Thing originThing)
        {
            if (hitThing != null && hitThing is Pawn hitPawn)
            {
                ApplyHediffToPawn(hitPawn, WeaponTraitOnHitExtension.TargetThingHediffDef);
            }
        }

        private void ApplyHediffToPawn(Pawn pawn, HediffDef hediffDef)
        {
            Hediff hediff = pawn.health?.hediffSet?.GetFirstHediffOfDef(hediffDef);

            if (hediff != null)
            {
                hediff.Severity += WeaponTraitOnHitExtension.ProcMagnitude;
                
                //if has duration, set duration
                //if has stacks, add stack
            }
            else
            {
                hediff = HediffMaker.MakeHediff(WeaponTraitOnHitExtension.TargetThingHediffDef, pawn);
                hediff.Severity = WeaponTraitOnHitExtension.ProcMagnitude;
                pawn.health.AddHediff(hediff);
            }
        }
    }
}