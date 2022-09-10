using RimWorld;
using Verse;

namespace MorePersonaWeaponTraits.WorkerClasses.ThoughtWorkerClasses
{
    public class ThoughtWorker_WeaponTraitGenderFemale : ThoughtWorker_WeaponTrait
    {
        protected override ThoughtState CurrentStateInternal(Pawn p)
        {
            if (!base.CurrentStateInternal(p).Active)
            {
                return ThoughtState.Inactive;
            }

            return
                p.equipment.bondedWeapon.TryGetComp<CompBladelinkWeapon>().TraitsListForReading.Exists(trait => trait.bondedThought == def)
                && p.gender == Gender.Female
                    ? true
                    : ThoughtState.Inactive;
        }
    }
}