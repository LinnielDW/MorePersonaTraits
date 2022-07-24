using System.Collections.Generic;
using MorePersonaTraits.Extensions;
using Verse;

namespace MorePersonaTraits.Utils
{
    public static class OnHitUtils
    {
        public static void attemptApplyOnHitEffects(List<WeaponTraitOnHitExtension> onHitExtensions,
            Thing targetThing, Thing originThing)
        {
            float rand = Rand.Value;
            foreach (var extension in onHitExtensions)
            {
                //TODO: change prochance to be inversely proportional to attackspeed
                if (rand <= extension.ProcChance)
                {
                    // Log.Warning("Onhit has proc'ed. Executing OnHit effect");
                    extension.OnHitWorker.OnHitEffect(targetThing, originThing);
                }
                else
                {
                    // Log.Warning("Onhit proc missed. Skipping onhit effect.");
                }
            }
        }
    }
}