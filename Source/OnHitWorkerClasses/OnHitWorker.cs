using MorePersonaTraits.Extensions;
using Verse;

namespace MorePersonaTraits.OnHitWorkerClasses
{
    public class OnHitWorker
    {
        public MorePersonaTraitsWeaponTraitExtension WeaponTraitOnHitExtension;

        public virtual void OnHitEffect(Thing hitThing, Thing originThing)
        {
        }
    }
}