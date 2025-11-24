using UnityEngine;
using UnityEngine.UI;
using FOW.Mutations;
using FOW.Logbook;

public class MutationSlotUI : MonoBehaviour
{
    public MutationSlotType slotType;
    public Image icon;
    public Sprite emptySprite;

    private MutationInfo currentMutation;

    public void SetSlot(MutationInfo mutation)
    {
        currentMutation = mutation;

        if (icon == null) return;

        if (mutation == null)
        {
            icon.sprite = emptySprite;
            icon.color = Color.white;
        }
        else
        {
            icon.sprite = mutation.icon;
            icon.color = Color.white;
        }
    }

    public void OnClick()
    {
        if (currentMutation != null)
        {
            LogbookManager.Instance.OpenMutationDetail(currentMutation);
        }
        else
        {
            Debug.Log($"Slot {slotType} is empty.");
        }
    }
}