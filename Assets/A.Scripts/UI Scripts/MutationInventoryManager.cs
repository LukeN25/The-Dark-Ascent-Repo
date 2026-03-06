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

            MutationInventoryUI ui = FindFirstObjectByType<MutationInventoryUI>();
            if (ui != null)
                ui.RefreshSlots();
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
                    return;
                }
            }
        }

        public void EquipMutationToSlot(MutationInfo mutation, MutationSlotType slot)
        {
            if (mutation == null)
                return;

            if (System.Array.IndexOf(mutation.allowedSlots, slot) < 0)
            {
                Debug.LogWarning($"{mutation.mutationName} cannot be equipped in {slot}");
                return;
            }

            equippedMutations[slot] = mutation;
            RecalculatePlayerStats();

            MutationInventoryUI ui = FindFirstObjectByType<MutationInventoryUI>();
            if (ui != null)
                ui.RefreshSlots();
        }

        public void UnequipMutationFromSlot(MutationSlotType slot)
        {
            if (equippedMutations.ContainsKey(slot))
                equippedMutations.Remove(slot);

            RecalculatePlayerStats();

            MutationInventoryUI ui = FindFirstObjectByType<MutationInventoryUI>();
            if (ui != null)
                ui.RefreshSlots();
        }

        public MutationInfo GetEquipped(MutationSlotType slot)
        {
            if (equippedMutations.TryGetValue(slot, out var m))
                return m;

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
