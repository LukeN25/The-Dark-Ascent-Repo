using UnityEngine;

namespace FOW.Mutations
{
    public class MutationInventoryUI : MonoBehaviour
    {
        [Header("Root Panels")]
        [Tooltip("The main mutation inventory panel (the whole UI for mutations).")]
        public GameObject rootPanel;

        [Tooltip("Container holding all the mutation slots (the skeleton UI with limb buttons).")]
        public GameObject slotsRoot;

        [Tooltip("The panel that shows when you click a mutation slot (name, description, icon, etc.).")]
        public GameObject detailPanelRoot;

        [Header("Slot UIs")]
        public MutationSlotUI headSlot;
        public MutationSlotUI chestSlot;
        public MutationSlotUI heartSlot;
        public MutationSlotUI leftArmSlot;
        public MutationSlotUI rightArmSlot;
        public MutationSlotUI leftLegSlot;
        public MutationSlotUI rightLegSlot;

        [Header("Detail UI")]
        public MutationDetailUI detailUI;

        [Header("Camera Focus Points (per slot)")]
        public Transform headFocusPoint;
        public Transform chestFocusPoint;
        public Transform heartFocusPoint;
        public Transform leftArmFocusPoint;
        public Transform rightArmFocusPoint;
        public Transform leftLegFocusPoint;
        public Transform rightLegFocusPoint;

        private MutationSlotUI currentSlot;
        private MutationInfo currentMutation;

        private void Awake()
        {
            if (headSlot != null) headSlot.SetInventoryUI(this);
            if (chestSlot != null) chestSlot.SetInventoryUI(this);
            if (heartSlot != null) heartSlot.SetInventoryUI(this);
            if (leftArmSlot != null) leftArmSlot.SetInventoryUI(this);
            if (rightArmSlot != null) rightArmSlot.SetInventoryUI(this);
            if (leftLegSlot != null) leftLegSlot.SetInventoryUI(this);
            if (rightLegSlot != null) rightLegSlot.SetInventoryUI(this);
        }

        public void Show()
        {
            if (rootPanel != null)
                rootPanel.SetActive(true);

            if (slotsRoot != null)
                slotsRoot.SetActive(true);

            if (detailPanelRoot != null)
                detailPanelRoot.SetActive(false);

            RefreshSlots();

            var cam = FindObjectOfType<MenuCameraController>();
            if (cam != null)
                cam.ReturnToDefault();
        }

        public void Hide()
        {
            if (rootPanel != null)
                rootPanel.SetActive(false);

            CloseDetail();

            var cam = FindObjectOfType<MenuCameraController>();
            if (cam != null)
                cam.ReturnToDefault();
        }

        public void RefreshSlots()
        {
            var inv = MutationInventoryManager.Instance;
            if (inv == null)
            {
                Debug.LogWarning("MutationInventoryManager.Instance is NULL in RefreshSlots.");
                return;
            }

            if (headSlot != null)
                headSlot.SetMutation(inv.GetEquipped(MutationSlotType.Head));

            if (chestSlot != null)
                chestSlot.SetMutation(inv.GetEquipped(MutationSlotType.Chest));

            if (heartSlot != null)
                heartSlot.SetMutation(inv.GetEquipped(MutationSlotType.Heart));

            if (leftArmSlot != null)
                leftArmSlot.SetMutation(inv.GetEquipped(MutationSlotType.LeftArm));

            if (rightArmSlot != null)
                rightArmSlot.SetMutation(inv.GetEquipped(MutationSlotType.RightArm));

            if (leftLegSlot != null)
                leftLegSlot.SetMutation(inv.GetEquipped(MutationSlotType.LeftLeg));

            if (rightLegSlot != null)
                rightLegSlot.SetMutation(inv.GetEquipped(MutationSlotType.RightLeg));
        }

        public void OpenDetail(MutationSlotUI slotUI)
        {
            if (slotUI == null)
                return;

            currentSlot = slotUI;

            var inv = MutationInventoryManager.Instance;
            if (inv == null)
            {
                Debug.LogWarning("MutationInventoryManager.Instance is NULL in OpenDetail.");
                return;
            }

            currentMutation = inv.GetEquipped(slotUI.slotType);

            if (detailPanelRoot != null)
                detailPanelRoot.SetActive(true);

            if (slotsRoot != null)
                slotsRoot.SetActive(false);

            if (detailUI != null)
            {
                if (currentMutation != null)
                    detailUI.ShowMutation(currentMutation);
                else
                    detailUI.ShowEmpty();
            }

            FocusCameraOnSlot(slotUI.slotType);
        }

        public void CloseDetail()
        {
            if (detailPanelRoot != null)
                detailPanelRoot.SetActive(false);

            if (slotsRoot != null)
                slotsRoot.SetActive(true);

            currentSlot = null;
            currentMutation = null;

            var cam = FindObjectOfType<MenuCameraController>();
            if (cam != null)
                cam.ReturnToDefault();
        }

        private void FocusCameraOnSlot(MutationSlotType slotType)
        {
            var cam = FindObjectOfType<MenuCameraController>();
            if (cam == null)
                return;

            Transform target = null;

            switch (slotType)
            {
                case MutationSlotType.Head:
                    target = headFocusPoint;
                    break;
                case MutationSlotType.Chest:
                    target = chestFocusPoint;
                    break;
                case MutationSlotType.Heart:
                    target = heartFocusPoint;
                    break;
                case MutationSlotType.LeftArm:
                    target = leftArmFocusPoint;
                    break;
                case MutationSlotType.RightArm:
                    target = rightArmFocusPoint;
                    break;
                case MutationSlotType.LeftLeg:
                    target = leftLegFocusPoint;
                    break;
                case MutationSlotType.RightLeg:
                    target = rightLegFocusPoint;
                    break;
            }

            if (target != null)
                cam.FocusOn(target);
            else
                cam.ReturnToDefault();
        }
    }
}
