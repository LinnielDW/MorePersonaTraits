using System.Reflection;
using HarmonyLib;
using Verse;

namespace MorePersonaWeaponTraits
{
    public class MorePersonaWeaponTraits : Mod
    {
        public MorePersonaWeaponTraits(ModContentPack content) : base(content)
        {
            var harmony = new Harmony("com.arquebus.rimworld.mod.morepersonaweapontraits");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}