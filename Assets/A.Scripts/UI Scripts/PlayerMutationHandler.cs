using UnityEngine;

namespace FOW.Mutations
{
    public class PlayerMutationHandler : MonoBehaviour
    {
        
        public float damageMultiplier = 1f;
        public float rangeMultiplier = 1f;

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
