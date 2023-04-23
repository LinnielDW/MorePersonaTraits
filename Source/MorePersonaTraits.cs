using System.Reflection;
using HarmonyLib;
using MorePersonaTraits.Settings;
using UnityEngine;
using Verse;

namespace MorePersonaTraits;

public class MorePersonaTraits : Mod
{
    readonly MorePersonaTraitsSettings settings;

    public MorePersonaTraits(ModContentPack content) : base(content)
    {
        settings = GetSettings<MorePersonaTraitsSettings>();

        var harmony = new Harmony("com.arquebus.rimworld.mod.morepersonatraits");
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