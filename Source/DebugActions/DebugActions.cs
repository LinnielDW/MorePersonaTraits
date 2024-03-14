using System.Collections.Generic;
using System.Linq;
using LudeonTK;
using MorePersonaTraits.Utils;
using RimWorld;
using Verse;
// ReSharper disable UnusedMember.Local

namespace MorePersonaTraits.DebugActions
{
    public class DebugActions
    {
        [DebugAction("PersonaWeapons", "[MPT] Spawn Persona Weapon", true, allowedGameStates = AllowedGameStates.PlayingOnMap)]
        private static void SpawnPersonaWeapon()
        {
            List<DebugMenuOption> list = new List<DebugMenuOption>();
            foreach (ThingDef localDef in from def in DefDatabase<ThingDef>.AllDefs
                where def.comps.Exists(p => p.compClass == typeof(CompBladelinkWeapon))
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
        
        [DebugAction("PersonaWeapons", "[MPT] Add Weapon Trait", true, allowedGameStates = AllowedGameStates.PlayingOnMap)]
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
                weapon.TryGetComp<CompBladelinkWeapon>().TempUnbond();
                weapon.TryGetComp<CompBladelinkWeapon>().TraitsListForReading.Add(weaponTraitDef);
                weapon.TryGetComp<CompBladelinkWeapon>().RegainBond();
            }
        }
        
        [DebugAction("PersonaWeapons", "[MPT] Remove Weapon Trait", true, actionType = DebugActionType.ToolMap, allowedGameStates = AllowedGameStates.PlayingOnMap)]
        private static void RemoveWeaponTrait()
        {
            List<DebugMenuOption> list = new List<DebugMenuOption>();
            var weapon = (ThingWithComps)Find.CurrentMap.thingGrid.ThingsAt(UI.MouseCell()).First(t => t is ThingWithComps comps && comps.TryGetComp<CompBladelinkWeapon>() != null);
            
            foreach (var trait in weapon.TryGetComp<CompBladelinkWeapon>().TraitsListForReading)
            {
                list.Add(new DebugMenuOption(trait.defName, DebugMenuOptionMode.Action,
                    delegate { RemoveTrait(weapon, trait); }));
            }

            Find.WindowStack.Add(new Dialog_DebugOptionListLister(list));
        }

        private static void RemoveTrait(
            ThingWithComps weapon,
            WeaponTraitDef weaponTraitDef)
        {
            weapon.TryGetComp<CompBladelinkWeapon>().TempUnbond();
            weapon.TryGetComp<CompBladelinkWeapon>().TraitsListForReading.Remove(weaponTraitDef);
            if (weapon.TryGetComp<CompBladelinkWeapon>().TraitsListForReading.NullOrEmpty()) weapon.TryGetComp<CompBladelinkWeapon>().InitializeTraits();
            weapon.TryGetComp<CompBladelinkWeapon>().RegainBond();
        }

        [DebugAction("PersonaWeapons", "[MPT] Reroll Weapon Traits", true, actionType = DebugActionType.ToolMap, allowedGameStates = AllowedGameStates.PlayingOnMap)]
        private static void RerollTraits()
        {
            foreach (var weapon in (from t in Find.CurrentMap.thingGrid.ThingsAt(UI.MouseCell())
                where t is ThingWithComps && (t as ThingWithComps).TryGetComp<CompBladelinkWeapon>() != null
                select t).Cast<ThingWithComps>())
            {
                weapon.TryGetComp<CompBladelinkWeapon>().TempUnbond();
                weapon.TryGetComp<CompBladelinkWeapon>().TraitsListForReading.Clear();
                weapon.TryGetComp<CompBladelinkWeapon>().InitializeTraits();
                weapon.TryGetComp<CompBladelinkWeapon>().RegainBond();
            }
        }

        //TODO: change this to just add 1 trait and make a new debug that simulates an annul
        [DebugAction("PersonaWeapons", "[MPT] Justaddmorelol", true, actionType = DebugActionType.ToolMap, allowedGameStates = AllowedGameStates.PlayingOnMap)]
        private static void JustAddMore()
        {
            foreach (var weapon in (from t in Find.CurrentMap.thingGrid.ThingsAt(UI.MouseCell())
                where t is ThingWithComps && (t as ThingWithComps).TryGetComp<CompBladelinkWeapon>() != null
                select t).Cast<ThingWithComps>())
            {
                weapon.TryGetComp<CompBladelinkWeapon>().TempUnbond();
                weapon.TryGetComp<CompBladelinkWeapon>().InitializeTraits();
                weapon.TryGetComp<CompBladelinkWeapon>().RegainBond();
            }
        }

        [DebugAction("PersonaWeapons", "[MPT] Regenerate Name", true, actionType = DebugActionType.ToolMap, allowedGameStates = AllowedGameStates.PlayingOnMap)]
        private static void regenerateName()
        {
            foreach (var weapon in (from t in Find.CurrentMap.thingGrid.ThingsAt(UI.MouseCell())
                where t is ThingWithComps && (t as ThingWithComps).TryGetComp<CompBladelinkWeapon>() != null
                select t).Cast<ThingWithComps>())
            {
                weapon.TryGetComp<CompGeneratedNames>().Initialize(weapon.TryGetComp<CompGeneratedNames>().Props);
            }
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

        [DebugOutput(category = "PersonaWeapons",name = "Traits Table", onlyWhenPlaying = true)]
        public static void AllWeaponTraits()
        {
            var traits = DefDatabase<WeaponTraitDef>.AllDefsListForReading
                // .Where(trait => trait.modContentPack.ToStringSafe() == "Arquebus.MorePersonaTraits")
                ;
            TableDataGetter<WeaponTraitDef>[] array = new TableDataGetter<WeaponTraitDef>[8];
            
            
            array[0] = new TableDataGetter<WeaponTraitDef>("defName", d => d.defName);
            array[1] = new TableDataGetter<WeaponTraitDef>("label", d => d.label);
            array[2] = new TableDataGetter<WeaponTraitDef>("description", d => d.description);
            array[3] = new TableDataGetter<WeaponTraitDef>("commonality", d => d.commonality);
            array[4] = new TableDataGetter<WeaponTraitDef>("exclusionTags", d =>
            {
                var txt = "";
                if (!d.exclusionTags.NullOrEmpty())
                {
                    foreach (var tag in d.exclusionTags)
                    {
                        txt += tag + ",";
                    }
                }

                return txt;
            });
            array[5] = new TableDataGetter<WeaponTraitDef>("marketValueOffset", d => d.marketValueOffset);
            array[6] = new TableDataGetter<WeaponTraitDef>("equippedStatOffsets", d =>
            {
                var txt = "";
                if (!d.equippedStatOffsets.NullOrEmpty())
                {
                    foreach (var offset in d.equippedStatOffsets)
                    {
                        txt += offset.stat + ": " + offset.value + "\n";
                    }
                }

                return txt;
            });
            array[7] = new TableDataGetter<WeaponTraitDef>("bondedStatOffsets", d =>
            {
                var txt = "";
                if (!d.bondedHediffs.NullOrEmpty() && !d.bondedHediffs[0].stages[0].statOffsets.NullOrEmpty())
                {
                    foreach (var offset in d.bondedHediffs[0].stages[0].statOffsets)
                    {
                        txt += offset.stat + ": " + offset.value + "\n";
                    }
                }

                return txt;
            });
            
            DebugTables.MakeTablesDialog(traits, array);
        }
    }
    
}