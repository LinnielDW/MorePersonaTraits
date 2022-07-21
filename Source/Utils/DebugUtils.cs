using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace MorePersonaTraits.Utils
{
    public class DebugUtils
    {
        [DebugAction("Spawning", "[MPT] SpawnPersonaWeapon", true, false, allowedGameStates = AllowedGameStates.PlayingOnMap)]
        private static void SpawnPersonaWeapon()
        {
            List<DebugMenuOption> list = new List<DebugMenuOption>();
            foreach (ThingDef localDef in from def in DefDatabase<ThingDef>.AllDefs
                where def.equipmentType == EquipmentType.Primary &&
                      def.comps.Exists(p => p.compClass == typeof(CompBladelinkWeapon))
                select def
                into d
                orderby d.defName
                select d)
            {
                list.Add(new DebugMenuOption(localDef.defName, DebugMenuOptionMode.Action, delegate()
                {
                    // DebugThingPlaceHelper.DebugSpawn(localDef, UI.MouseCell(), -1, false, null);
                    Find.WindowStack.Add(new Dialog_DebugOptionListLister(ChooseWeaponTraits(localDef)));
                }));
            }

            Find.WindowStack.Add(new Dialog_DebugOptionListLister(list));
        }

        private static List<DebugMenuOption> ChooseWeaponTraits(ThingDef localDef)
        {
            List<DebugMenuOption> list = new List<DebugMenuOption>();

            foreach (WeaponTraitDef weaponTraitDef in DefDatabase<WeaponTraitDef>.AllDefsListForReading)
            {
                list.Add(new DebugMenuOption(weaponTraitDef.defName, DebugMenuOptionMode.Tool,
                    delegate()
                    {
                        DebugSpawnPersonaWeapon(localDef, weaponTraitDef, UI.MouseCell(), -1, false, null);
                    }));
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
            ThingDef stuff = GenStuff.RandomStuffFor(def);
            Thing thing = ThingMaker.MakeThing(def, stuff);
            if (thingStyleDef != null)
                thing.StyleDef = thingStyleDef;
            thing.TryGetComp<CompQuality>()?.SetQuality(QualityUtility.GenerateQualityRandomEqualChance(),
                ArtGenerationContext.Colony);
            if (thing.def.Minifiable)
                thing = (Thing) thing.MakeMinified();
            thing.stackCount = stackCount;

            thing.TryGetComp<CompBladelinkWeapon>()?.TraitsListForReading.Clear();
            thing.TryGetComp<CompBladelinkWeapon>()?.TraitsListForReading.Add(weaponTraitDef);

            if (direct)
                GenPlace.TryPlaceThing(thing, c, Find.CurrentMap, ThingPlaceMode.Direct, (Action<Thing, int>) null,
                    (Predicate<IntVec3>) null, new Rot4());
            else
                GenPlace.TryPlaceThing(thing, c, Find.CurrentMap, ThingPlaceMode.Near, (Action<Thing, int>) null,
                    (Predicate<IntVec3>) null, new Rot4());
        }
    }
}