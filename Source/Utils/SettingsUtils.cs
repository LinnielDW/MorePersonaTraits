using Verse;

namespace MorePersonaWeaponTraits.Utils
{
    public static class SettingsUtils
    {
        public static void DrawLabelledNumericSetting<T>(this Listing_Standard settingsList, ref T settingValue, ref string settingsBuffer, string settingName, T min, T max) where T : struct
        {
            var settingValueStringBuffer = settingValue.ToString();

            var numericSettingRect = settingsList.GetRect(24f);
            var leftSide = numericSettingRect.LeftPart(0.8f).Rounded();
            var rightSide = numericSettingRect.RightPart(0.2f).Rounded();

            Widgets.Label(leftSide, settingName.Translate());
            TooltipHandler.TipRegion(leftSide, (settingName + "Tooltip").Translate());

            Widgets.TextFieldNumeric(rightSide, ref settingValue, ref settingValueStringBuffer, float.Parse(min.ToString()), float.Parse(max.ToString()));
        }
        
    }
}