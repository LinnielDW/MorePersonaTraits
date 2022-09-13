using System.Collections.Generic;
using HarmonyLib;
using RimWorld;

namespace MorePersonaTraits.Utils
{
    public static class FieldRefUtils
    {
        public static AccessTools.FieldRef<object, List<WeaponTraitDef>> TraitsFieldRef = AccessTools
            .FieldRefAccess<List<WeaponTraitDef>>(typeof(CompBladelinkWeapon), "traits");
    }
}