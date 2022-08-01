using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
// ReSharper disable UnusedMember.Local

namespace MorePersonaTraits.Utils
{
    public class DebugUtils
    {
        [DebugAction("Spawning", "[MPT] Add Weapon Trait", true, allowedGameStates = AllowedGameStates.PlayingOnMap)]
        private static void AddWeaponTrait()
        {
            List<DebugMenuOption> list = new List<DebugMenuOption>();
            foreach (var weaponTraitDef in DefDatabase<WeaponTraitDef>.AllDefsListForReading)
            {
                list.Add(new DebugMenuOption(weaponTraitDef.defName, DebugMenuOptionMode.Tool,
                    delegate { AddTrait(weaponTraitDef, UI.MouseCell()); }));
            }

            Find.WindowStack.Add(new Dialog_DebugOptionListLister(list));
        }

        private static void AddTrait(
            WeaponTraitDef weaponTraitDef,
            IntVec3 c)
        {
            foreach (var weapon in (from t in Find.CurrentMap.thingGrid.ThingsAt(c)
                where t is ThingWithComps && (t as ThingWithComps).TryGetComp<CompBladelinkWeapon>() != null
                select t).Cast<ThingWithComps>())
            {
                weapon.TryGetComp<CompBladelinkWeapon>().TraitsListForReading.Add(weaponTraitDef);
            }
        }

        [DebugAction("Spawning", "[MPT] Reroll Weapon Traits", true, actionType = DebugActionType.ToolMap, allowedGameStates = AllowedGameStates.PlayingOnMap)]
        private static void RerollTraits()
        {
            foreach (var weapon in (from t in Find.CurrentMap.thingGrid.ThingsAt(UI.MouseCell())
                where t is ThingWithComps && (t as ThingWithComps).TryGetComp<CompBladelinkWeapon>() != null
                select t).Cast<ThingWithComps>())
            {
                weapon.TryGetComp<CompBladelinkWeapon>().TraitsListForReading.Clear();
                weapon.TryGetComp<CompBladelinkWeapon>().InitializeTraits();
            }
        }

        [DebugAction("Spawning", "[MPT] Justaddmorelol", true, actionType = DebugActionType.ToolMap, allowedGameStates = AllowedGameStates.PlayingOnMap)]
        private static void JustAddMore()
        {
            foreach (var weapon in (from t in Find.CurrentMap.thingGrid.ThingsAt(UI.MouseCell())
                where t is ThingWithComps && (t as ThingWithComps).TryGetComp<CompBladelinkWeapon>() != null
                select t).Cast<ThingWithComps>())
            {
                weapon.TryGetComp<CompBladelinkWeapon>().InitializeTraits();
            }
        }

        [DebugAction("Spawning", "[MPT] Spawn Persona Weapon", true, allowedGameStates = AllowedGameStates.PlayingOnMap)]
        private static void SpawnPersonaWeapon()
        {
            List<DebugMenuOption> list = new List<DebugMenuOption>();
            foreach (ThingDef localDef in from def in DefDatabase<ThingDef>.AllDefs
                where /*def.equipmentType == EquipmentType.Primary &&*/
                      def.comps.Exists(p => p.compClass == typeof(CompBladelinkWeapon))
                select def
                into d
                orderby d.defName
                select d)
            {
                list.Add(new DebugMenuOption(localDef.defName, DebugMenuOptionMode.Action,
                    delegate
                    {
                        Find.WindowStack.Add(new Dialog_DebugOptionListLister(ChooseWeaponTraits(localDef)));
                    }));
            }

            Find.WindowStack.Add(new Dialog_DebugOptionListLister(list));
        }

        private static List<DebugMenuOption> ChooseWeaponTraits(ThingDef localDef)
        {
            var list = new List<DebugMenuOption>();

            foreach (var weaponTraitDef in DefDatabase<WeaponTraitDef>.AllDefsListForReading)
            {
                list.Add(new DebugMenuOption(weaponTraitDef.defName, DebugMenuOptionMode.Tool,
                    delegate { DebugSpawnPersonaWeapon(localDef, weaponTraitDef, UI.MouseCell()); }));
            }

            return list;
        }

        private static void DebugSpawnPersonaWeapon(
            ThingDef def,
            WeaponTraitDef weaponTraitDef,
            IntVec3 c,
            int stackCount = -1,
            bool direct = false,
            ThingStyleDef thingStyleDef = null)
        {
            if (stackCount <= 0)
                stackCount = def.stackLimit;

            var personaWeaponThing = ThingMaker.MakeThing(def, GenStuff.RandomStuffFor(def));

            if (thingStyleDef != null)
                personaWeaponThing.StyleDef = thingStyleDef;
            personaWeaponThing.TryGetComp<CompQuality>()?.SetQuality(QualityUtility.GenerateQualityRandomEqualChance(),
                ArtGenerationContext.Colony);
            if (personaWeaponThing.def.Minifiable)
                personaWeaponThing = personaWeaponThing.MakeMinified();
            personaWeaponThing.stackCount = stackCount;

            personaWeaponThing.TryGetComp<CompBladelinkWeapon>().TraitsListForReading.Clear();
            personaWeaponThing.TryGetComp<CompBladelinkWeapon>().TraitsListForReading.Add(weaponTraitDef);

            if (direct)
                GenPlace.TryPlaceThing(personaWeaponThing, c, Find.CurrentMap, ThingPlaceMode.Direct);
            else
                GenPlace.TryPlaceThing(personaWeaponThing, c, Find.CurrentMap, ThingPlaceMode.Near);
        }
    }
}