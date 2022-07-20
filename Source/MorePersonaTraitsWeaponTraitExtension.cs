using System;
using Verse;

namespace MorePersonaTraits
{
    public class MorePersonaTraitsWeaponTraitExtension : DefModExtension
    {
        public Type OnHitWorkerClass = typeof(OnHitWorker);
        private OnHitWorker _onHitWorker;
        
        public OnHitWorker OnHitWorker
        {
            get
            {
                if (_onHitWorker == null)
                {
                    _onHitWorker = (OnHitWorker) Activator.CreateInstance(OnHitWorkerClass);
                    _onHitWorker.def = this;
                }

                return _onHitWorker;
            }
        }
    }
}