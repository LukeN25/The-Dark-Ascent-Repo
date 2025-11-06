using System.Collections.Generic;
using UnityEngine;

namespace FOW.Mutations
{
    public class MutationInventoryManager : MonoBehaviour
    {
        public static MutationInventoryManager Instance;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        [Header("Collected Mutations")]
        public List<MutationInfo> collectedMutations = new List<MutationInfo>();

        [Header("Equipped Mutations (Slot -> Mutation)")]
        public Dictionary<MutationSlotType, MutationInfo> equippedMutations = new Dictionary<MutationSlotType, MutationInfo>();

        public void AddMutation(MutationInfo mutation)
        {
            if (!collectedMutations.Contains(mutation))
            {
                collectedMutations.Add(mutation);
                Debug.Log($"Collected new mutation: {mutation.mutationName}");
            }

            MutationMenuUI.Instance?.RefreshMenu();
        }

        public void EquipMutation(MutationInfo mutation)
        {
            equippedMutations[mutation.slotType] = mutation;
            Debug.Log($"Equipped {mutation.mutationName} to {mutation.slotType}");
        }

        public void UnequipMutation(MutationSlotType slot)
        {
            if (equippedMutations.ContainsKey(slot))
                equippedMutations.Remove(slot);
        }

        public MutationInfo GetEquippedMutation(MutationSlotType slot)
        {
            if (equippedMutations.TryGetValue(slot, out var mutation))
                return mutation;
            return null;
        }
    }
}
