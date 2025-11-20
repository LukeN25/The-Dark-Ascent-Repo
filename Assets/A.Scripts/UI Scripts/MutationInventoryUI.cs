using UnityEngine;
using UnityEngine.UI;
using FOW.Mutations;

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
            if (rootCanvas.activeSelf) Hide();
            else Show();
        }
    }

    public void Show()
    {
        rootCanvas.SetActive(true);
        RefreshSlots();
    }

    public void Hide()
    {
        rootCanvas.SetActive(false);
    }

    public void RefreshSlots()
    {
        var inv = MutationInventoryManager.Instance;

        var left = inv.GetEquipped(MutationSlotType.LeftArm);
        leftArmSlot.icon.sprite = left ? left.icon : null;
        leftArmSlot.icon.enabled = left;

        var right = inv.GetEquipped(MutationSlotType.RightArm);
        rightArmSlot.icon.sprite = right ? right.icon : null;
        rightArmSlot.icon.enabled = right;
    }
}
