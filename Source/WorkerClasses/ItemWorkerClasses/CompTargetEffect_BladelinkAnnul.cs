using MorePersonaTraits.Utils;
using RimWorld;
using Verse;

namespace MorePersonaTraits.WorkerClasses.ItemWorkerClasses
{
    public class CompTargetEffect_BladelinkAnnul : CompTargetEffect
    {
        public override void DoEffectOn(Pawn user, Thing target)
        {
            var compBladelink = target.TryGetComp<CompBladelinkWeapon>();
            var existingTraits = FieldRefUtils.TraitsFieldRef.Invoke(compBladelink);


            if (compBladelink == null)
            {
                return;
            }

            if (existingTraits.NullOrEmpty())
            {
                TraitUtils.InitializeTraits(compBladelink);
                // Log.Error("[MorePersonaTraits]: No traits to remove on this weapon. This should have been caught by the targeting class. Please let the mod author know.");
            }
            else
            {
                var indexToRemove = Rand.Range(0, existingTraits.Count - 1);

                Messages.Message("MPT_WeaponTraitLost".Translate(target.LabelShort, existingTraits[indexToRemove].LabelCap), target, MessageTypeDefOf.NeutralEvent);
                existingTraits.RemoveAt(indexToRemove);
                if (existingTraits.NullOrEmpty()) TraitUtils.InitializeTraits(compBladelink);
            }
        }
    }
}