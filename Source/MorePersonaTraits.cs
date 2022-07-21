using System.Reflection;
using HarmonyLib;
using Verse;

namespace MorePersonaTraits
{
    public class MorePersonaTraits : Mod
    {
        public MorePersonaTraits(ModContentPack content) : base(content)
        {
            var harmony = new Harmony("com.arquebus.rimworld.mod.morepersonatraits");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}