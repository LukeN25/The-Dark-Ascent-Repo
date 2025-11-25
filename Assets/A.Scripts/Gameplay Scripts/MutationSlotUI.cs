using UnityEngine;
using UnityEngine.UI;
using FOW.Mutations;

public class MutationSlotUI : MonoBehaviour
{
    public MutationSlotType slotType;
    public Image icon;
    public Sprite emptySprite;

    private MutationInfo currentMutation;

    public void SetSlot(MutationInfo mutation)
    {
        currentMutation = mutation;

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
            MenuManager.Instance.OpenMutationDetail(currentMutation);
        else
            Debug.Log("No mutation equipped.");
    }
}
