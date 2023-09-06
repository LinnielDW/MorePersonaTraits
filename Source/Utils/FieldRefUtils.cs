using System.Collections.Generic;
using HarmonyLib;
using RimWorld;
using Verse;

namespace MorePersonaTraits.Utils
{
    public static class FieldRefUtils
    {
        public static AccessTools.FieldRef<object, List<WeaponTraitDef>> TraitsFieldRef = AccessTools
            .FieldRefAccess<List<WeaponTraitDef>>(typeof(CompBladelinkWeapon), "traits");

        public static AccessTools.FieldRef<object, Thing> TargetFieldRef = AccessTools
            .FieldRefAccess<Thing>(typeof(CompTargetable), "target");
        
        public static ref Thing Target(this CompTargetable comp) => ref TargetFieldRef(comp);
    }
}