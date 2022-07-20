using RimWorld;

namespace MorePersonaTraits
{
    public class OnHitWorker
    {
        public float ProcChance = 0f;
        public float DefaultMagnitude = 0f;
        public MorePersonaTraitsWeaponTraitExtension def;
        
        public virtual void OnHitEffect() {
        }

    }
}