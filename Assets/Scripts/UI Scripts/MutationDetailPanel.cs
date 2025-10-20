using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MutationDetailPanel : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public Button removeButton;

    private MutationPart currentPart;

    void Awake()
    {
        gameObject.SetActive(false);
        removeButton.onClick.AddListener(OnRemoveClicked);
    }

    public void ShowDetails(MutationPart part)
    {
        currentPart = part;
        gameObject.SetActive(true);
        titleText.text = $"{part.partName} Mutation";
        descriptionText.text = "Example mutation details go here...";
    }

    public void ShowEmptySlot()
    {
        titleText.text = $"{currentPart.partName} - Empty Slot";
        descriptionText.text = "No mutation equipped.";
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    void OnRemoveClicked()
    {
        if (currentPart == null) return;
        MenuManager.Instance.RequestRemoveMutation(currentPart);
    }
}
