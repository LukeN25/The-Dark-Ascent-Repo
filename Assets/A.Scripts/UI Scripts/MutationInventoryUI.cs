using UnityEngine;

public class MutationInventoryUI : MonoBehaviour
{
    public GameObject rootCanvas;

    public void Show()
    {
        rootCanvas.SetActive(true);
    }

    public void Hide()
    {
        rootCanvas.SetActive(false);
    }
}
