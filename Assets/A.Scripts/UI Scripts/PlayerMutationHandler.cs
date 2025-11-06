using UnityEngine;

namespace FOW.Mutations
{
    public class PlayerMutationHandler : MonoBehaviour
    {
        public float damageMultiplier = 1f;
        public float rangeMultiplier = 1f;

        private void Update()
        {
            Recalculate();
        }

        private void Recalculate()
        {
            damageMultiplier = 1f;
            rangeMultiplier = 1f;

            var inv = MutationInventoryManager.Instance;
            if (inv == null) return;

            foreach (var kv in inv.equippedMutations)
            {
                var m = kv.Value;
                if (m == null) continue;

                // Hook up when you add effect fields (e.g., damage boost/range boost).
                // Example if you later add parameters:
                // if (m.effectType == DamageBoost) damageMultiplier *= m.damageMultiplier;
                // if (m.effectType == RangeBoost) rangeMultiplier *= m.slashRangeMultiplier;
            }
        }
    }
}
