using UnityEngine;

namespace FOW.Mutations
{
    public class PlayerMutationHandler : MonoBehaviour
    {
        public float damageMultiplier = 1f;
        public float rangeMultiplier = 1f;

        private void Update()
        {
            ApplyEquippedMutations();
        }

        private void ApplyEquippedMutations()
        {
            damageMultiplier = 1f;
            rangeMultiplier = 1f;

            foreach (var kvp in MutationInventoryManager.Instance.equippedMutations)
            {
                MutationInfo mutation = kvp.Value;
                if (mutation == null) continue;

                switch (mutation.effectType)
                {
                    case MutationEffectType.DamageBoost:
                        damageMultiplier *= mutation.damageMultiplier;
                        break;
                    case MutationEffectType.RangeBoost:
                        rangeMultiplier *= mutation.slashRangeMultiplier;
                        break;
                }
            }
        }
    }
}
