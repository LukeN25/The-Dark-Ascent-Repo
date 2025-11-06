using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class MutationSlot : MonoBehaviour
{
    [Header("Slot Info")]
    public string partName;
    public Transform focusPoint;                

    [Header("UI")]
    public Image iconImage;                    
    public Sprite emptySlotSprite;             

    [Header("Mutation Data")]
    public MutationData equippedMutation;       

    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
        RefreshSlotVisual();
    }

    public void EquipMutation(MutationData mutation)
    {
        equippedMutation = mutation;
        RefreshSlotVisual();
    }

    public void UnequipMutation()
    {
        equippedMutation = null;
        RefreshSlotVisual();
    }

    void OnClick()
    {
        if (!MenuManager.Instance) return;
        MenuManager.Instance.OnSlotClicked(this);
    }

    public void RefreshSlotVisual()
    {
        if (iconImage == null) return;

        if (equippedMutation != null && equippedMutation.icon != null)
            iconImage.sprite = equippedMutation.icon;
        else
            iconImage.sprite = emptySlotSprite;
    }
}
