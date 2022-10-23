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
        var ele = slate.Get<IEnumerable<Thing>>("generatedItemStashThings").ToList().RandomElement();
        if (ele.TryGetComp<CompGeneratedNames>()?.Name is { } label) return label;
        return ele.LabelNoParenthesisCap;
    }

    protected override bool TestRunInt(Slate slate)
    {
        return true;
    }
}