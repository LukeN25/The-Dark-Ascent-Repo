using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MutationDetailPanel : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public Image iconImage;
    public Button unequipButton;

    private MutationSlot currentSlot;

    void Awake()
    {
        gameObject.SetActive(false);
        if (unequipButton != null) unequipButton.onClick.AddListener(OnUnequipClicked);
    }

    public void ShowDetails(MutationSlot slot)
    {
        currentSlot = slot;
        gameObject.SetActive(true);

        if (slot.equippedMutation != null)
        {
            var mut = slot.equippedMutation;
            if (titleText != null) titleText.text = mut.mutationName;
            if (descriptionText != null) descriptionText.text = mut.description;
            if (iconImage != null) iconImage.sprite = mut.icon;
            if (unequipButton != null) unequipButton.gameObject.SetActive(true);
        }
        else
        {
            ShowEmptySlot(slot.partName);
        }
    }

    public void ShowEmptySlot(string partName)
    {
        if (titleText != null) titleText.text = $"{partName} - Empty Slot";
        if (descriptionText != null) descriptionText.text = "No mutation equipped.";
        if (iconImage != null) iconImage.sprite = null;
        if (unequipButton != null) unequipButton.gameObject.SetActive(false);
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
        currentSlot = null;
    }

    void OnUnequipClicked()
    {
        if (currentSlot == null) return;
        MenuManager.Instance.RequestUnequip(currentSlot);
    }
}
