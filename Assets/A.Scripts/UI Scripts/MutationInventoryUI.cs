using FOW.Mutations;
using UnityEngine;

public class MutationInventoryUI : MonoBehaviour
{
    public GameObject rootCanvas;

    public MutationSlotUI leftArmSlot;
    public MutationSlotUI rightArmSlot;

    private void Start()
    {
        Hide();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (rootCanvas.activeSelf)
                Hide();
            else
                Show();
        }
    }

    public void Show()
    {
        RefreshSlots();
        rootCanvas.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Hide()
    {
        rootCanvas.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void RefreshSlots()
    {
        var inv = MutationInventoryManager.Instance;
        if (inv == null) return;

        if (leftArmSlot != null)
            leftArmSlot.SetSlot(inv.GetEquipped(MutationSlotType.LeftArm));

        if (rightArmSlot != null)
            rightArmSlot.SetSlot(inv.GetEquipped(MutationSlotType.RightArm));
    }
}
