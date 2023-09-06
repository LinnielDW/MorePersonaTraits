using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld.QuestGen;
using Verse;

namespace MorePersonaTraits;

public class QuestNode_SetItemBySetMakerTags : QuestNode
{
    protected override void RunInt()
    {
        Slate slate = QuestGen.slate;
        slate.Set("itemStashThings", GetThingsByTags(slate), false);
    }

    private IEnumerable<ThingDef> GetThingsByTags(Slate slate)
    {
        List<String> tags = thingSetMakerTags.GetValue(slate).ToList();
        if (tags != null)
        {
            yield return DefDatabase<ThingDef>.AllDefs.Where(thing => !thing.thingSetMakerTags.NullOrEmpty() && thing.thingSetMakerTags.Intersect(tags).Any()).RandomElement();
        }

        yield break;
    }

    protected override bool TestRunInt(Slate slate)
    {
        return true;
    }

    public SlateRef<IEnumerable<String>> thingSetMakerTags;
}