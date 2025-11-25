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

        iconImage.sprite = mutation.icon;
        nameText.text = mutation.mutationName;
        descriptionText.text = mutation.description;
    }
}
