using RimWorld;
using Verse;

namespace MorePersonaTraits.WorkerClasses.OnHitWorkerClasses;

public class OnHitWorker_ApplyGeneResource : OnHitWorker
{
    public GeneDef GeneDef = null;
    public override void OnHitEffect(Thing hitThing, Thing originThing)
    {
        ApplyOnHitEffect(hitThing, originThing, ApplyGeneResourceToPawn);
    }

    private void ApplyGeneResourceToPawn(Thing pawn)
    {
        var gene = (Gene_Resource)(pawn as Pawn)?.genes.GetGene(GeneDef);
        if (gene != null)
        {
            gene.Value += gene.Max * ProcMagnitude;
        }
    } 
}