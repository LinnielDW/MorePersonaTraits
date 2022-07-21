﻿using System;
using System.Collections.Generic;
using MorePersonaTraits.OnHitWorkerClasses;
using RimWorld;
using Verse;

namespace MorePersonaTraits.Extensions
{
    public class WeaponTraitOnHitExtension : DefModExtension
    {
        public float ProcChance = 0f;
        public float ProcMagnitude = 0f;

        public Type OnHitWorkerClass = typeof(OnHitWorker);

        public HediffDef TargetThingHediff = null;
        public HediffDef OriginThingHediff = null;
        public ThoughtDef TargetThingThoughtDef = null;
        public ThoughtDef OriginThingThoughtDef = null;

        private OnHitWorker _onHitWorker;

        public OnHitWorker OnHitWorker
        {
            get
            {
                if (_onHitWorker == null)
                {
                    _onHitWorker = (OnHitWorker) Activator.CreateInstance(OnHitWorkerClass);
                    _onHitWorker.WeaponTraitOnHitExtension = this;
                }

                return _onHitWorker;
            }
        }
    }
}