using RimWorld;
using Verse;

namespace MorePersonaTraits.Utils
{
    public static class NeedFillUtil
    {
        [TweakValue("MPT_NeedFill", 0f, 1f)]
        private static float ProcChance = 0.2f;
        
        public static void AttemptToFillNeed(this Pawn pawn, NeedDef need, float procChance = 0.0f)
        {
            var x = pawn.needs.TryGetNeed(need);
            if (x == null)
            {
                return;
            }

            var procLocal = procChance <= 0f ? ProcChance : procChance;

            if (Rand.Value <= procLocal)
            {
                x.CurLevel = x.MaxLevel;
                Messages.Message("MPT_NeedSated".Translate(pawn.LabelShortCap, need.label), pawn, MessageTypeDefOf.NeutralEvent);
            }
        }
    }
}