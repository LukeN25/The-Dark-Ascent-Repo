using System.Collections.Generic;
using UnityEngine;

namespace FOW.Mutations
{
    public class MutationInventoryManager : MonoBehaviour
    {
        public static MutationInventoryManager Instance;

        public List<MutationInfo> collectedMutations = new List<MutationInfo>();
        public Dictionary<MutationSlotType, MutationInfo> equippedMutations = new Dictionary<MutationSlotType, MutationInfo>();

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        public void AddMutation(MutationInfo mutation)
        {
            if (!collectedMutations.Contains(mutation))
                collectedMutations.Add(mutation);
        }

        public void Equip(MutationSlotType slot, MutationInfo mutation)
        {
            equippedMutations[slot] = mutation;
        }

        public MutationInfo GetEquipped(MutationSlotType slot)
        {
            return equippedMutations.ContainsKey(slot)
                ? equippedMutations[slot]
                : null;
        }

        public bool CanEquipToSlot(MutationInfo m, MutationSlotType slot)
        {
            foreach (var s in m.allowedSlots)
                if (s == slot)
                    return true;

            return false;
        }
    }
}
