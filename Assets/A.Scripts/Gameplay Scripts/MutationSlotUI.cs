using UnityEngine;
using UnityEngine.UI;
using FOW.Mutations;

public class MutationSlotUI : MonoBehaviour
{
    public MutationSlotType slotType;
    public Image icon;

    public void SetSlot(MutationInfo mutation)
    {
        if (mutation == null)
        {
            icon.enabled = false;
            return;
        }

        icon.sprite = mutation.icon;
        icon.enabled = true;
    }
}
