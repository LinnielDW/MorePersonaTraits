using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using RimWorld.QuestGen;
using Verse;

namespace MorePersonaTraits;

public class QuestNode_GetRandomLootLabel : QuestNode
{
    protected override void RunInt()
    {
        Slate slate = QuestGen.slate;
        slate.Set("lootLabel", getLabel(slate), false);
    }

    private string getLabel(Slate slate)
    {
        var eles = slate.Get<IEnumerable<Thing>>("generatedItemStashThings").ToList();
        if (!eles.NullOrEmpty())
        {
            var ele = eles.RandomElement();
            if (ele.TryGetComp<CompGeneratedNames>()?.Name is { } label) return label;
            return ele.LabelNoParenthesisCap;
        }

        Log.Warning("generatedItemStashThings is empty. could not get label");
        return "";
    }

    protected override bool TestRunInt(Slate slate)
    {
        return true;
    }
}