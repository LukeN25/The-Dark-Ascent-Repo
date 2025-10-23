using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MutationDetailPanel : MonoBehaviour
{
    [Header("UI References")]
    public CanvasGroup canvasGroup;
    public Image icon;
    public TMP_Text nameText;
    public TMP_Text descriptionText;
    public Button unequipButton;

    private MutationSlot currentSlot;

    void Awake()
    {
        HideInstantly();
        if (unequipButton != null)
            unequipButton.onClick.AddListener(OnUnequipClicked);
    }

    public void ShowDetails(MutationSlot slot)
    {
        currentSlot = slot;
        if (slot.equippedMutation != null)
        {
            icon.sprite = slot.equippedMutation.icon;
            nameText.text = slot.equippedMutation.mutationName;
            descriptionText.text = slot.equippedMutation.description;
            if (unequipButton != null) unequipButton.gameObject.SetActive(true);
        }
        else
        {
            ShowEmptySlot(slot.partName);
        }

       
        gameObject.SetActive(false);
        gameObject.SetActive(true);

        
        StopAllCoroutines();
        ShowInstantly();
    }

    public void ShowEmptySlot(string partName)
    {
        if (icon != null) icon.sprite = null;
        nameText.text = partName;
        descriptionText.text = "No mutation equipped.";
        if (unequipButton != null) unequipButton.gameObject.SetActive(false);

        StopAllCoroutines();
        ShowInstantly();
    }

    public void ClosePanel()
    {
        currentSlot = null;
        StopAllCoroutines();
        HideInstantly();
    }

    private void OnUnequipClicked()
    {
        if (currentSlot != null)
        {
            MenuManager.Instance.RequestUnequip(currentSlot);
        }
    }

    private void ShowInstantly()
    {
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
    }

    private void HideInstantly()
    {
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }
}
