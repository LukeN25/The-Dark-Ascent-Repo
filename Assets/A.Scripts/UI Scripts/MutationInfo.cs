using UnityEngine;

namespace FOW.Mutations
{
    [CreateAssetMenu(menuName = "Mutations/Mutation Info")]
    public class MutationInfo : ScriptableObject
    {
        public string mutationName;
        public Sprite icon;
        public string description;

        public MutationSlotType[] allowedSlots;
        public float damageMultiplier = 1f;
        public float rangeMultiplier = 1f;
    }
}
