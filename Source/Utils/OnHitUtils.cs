using System.Collections.Generic;
using MorePersonaTraits.Extensions;
using Verse;

namespace MorePersonaTraits.Utils
{
    public static class OnHitUtils
    {
        public static void applyOnHitEffects(List<MorePersonaTraitsWeaponTraitExtension> onHitExtensions,
            Thing targetThing, Thing originThing)
        {
            foreach (var extension in onHitExtensions)
            {
                //TODO: compare rand to proc chance.
                extension.OnHitWorker.OnHitEffect(targetThing, originThing);
            }
        }
    }
}