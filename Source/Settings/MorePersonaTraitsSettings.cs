using MorePersonaTraits.Utils;
using UnityEngine;
using Verse;

namespace MorePersonaTraits.Settings
{
    
    public class MorePersonaTraitsSettings : ModSettings
    {

        public static int minTraits = 1;
        public static int maxTraits = 4; //Default for vanilla is 3
        public static int absoluteMaxTraits = 10;
        public static float rangedProcChanceFactor = 0.5f;
        public static bool showBoundLetterForBladeWhisperer = true;
        public static bool showBoundMessageInsteadForBladeWhisperer = true;

        private static string minTraitsBuffer = minTraits.ToString();
        private static string maxTraitsBuffer = maxTraits.ToString();
        private static string absoluteMaxTraitsBuffer = absoluteMaxTraits.ToString();
        private static string rangedProcChanceFactorBuffer = rangedProcChanceFactor.ToString();

        public override void ExposeData()
        {
            if (minTraits > maxTraits)
            {
                minTraits = maxTraits;
            }

            Scribe_Values.Look(ref minTraits, "minTraits", 1);
            Scribe_Values.Look(ref maxTraits, "maxTraits", 5);
            Scribe_Values.Look(ref absoluteMaxTraits, "absoluteMaxTraits", 10);
            Scribe_Values.Look(ref rangedProcChanceFactor, "rangedProcChanceFactor", 0.5f);
            Scribe_Values.Look(ref showBoundLetterForBladeWhisperer, "showBoundLetterForBladeWhisperer", true);
            Scribe_Values.Look(ref showBoundMessageInsteadForBladeWhisperer, "showBoundMessageInsteadForBladeWhisperer", true);
            
            base.ExposeData();
        }

        public void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();

            listingStandard.Begin(inRect);
            listingStandard.DrawLabelledNumericSetting(ref minTraits, ref minTraitsBuffer, "MPT_minTraits", 1, 500);
            listingStandard.DrawLabelledNumericSetting(ref maxTraits, ref maxTraitsBuffer, "MPT_maxTraits", 1, 500);
            listingStandard.DrawLabelledNumericSetting(ref absoluteMaxTraits, ref absoluteMaxTraitsBuffer, "MPT_absoluteMaxTraits", 1, 500);
            listingStandard.DrawLabelledNumericSetting(ref rangedProcChanceFactor, ref rangedProcChanceFactorBuffer, "MPT_rangedProcChanceFactor", 0f, 1f);
            listingStandard.CheckboxLabeled("MPT_showBoundLetterForBladeWhisperer".Translate(), ref showBoundLetterForBladeWhisperer, tooltip: "MPT_showBoundLetterForBladeWhispererTooltip".Translate());
            listingStandard.CheckboxLabeled("MPT_showBoundMessageInsteadForBladeWhisperer".Translate(), ref showBoundMessageInsteadForBladeWhisperer, tooltip: "MPT_showBoundMessageInsteadForBladeWhispererTooltip".Translate());
            listingStandard.End();
        }
    }
}