using System.Collections.Generic;
using System.Linq;
using RimWorld;
using UnityEngine;
using Verse;

namespace MorePersonaTraits.Settings;

[StaticConstructorOnStartup]
public class MPTStatics
{
    public static readonly List<WeaponTraitDef> AllTraitsAlphabetically = DefDatabase<WeaponTraitDef>.AllDefs.OrderBy(x => x.label).ToList();
}

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
        int rowCount = MPTStatics.AllTraitsAlphabetically.Count();

        Listing_Standard listingStandard = new Listing_Standard();
        
        listingStandard.Begin(inRect);
        
        listingStandard.Label("WeaponTraitSpawnSettingsTooltip".Translate());

        Rect resetRect = new Rect(0f, listingStandard.CurHeight, (listingStandard.ColumnWidth * 0.25f) - 6f, 30f);
        if (Widgets.ButtonText(resetRect, "reset")) {
            foreach (var negativeTrait in MPTStatics.AllTraitsAlphabetically)
            {
                WeaponTraitSpawnSettings[negativeTrait.defName] = true;
            }
            base.Write();
        }
        
        Rect disableBadTraitsRect = new Rect(resetRect.xMax + 6f, listingStandard.CurHeight, listingStandard.ColumnWidth * 0.25f - 6f, 30f);
        if (Widgets.ButtonText(disableBadTraitsRect, "disable all negative traits"))
        {
            foreach (var negativeTrait in MPTStatics.AllTraitsAlphabetically.Where(t => t.marketValueOffset < 0))
            {
                WeaponTraitSpawnSettings[negativeTrait.defName] = false;
            }
            base.Write();
        }
        
        Rect disableVanillaTraitsRect = new Rect(disableBadTraitsRect.xMax + 6f, listingStandard.CurHeight, listingStandard.ColumnWidth * 0.25f - 6f, 30f);
        if (Widgets.ButtonText(disableVanillaTraitsRect, "disable all vanilla traits"))
        {
            foreach (var negativeTrait in MPTStatics.AllTraitsAlphabetically.Where(t => t.modContentPack.IsOfficialMod))
            {
                WeaponTraitSpawnSettings[negativeTrait.defName] = false;
            }
            base.Write();
        }
        
        listingStandard.Gap(32f);

        Rect viewRect = new Rect(inRect.x, listingStandard.CurHeight, inRect.width, inRect.height - listingStandard.CurHeight);
        Rect scrollRect = new Rect(0f, 0f, viewRect.width - 32f, (Text.LineHeight + listingStandard.verticalSpacing) * rowCount);

        Widgets.BeginScrollView(viewRect, ref scrollPosition, scrollRect);
        listingStandard.Begin(scrollRect);
        
        foreach (var trait in MPTStatics.AllTraitsAlphabetically)
        {
            var enabledSetting = GetOrCreateStorytellerEnabledSetting(trait.defName);
            listingStandard.CheckboxLabeled(trait.LabelCap, ref enabledSetting, trait.description);
            WeaponTraitSpawnSettings[trait.defName] = enabledSetting;
        }

        listingStandard.End();
        Widgets.EndScrollView();
        
        listingStandard.End();
    }
    
    bool GetOrCreateStorytellerEnabledSetting(string traitDefName)
    {
        var settingsValue = WeaponTraitSpawnSettings.TryGetValue(traitDefName, true);
        return settingsValue;
    }
}