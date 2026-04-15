using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FOW.Mutations;

public class MutationDetailUI : MonoBehaviour
{
    [Header("Panel Root")]
    public GameObject panelRoot;

    [Header("UI References")]
    public Image iconImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;

    private void Awake()
    {
        if (panelRoot != null)
            panelRoot.SetActive(false);
    }

    public void ShowMutation(MutationInfo mutation)
    {
        if (mutation == null)
        {
            Debug.LogWarning("MutationDetailUI.ShowMutation called with null mutation.");
            return;
        }

        Debug.Log("MutationDetailUI showing: " + mutation.mutationName);

        if (iconImage != null)
            iconImage.sprite = mutation.icon;

        if (nameText != null)
            nameText.text = mutation.mutationName;

        if (descriptionText != null)
            descriptionText.text = mutation.description;

        if (panelRoot != null)
            panelRoot.SetActive(true);
    }

    public void ShowEmpty()
    {
        if (iconImage != null)
            iconImage.sprite = null;

        if (nameText != null)
            nameText.text = "No mutation equipped";

        if (descriptionText != null)
            descriptionText.text = "";

        if (panelRoot != null)
            panelRoot.SetActive(true);
    }

    public void Hide()
    {
        if (panelRoot != null)
            panelRoot.SetActive(false);
    }
}
