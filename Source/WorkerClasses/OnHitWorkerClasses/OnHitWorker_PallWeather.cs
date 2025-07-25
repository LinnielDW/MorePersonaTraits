﻿using RimWorld;
using UnityEngine;
using Verse;

namespace MorePersonaTraits.WorkerClasses.OnHitWorkerClasses;

[StaticConstructorOnStartup]
public static class PallCollection
{
    public static readonly GameConditionDef[] deathWeathers = { GameConditionDefOf.GrayPall, GameConditionDefOf.DeathPall };
}

public class OnHitWorker_PallWeather : OnHitWorker
{
    private FloatRange GrayPallConditionDaysRange = new(1f, 3f);

    public override void OnHitEffect(Thing hitThing, Thing originThing)
    {
        ApplyOnHitEffect(hitThing, originThing, TryMakeWeather);
    }

    private void TryMakeWeather(Thing thing)
    {
        if (thing.Map.weatherManager.curWeatherAge < 1600 || thing.Map.weatherDecider.ForcedWeather != null) return;
            
        var num = Mathf.CeilToInt(GrayPallConditionDaysRange.RandomInRange * 60000f);
        var chosenCondition = Rand.Element(PallCollection.deathWeathers);
            
        GameConditionMaker.MakeCondition(chosenCondition, num);

        Messages.Message("MPT_WeatherChangedBy".Translate(thing.LabelShort), thing, MessageTypeDefOf.NeutralEvent);
    }
}