using MorePersonaTraits.Extensions;
using Verse;

namespace MorePersonaTraits.OnHitWorkerClasses
{
    public class OnHitWorker
    {
        public WeaponTraitOnHitExtension WeaponTraitOnHitExtension;

        public virtual void OnHitEffect(Thing hitThing, Thing originThing)
        {
        }
    }
}