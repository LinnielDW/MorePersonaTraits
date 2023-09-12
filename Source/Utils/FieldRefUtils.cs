using System;
using HarmonyLib;
using RimWorld;
using Verse;

namespace MorePersonaTraits.Utils
{
    public static class FieldRefUtils
    {
        public static AccessTools.FieldRef<object, IntRange> IntRangeFieldRef = AccessTools
            .FieldRefAccess<IntRange>(typeof(CompBladelinkWeapon), "TraitsRange");

        public static AccessTools.FieldRef<object, Thing> TargetFieldRef = AccessTools
            .FieldRefAccess<Thing>(typeof(CompTargetable), "target");
        
        public static ref Thing Target(this CompTargetable comp) => ref TargetFieldRef(comp);

        public static AccessTools.FieldRef<object, Action<LocalTargetInfo>> OnGuiActionFieldRef = AccessTools
            .FieldRefAccess<Action<LocalTargetInfo>>(typeof(Targeter), "onGuiAction");

        public static void NullifyOnGuiAction()
        {
            OnGuiActionFieldRef(Find.Targeter) = null;
        }
    }
}