using UnityEngine;
using UnityEngine.UI;
using FOW.Mutations;

public class MutationSlotUI : MonoBehaviour
{
    [Header("Slot Setup")]
    public MutationSlotType slotType;
    public Image icon;
    public Sprite emptySprite;

    private MutationInfo currentMutation;
    private MutationInventoryUI inventoryUI;

    public MutationInfo CurrentMutation => currentMutation;

    public void SetInventoryUI(MutationInventoryUI ui)
    {
        inventoryUI = ui;
    }

    public void SetMutation(MutationInfo mutation)
    {
        currentMutation = mutation;

        if (icon == null)
            return;

        if (mutation != null && mutation.icon != null)
        {
            icon.sprite = mutation.icon;
            icon.color = Color.white;
        }
        else
        {
            icon.sprite = emptySprite;
            icon.color = Color.white;
        }
    }

    public void OnClick()
    {
        if (inventoryUI == null)
        {
            Debug.LogWarning($"MutationSlotUI.OnClick on {name}, but inventoryUI is NULL.");
            return;
        }

        inventoryUI.OpenDetail(this);
    }
}
