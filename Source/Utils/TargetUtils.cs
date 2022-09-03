using MorePersonaWeaponTraits.WorkerClasses.ItemWorkerClasses;
using RimWorld;
using Verse;

namespace MorePersonaWeaponTraits.Utils
{
    public class TargetUtils
    {
        public static bool ValidateRequiresBond(CompProperties_Targetable props,TargetInfo targetInfo)
        {
            if (((CompProperties_TargetableSingleBladelink) props).RequiresBond)
                return targetInfo.Thing.TryGetComp<CompBladelinkWeapon>().CodedPawn != null;
            return true;
        }
    }
}