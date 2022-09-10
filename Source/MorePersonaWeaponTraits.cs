using System.Reflection;
using HarmonyLib;
using MorePersonaWeaponTraits.Settings;
using UnityEngine;
using Verse;

namespace MorePersonaWeaponTraits
{
    public class MorePersonaWeaponTraits : Mod
    {
        readonly MorePersonaWeaponTraitsSettings settings;

        public MorePersonaWeaponTraits(ModContentPack content) : base(content)
        {
            settings = GetSettings<MorePersonaWeaponTraitsSettings>();

            var harmony = new Harmony("com.arquebus.rimworld.mod.morepersonaweapontraits");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            settings.DoSettingsWindowContents(inRect);
            base.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "MPT_SettingsTitle".Translate();
        }
    }
}