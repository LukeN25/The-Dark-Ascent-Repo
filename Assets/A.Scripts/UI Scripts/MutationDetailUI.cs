using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FOW.Mutations;


public class MutationDetailUI : MonoBehaviour
{
    [Header("UI References")]
    public Image iconImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;

    public void ShowMutation(MutationInfo mutation)
    {
        if (mutation == null)
        {
            Debug.LogError(" Mutation is null in MutationDetailUI.ShowMutation()");
            return;
        }

        if (iconImage != null)
            iconImage.sprite = mutation.icon;

        if (nameText != null)
            nameText.text = mutation.mutationName;

        if (descriptionText != null)
            descriptionText.text = mutation.description;

        Debug.Log(" Mutation detail panel updated: " + mutation.mutationName);
    }
}
