using System.Linq;
using MorePersonaTraits.Extensions;
using MorePersonaTraits.Utils;
using RimWorld;
using Verse;

namespace MorePersonaTraits.WorkerClasses.WeaponTraitWorkerClasses
{
    public class WeaponTraitWorker_FoodFilledOnKill : WeaponTraitWorker
    {
        public override void Notify_KilledPawn(Pawn pawn)
        {
            base.Notify_KilledPawn(pawn);
            pawn.AttemptToFillNeed(NeedDefOf.Food);
        }
    }

    public class WeaponTraitWorker_RestFilledOnKill : WeaponTraitWorker
    {
        public override void Notify_KilledPawn(Pawn pawn)
        {
            base.Notify_KilledPawn(pawn);
            pawn.AttemptToFillNeed(NeedDefOf.Rest);
        }
    }

    public class WeaponTraitWorker_JoyFilledOnKill : WeaponTraitWorker
    {
        public override void Notify_KilledPawn(Pawn pawn)
        {
            base.Notify_KilledPawn(pawn);
            pawn.AttemptToFillNeed(NeedDefOf.Joy);
        }
    }

    public class WeaponTraitWorker_InOutdoorsFilledOnKill : WeaponTraitWorker
    {
        public override void Notify_KilledPawn(Pawn pawn)
        {
            base.Notify_KilledPawn(pawn);
            pawn.AttemptToFillNeed(NeedDefOf.Indoors);
            pawn.AttemptToFillNeed(MPT_NeedDefOf.Outdoors);
        }
    }

    public class WeaponTraitWorker_BeautyFilledOnKill : WeaponTraitWorker
    {
        public override void Notify_KilledPawn(Pawn pawn)
        {
            base.Notify_KilledPawn(pawn);
            pawn.AttemptToFillNeed(MPT_NeedDefOf.Beauty);
        }
    }

    public class WeaponTraitWorker_ComfortFilledOnKill : WeaponTraitWorker
    {
        public override void Notify_KilledPawn(Pawn pawn)
        {
            base.Notify_KilledPawn(pawn);
            pawn.AttemptToFillNeed(MPT_NeedDefOf.Comfort);
        }
    }

    public class WeaponTraitWorker_ChemicalFilledOnKill : WeaponTraitWorker
    {
        public override void Notify_KilledPawn(Pawn pawn)
        {
            base.Notify_KilledPawn(pawn);
            var chemNeed = pawn.needs.AllNeeds.Where(need => need is Need_Chemical).RandomElementWithFallback();
            if (chemNeed == null) return;

            pawn.AttemptToFillNeed(chemNeed.def);
        }
    }
}