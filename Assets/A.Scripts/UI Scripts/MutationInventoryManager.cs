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

        public void EquipMutation(MutationInfo mutation)
        {
            equippedMutations[mutation.slotType] = mutation;
        }

        public void UnequipMutation(MutationSlotType slot)
        {
            if (equippedMutations.ContainsKey(slot))
                equippedMutations.Remove(slot);
        }
    }
}
