using RimWorld;
using Verse;

namespace MorePersonaTraits.Utils
{
    public static class NeedFillUtil
    {
        public static void AttemptToFillNeed(this Pawn pawn, NeedDef need)
        {
            var x = pawn.needs.TryGetNeed(need);
            if (x == null)
            {
                return;
            }

            if (Rand.Value <= 0.2f)
            {
                x.CurLevel = x.MaxLevel;
            }
        }
    }
}