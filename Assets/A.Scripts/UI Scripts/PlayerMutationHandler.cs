using UnityEngine;

namespace FOW.Mutations
{
    public class PlayerMutationHandler : MonoBehaviour
    {
        public static PlayerMutationHandler Instance;
        

        public float damageMultiplier = 1f;
        public float rangeMultiplier = 1f;

        private void Awake()
        {
            Instance = this;
        }

        public void ApplyMutation(MutationInfo mutation)
        {
            if (mutation == null) return;

            damageMultiplier *= mutation.damageMultiplier;
            rangeMultiplier *= mutation.rangeMultiplier;

            Debug.Log("Applied mutation: " + mutation.mutationName);
        }

        public void ResetMutationEffects()
        {
            damageMultiplier = 1f;
            rangeMultiplier = 1f;
        }
    }
}
