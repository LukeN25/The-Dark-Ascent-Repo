using UnityEngine;
using UnityEngine.UI;

public class HeartUI : MonoBehaviour
{
    public Image greyHeart;
    public Image redHeart;

    public void SetFilled(bool filled)
    {
        if (redHeart != null)
            redHeart.enabled = filled;

        
        if (greyHeart != null)
            greyHeart.enabled = true;
    }
}
