using System.Collections.Generic;
using UnityEngine;

namespace FOW.Mutations
{
    public class MutationInventoryManager : MonoBehaviour
    {
        public static MutationInventoryManager Instance;

        public List<MutationInfo> collectedMutations = new List<MutationInfo>();

        public Dictionary<MutationSlotType, MutationInfo> equipped = new Dictionary<MutationSlotType, MutationInfo>();

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

        public void Equip(MutationInfo mutation)
        {
            foreach (var slot in mutation.allowedSlots)
            {
                equipped[slot] = mutation;
                Debug.Log($"Equipped {mutation.mutationName} in slot {slot}");
                return;
            }

            Debug.LogWarning("Mutation has no allowed slots defined.");
        }

        public MutationInfo GetEquipped(MutationSlotType slot)
        {
            if (equipped.ContainsKey(slot))
                return equipped[slot];

            return null;
        }
    }
}
