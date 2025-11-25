using UnityEngine;
using UnityEngine.UI;
using FOW.Mutations;

public class MutationSlot : MonoBehaviour
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
        if (currentMutation == null)
        {
            Debug.Log("No mutation equipped in: " + slotType);
            return;
        }

        MenuManager manager = FindObjectOfType<MenuManager>();

        if (manager != null && manager.mutationDetailUI != null)
        {
            manager.mutationDetailUI.gameObject.SetActive(true);
            manager.mutationDetailUI.ShowMutation(currentMutation);
        }
        else
        {
            Debug.LogWarning("MenuManager or MutationDetailUI missing from scene!");
        }
    }
}
