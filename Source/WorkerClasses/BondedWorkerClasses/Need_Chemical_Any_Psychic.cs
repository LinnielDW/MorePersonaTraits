﻿using System.Collections.Generic;
using MorePersonaTraits.Extensions;
 using MorePersonaTraits.WorkerClasses.BondedWorkerClasses;
 using RimWorld;
using UnityEngine;
using Verse;

namespace MorePersonaTraits
{
    // public class Need_Chemical_Any_Psychic : Need_Chemical_Any
    // {
    //     public Need_Chemical_Any_Psychic(Pawn pawn) : base(pawn)
    //     {
    //     }
    //     
    //     public override void DrawOnGUI(
    //         Rect rect,
    //         int maxThresholdMarkers = 2147483647,
    //         float customMargin = -1f,
    //         bool drawArrows = true,
    //         bool doTooltip = true,
    //         Rect? rectForTooltip = null)
    //     {
    //         if (PawnHasDrugDesirePsychic(pawn))
    //         {
    //             threshPercents = new List<float>();
    //             threshPercents.Add(CurrentLevelThresholds.extremelyNegative);
    //             threshPercents.Add(CurrentLevelThresholds.veryNegative);
    //             threshPercents.Add(CurrentLevelThresholds.negative);
    //             threshPercents.Add(CurrentLevelThresholds.positive);
    //             threshPercents.Add(CurrentLevelThresholds.veryPositive);
    //         }
    //         base.DrawOnGUI(rect, maxThresholdMarkers, customMargin, drawArrows, doTooltip, rectForTooltip);
    //     }
    //     
    //     
    //     
    //     
    //     public override bool ShowOnNeedList
    //     {
    //         get
    //         {
    //             return !Disabled;
    //         }
    //     }
    //     
    //     protected float FallPerNeedIntervalTick
    //     {
    //         get
    //         {
    //             float num = 1f * InterestDegreeFallCurve.Evaluate(CurLevel);;
    //             return def.fallPerDay * num / 60000f * 150f;
    //         }
    //     }
    //
    //     private bool Disabled => !PawnHasDrugDesirePsychic(pawn);
    //
    //
    //     private static readonly SimpleCurve InterestDegreeFallCurve = new SimpleCurve()
    //     {
    //         {
    //             new CurvePoint(0.0f, 0.3f),
    //             true
    //         },
    //         {
    //             new CurvePoint(InterestDegreeLevelThresholdsForMood.negative, 0.6f),
    //             true
    //         },
    //         {
    //             new CurvePoint(InterestDegreeLevelThresholdsForMood.negative + 1f / 1000f, 1f),
    //             true
    //         },
    //         {
    //             new CurvePoint(InterestDegreeLevelThresholdsForMood.positive, 1f),
    //             true
    //         },
    //         {
    //             new CurvePoint(1f, 1f),
    //             true
    //         }
    //     };
    //
    //     private LevelThresholds CurrentLevelThresholds => InterestDegreeLevelThresholdsForMood;
    //
    //     private static readonly LevelThresholds InterestDegreeLevelThresholdsForMood = new LevelThresholds
    //     {
    //         extremelyNegative = 0.01f,
    //         veryNegative = 0.15f,
    //         negative = 0.3f,
    //         positive = 0.6f,
    //         veryPositive = 0.75f
    //     };
    //     
    //     private static bool PawnHasDrugDesirePsychic(Pawn pawn)
    //     {
    //         try
    //         {
    //             return pawn.equipment.bondedWeapon.TryGetComp<CompBladelinkWeapon>().TraitsListForReading
    //                 .Exists(trait => ((BondedWorker_ChemicalAny) trait.GetModExtension<WeaponTraitBondedNeedExtension>()
    //                     .BondedWorker).NeedDef.defName == "DrugDesirePsychic");
    //         }
    //         catch
    //         {
    //             return false;
    //         }
    //     }
    //     
    //     
    // }
}