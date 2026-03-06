using UnityEngine;

namespace FOW.Mutations
{
    public class MutationInventoryUI : MonoBehaviour
    {
        [Header("Root Panels")]
        public GameObject rootPanel;
        public GameObject slotsRoot;
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

        private bool inventoryOpen = false;

        private void Awake()
        {
            headSlot?.SetInventoryUI(this);
            chestSlot?.SetInventoryUI(this);
            heartSlot?.SetInventoryUI(this);
            leftArmSlot?.SetInventoryUI(this);
            rightArmSlot?.SetInventoryUI(this);
            leftLegSlot?.SetInventoryUI(this);
            rightLegSlot?.SetInventoryUI(this);
        }

        private void Update()
        {

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (!inventoryOpen)
                    Show();
            }

            if (!inventoryOpen)
                return;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Hide();
            }
        }

        public void Show()
        {
            inventoryOpen = true;

            rootPanel?.SetActive(true);
            slotsRoot?.SetActive(true);
            detailPanelRoot?.SetActive(false);

            RefreshSlots();

            var cam = FindFirstObjectByType<MenuCameraController>();
            cam?.ReturnToDefault();

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        public void Hide()
        {
            inventoryOpen = false;

            rootPanel?.SetActive(false);
            detailPanelRoot?.SetActive(false);
            slotsRoot?.SetActive(false);

            var cam = FindFirstObjectByType<MenuCameraController>();
            cam?.ReturnToDefault();

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void RefreshSlots()
        {
            var inv = MutationInventoryManager.Instance;
            if (inv == null) return;

            headSlot?.SetMutation(inv.GetEquipped(MutationSlotType.Head));
            chestSlot?.SetMutation(inv.GetEquipped(MutationSlotType.Chest));
            heartSlot?.SetMutation(inv.GetEquipped(MutationSlotType.Heart));
            leftArmSlot?.SetMutation(inv.GetEquipped(MutationSlotType.LeftArm));
            rightArmSlot?.SetMutation(inv.GetEquipped(MutationSlotType.RightArm));
            leftLegSlot?.SetMutation(inv.GetEquipped(MutationSlotType.LeftLeg));
            rightLegSlot?.SetMutation(inv.GetEquipped(MutationSlotType.RightLeg));
        }

        public void OpenDetail(MutationSlotUI slotUI)
        {
            currentSlot = slotUI;

            var inv = MutationInventoryManager.Instance;
            currentMutation = inv.GetEquipped(slotUI.slotType);

            slotsRoot?.SetActive(false);
            detailPanelRoot?.SetActive(true);

            if (currentMutation != null)
                detailUI.ShowMutation(currentMutation);
            else
                detailUI.ShowEmpty();

            FocusCameraOnSlot(slotUI.slotType);
        }

        public void CloseDetail()
        {
            detailPanelRoot?.SetActive(false);
            slotsRoot?.SetActive(true);

            var cam = FindFirstObjectByType<MenuCameraController>();
            cam?.ReturnToDefault();
        }

        private void FocusCameraOnSlot(MutationSlotType slotType)
        {
            var cam = FindFirstObjectByType<MenuCameraController>();
            if (cam == null) return;

            Transform target = slotType switch
            {
                MutationSlotType.Head => headFocusPoint,
                MutationSlotType.Chest => chestFocusPoint,
                MutationSlotType.Heart => heartFocusPoint,
                MutationSlotType.LeftArm => leftArmFocusPoint,
                MutationSlotType.RightArm => rightArmFocusPoint,
                MutationSlotType.LeftLeg => leftLegFocusPoint,
                MutationSlotType.RightLeg => rightLegFocusPoint,
                _ => null
            };

            if (target != null) cam.FocusOn(target);
        }

        public void OnBackFromDetailButton() => CloseDetail();

        public void OnBackFromSlotsButton() => Hide();
    }
}
