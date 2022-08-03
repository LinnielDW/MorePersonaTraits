using RimWorld;
using Verse;

namespace MorePersonaTraits.Utils
{
    public static class NeedFillUtil
    {
        public static void AttemptToFillNeed(this Pawn pawn, NeedDef need, float procChance = 0.2f)
        {
            var x = pawn.needs.TryGetNeed(need);
            if (x == null)
            {
                return;
            }

            if (Rand.Value <= procChance)
            {
                x.CurLevel = x.MaxLevel;
            }
        }
    }
}