using UnityEngine;
using UnityEngine.UI;
using FOW.Mutations;

public class MutationSlotUI : MonoBehaviour
{
    public MutationSlotType slotType;
    public Image icon;
    public Sprite emptySprite;

    private MutationInfo currentMutation;
    private MutationInventoryUI inventoryUI;

    public void SetInventoryUI(MutationInventoryUI ui)
    {
        inventoryUI = ui;
    }

    public void SetMutation(MutationInfo mutation)
    {
        currentMutation = mutation;

        if (icon == null) return;

        if (mutation == null)
        {
            icon.sprite = emptySprite;
        }
        else
        {
            icon.sprite = mutation.icon;
        }
    }

    public void OnClick()
    {
        if (inventoryUI != null)
            inventoryUI.OpenDetail(this);
    }
}
