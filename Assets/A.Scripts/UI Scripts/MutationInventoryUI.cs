using UnityEngine;

public class MutationInventoryUI : MonoBehaviour
{
    public GameObject rootCanvas;

    public bool IsOpen => rootCanvas.activeSelf;

    private void Start()
    {
        Hide();
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!IsOpen)
                Show();
            else
                Hide();
        }

        
        if (IsOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            Hide();
        }
    }

    public void Show() => rootCanvas.SetActive(true);

    public void Hide() => rootCanvas.SetActive(false);
}
