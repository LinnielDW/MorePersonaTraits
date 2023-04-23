using System.Collections.Generic;
using System.Linq;
using RimWorld;
using UnityEngine;
using Verse;

namespace MorePersonaTraits.Settings;

public class MorePersonaTraitsSpawnsSettings : ModSettings
{
    public static Dictionary<string, bool> WeaponTraitSpawnSettings = new Dictionary<string, bool>();

    private List<string> traitDefNames;
    private List<bool> boolValues;
    
    private static Vector2 scrollPosition = Vector2.zero;

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Collections.Look<string, bool>(ref WeaponTraitSpawnSettings, "WeaponTraitSpawnSettings", LookMode.Value, LookMode.Value, ref traitDefNames, ref boolValues, true);
    }

    public void DoWindowContents(Rect inRect)
    {
        Listing_Standard listingStandard = new Listing_Standard();
        
        Text.Font = GameFont.Medium;
        listingStandard.Label("WeaponTraitSpawnSettingsTitle".Translate(), -1, "WeaponTraitSpawnSettingsTooltip".Translate());
        Text.Font = GameFont.Small;

        int rowCount = DefDatabase<WeaponTraitDef>.AllDefs.Count();

        Rect viewRect = new Rect(inRect.x, inRect.y, inRect.width, inRect.height);
        Rect scrollRect = new Rect(0f, 0f, viewRect.width - 32f, (Text.LineHeight + listingStandard.verticalSpacing) * rowCount);

        Widgets.BeginScrollView(viewRect, ref scrollPosition, scrollRect);
        listingStandard.Begin(scrollRect);
        
        foreach (var trait in DefDatabase<WeaponTraitDef>.AllDefs)
        {
            var enabledSetting = GetOrCreateStorytellerEnabledSetting(trait.defName);
            listingStandard.CheckboxLabeled("MPT_Disable".Translate(trait.LabelCap), ref enabledSetting, trait.description);
            WeaponTraitSpawnSettings[trait.defName] = enabledSetting;
        }

        listingStandard.End();
        Widgets.EndScrollView();
    }
    
    bool GetOrCreateStorytellerEnabledSetting(string traitDefName)
    {
        var settingsValue = WeaponTraitSpawnSettings.TryGetValue(traitDefName, true);
        return settingsValue;
    }
}