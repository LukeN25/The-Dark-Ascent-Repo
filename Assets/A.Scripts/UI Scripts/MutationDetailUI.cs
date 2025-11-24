using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FOW.Mutations;

public class MutationDetailUI : MonoBehaviour
{
    public Image iconImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;

    public void ShowMutation(MutationInfo mutation)
    {
        if (mutation == null) return;

        if (iconImage) iconImage.sprite = mutation.icon;
        if (nameText) nameText.text = mutation.mutationName;
        if (descriptionText) descriptionText.text = mutation.description;
    }
}