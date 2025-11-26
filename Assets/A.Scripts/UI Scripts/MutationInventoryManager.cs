using System.Collections.Generic;
using UnityEngine;

namespace FOW.Mutations
{
    public class MutationInventoryManager : MonoBehaviour
    {
        public static MutationInventoryManager Instance;

        public List<MutationInfo> collectedMutations = new List<MutationInfo>();

        public Dictionary<MutationSlotType, MutationInfo> equippedMutations =
            new Dictionary<MutationSlotType, MutationInfo>();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void AddMutation(MutationInfo mutation)
        {
            if (mutation == null)
                return;

            if (!collectedMutations.Contains(mutation))
                collectedMutations.Add(mutation);

            AutoEquipIfPossible(mutation);
            RecalculatePlayerStats();
        }

        private void AutoEquipIfPossible(MutationInfo mutation)
        {
            if (mutation.allowedSlots == null || mutation.allowedSlots.Length == 0)
                return;

            foreach (var slot in mutation.allowedSlots)
            {
                if (!equippedMutations.ContainsKey(slot))
                {
                    EquipMutationToSlot(mutation, slot);
                    break;
                }
            }
        }

        public void EquipMutationToSlot(MutationInfo mutation, MutationSlotType slot)
        {
            if (mutation == null)
                return;

            if (mutation.allowedSlots == null ||
                System.Array.IndexOf(mutation.allowedSlots, slot) < 0)
            {
                Debug.LogWarning($"{mutation.mutationName} is not allowed in slot {slot}");
                return;
            }

            equippedMutations[slot] = mutation;
            RecalculatePlayerStats();
        }

        public void UnequipMutationFromSlot(MutationSlotType slot)
        {
            if (equippedMutations.ContainsKey(slot))
                equippedMutations.Remove(slot);

            RecalculatePlayerStats();
        }

        public MutationInfo GetEquipped(MutationSlotType slot)
        {
            if (equippedMutations.TryGetValue(slot, out var mutation))
                return mutation;

            return null;
        }

        private void RecalculatePlayerStats()
        {
            var handler = PlayerMutationHandler.Instance;
            if (handler == null)
                return;

            handler.ResetMutationEffects();

            foreach (var kv in equippedMutations)
            {
                if (kv.Value != null)
                    handler.ApplyMutation(kv.Value);
            }
        }
    }
}
