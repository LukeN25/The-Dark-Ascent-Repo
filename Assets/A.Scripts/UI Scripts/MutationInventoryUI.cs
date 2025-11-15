using UnityEngine;

public class MutationInventoryUI : MonoBehaviour
{
    public GameObject rootCanvas;

    private void Start()
    {
        Hide();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (rootCanvas.activeSelf)
                Hide();
            else
                Show();
        }
    }

    public void Show() => rootCanvas.SetActive(true);

    public void Hide() => rootCanvas.SetActive(false);
}
