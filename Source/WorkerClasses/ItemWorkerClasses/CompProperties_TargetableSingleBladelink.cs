using RimWorld;

namespace MorePersonaWeaponTraits.WorkerClasses.ItemWorkerClasses
{
    public class CompProperties_TargetableSingleBladelink : CompProperties_Targetable
    {
        public CompProperties_TargetableSingleBladelink()
        {
            compClass = typeof(CompTargetable_SingleBladelink);
        }
        
        public bool RequiresBond = false;

    }
}