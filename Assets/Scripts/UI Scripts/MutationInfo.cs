using UnityEngine;

namespace FOW.Mutations
{
    [CreateAssetMenu(fileName = "NewMutation", menuName = "Mutations/MutationInfo")]
    public class MutationInfo : ScriptableObject
    {
        [Header("Basic Info")]
        public string mutationName;
        [TextArea] public string description;
        public Sprite icon;

        [Header("Slot & Effect Type")]
        public MutationSlotType slotType = MutationSlotType.RightArm;
        public MutationEffectType effectType = MutationEffectType.None;

        [Header("Effect Parameters")]
        public float damageMultiplier = 1f;
        public float slashRangeMultiplier = 1f;
    }

    public enum MutationSlotType
    {
        Head,
        Chest,
        Heart,
        LeftArm,
        RightArm,
        LeftLeg,
        RightLeg
    }

    public enum MutationEffectType
    {
        None,
        DamageBoost,
        RangeBoost,
        Other
    }
}
