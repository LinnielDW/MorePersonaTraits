using System;
using System.Linq;
using Verse;

namespace MorePersonaTraits.HediffComps;

public class HediffCompProperties_OnDeathSpawn : HediffCompProperties
{
    public HediffCompProperties_OnDeathSpawn()
    {
        compClass = typeof(HediffComp_OnDeathSpawn);
    }

    public ThingDef ThingDef;
    public FloatRange MultiplierRange = new(1, 1);
    public int MaxStackToSpawn = 1000;
    public bool DestroyPawn = true;
}

public class HediffComp_OnDeathSpawn : HediffComp
{
    public HediffCompProperties_OnDeathSpawn Props => (HediffCompProperties_OnDeathSpawn)props;

    private float valueToConvert;

    //Occurs before the pawn becomes a corpse
    public override void Notify_PawnKilled()
    {
        base.Notify_PawnKilled();

        if (Pawn.Destroyed || !Pawn.Spawned)
        {
            return;
        }

        foreach (var inventoryThing in Pawn.EquippedWornOrInventoryThings)
        {
            valueToConvert += inventoryThing.MarketValue;
        }

        if (Props.DestroyPawn)
        {
            Pawn.equipment?.DestroyAllEquipment();
            Pawn.inventory?.DestroyAll();
            Pawn.apparel?.DestroyAll();
        }
    }

    //Occurs after the pawn becomes a corpse
    public override void Notify_PawnDied()
    {
        base.Notify_PawnDied();

        valueToConvert += Pawn.Corpse.MarketValue;
        var thingToSpawn = ThingMaker.MakeThing(Props.ThingDef);

        thingToSpawn.stackCount = CalculateStackCount();

        if (thingToSpawn.stackCount > 0)
        {
            GenSpawn.Spawn(thingToSpawn, Pawn.Corpse.Position, Pawn.Corpse.Map);
        }

        if (Props.DestroyPawn)
        {
            Pawn.Corpse.Destroy();
        }
    }

    private int CalculateStackCount()
    {
        return new[]
        {
            Props.MaxStackToSpawn,
            Props.ThingDef.stackLimit,
            (int)Math.Floor((valueToConvert * Props.MultiplierRange.RandomInRange) / (Props.ThingDef.BaseMarketValue * 2))
        }.Min();
    }
}