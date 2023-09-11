using System.Collections.Generic;
using RimWorld;
using Verse;

namespace MorePersonaTraits.WorkerClasses.OnHitWorkerClasses;

public class OnHitWorker_ApplyTeleport : OnHitWorker
{
    public FloatRange RandomRange;
    public IntRange stunTicks;
    private List<IntVec3> cells = new List<IntVec3>();

    public override void OnHitEffect(Thing hitThing, Thing originThing)
    {
        ApplyOnHitEffect(hitThing, originThing, delegate(Thing targetThing) { ApplyTeleportToPawn(originThing, targetThing); });
    }

    private void ApplyTeleportToPawn(Thing originThing, Thing targetThing)
    {
        cells.Clear();
        foreach (var intVec2 in GenRadial.RadialCellsAround(targetThing.Position, RandomRange.RandomInRange, true))
        {
            if (intVec2.Standable(targetThing.Map) && GenSight.LineOfSight(targetThing.Position, intVec2, targetThing.Map))
            {
                cells.Add(intVec2);
            }
        }

        var targetPawn = targetThing as Pawn;
        if (cells.Any() && targetPawn != null)
        {
            EffecterDefOf.Skip_EntryNoDelay.Spawn(targetPawn, targetPawn.Map, 1f);
            targetPawn.Position = cells.RandomElement();

            targetPawn.stances.stunner.StunFor(stunTicks.RandomInRange, originThing, false, false);
            targetPawn.Notify_Teleported(true, true);
            CompAbilityEffect_Teleport.SendSkipUsedSignal(targetPawn.Position, targetPawn);

            EffecterDefOf.Skip_Exit.Spawn(targetPawn, targetPawn.Map, 1f);
        }
    }
}