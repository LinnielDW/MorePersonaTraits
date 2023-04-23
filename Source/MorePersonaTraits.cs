using System.Reflection;
using HarmonyLib;
using MorePersonaTraits.Settings;
using UnityEngine;
using Verse;

namespace MorePersonaTraits;

public class MorePersonaTraits : Mod
{
    public static MorePersonaTraitsSettings settings;

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

public class MorePersonaTraitsSpawns : Mod
{
    public MorePersonaTraitsSpawns(ModContentPack content) : base(content)
    {
        settings = base.GetSettings<MorePersonaTraitsSpawnsSettings>();
    }

    public override string SettingsCategory()
    {
        return "MPT_SpawnSettingsTitle".Translate();
    }

    public override void DoSettingsWindowContents(Rect inRect)
    {
        settings.DoWindowContents(inRect);
        base.DoSettingsWindowContents(inRect);
    }

    public static MorePersonaTraitsSpawnsSettings settings;
}
