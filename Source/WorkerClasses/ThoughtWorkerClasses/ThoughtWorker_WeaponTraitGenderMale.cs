using RimWorld;
using Verse;

namespace MorePersonaTraits.WorkerClasses.ThoughtWorkerClasses
{
    public class ThoughtWorker_WeaponTraitGenderMale : ThoughtWorker_WeaponTrait
    {
        public override ThoughtState CurrentStateInternal(Pawn p)
        {
            if (!base.CurrentStateInternal(p).Active)
            {
                return ThoughtState.Inactive;
            }

            return
                p.equipment.bondedWeapon.TryGetComp<CompBladelinkWeapon>().TraitsListForReading.Exists(trait => trait.bondedThought == def)
                && p.gender == Gender.Male
                    ? true
                    : ThoughtState.Inactive;
        }
    }
}