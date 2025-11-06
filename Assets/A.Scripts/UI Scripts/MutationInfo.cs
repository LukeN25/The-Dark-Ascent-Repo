using UnityEngine;

namespace FOW.Mutations
{
    [CreateAssetMenu(fileName = "NewMutation", menuName = "Mutations/MutationInfo")]
    public class MutationInfo : ScriptableObject
    {
        public string mutationName;
        public Sprite icon;
        public string description;
        public MutationSlotType slotType; 
    }

    public enum MutationSlotType
    {
        Head, Chest, Heart, LeftArm, RightArm, LeftLeg, RightLeg
    }
}
