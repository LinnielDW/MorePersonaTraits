using System;
using MorePersonaTraits.OnHitWorkerClasses;
using Verse;

namespace MorePersonaTraits.Extensions
{
    public class MorePersonaTraitsWeaponTraitExtension : DefModExtension
    {
        public float ProcChance = 0f;
        public float Magnitude = 0f;
        public Type OnHitWorkerClass = typeof(OnHitWorker);

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