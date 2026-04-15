using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Panels")]
    public GameObject logbookPanel;            
    public GameObject mutationInventoryPanel;  

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!logbookPanel.activeSelf && !mutationInventoryPanel.activeSelf)
                OpenLogbook();
            else if (logbookPanel.activeSelf)
                CloseAll();
        }

        
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!mutationInventoryPanel.activeSelf && !logbookPanel.activeSelf)
                OpenMutationInventory();
            else if (mutationInventoryPanel.activeSelf)
                CloseAll();
        }

        
        if (Input.GetKeyDown(KeyCode.Escape))
            CloseAll();
    }

    public void OpenLogbook()
    {
        CloseAll();
        logbookPanel.SetActive(true);
    }

    public void OpenMutationInventory()
    {
        CloseAll();
        mutationInventoryPanel.SetActive(true);
    }

    public void CloseAll()
    {
        if (logbookPanel != null) logbookPanel.SetActive(false);
        if (mutationInventoryPanel != null) mutationInventoryPanel.SetActive(false);
    }

    public bool AnyUIOpen()
    {
        return logbookPanel.activeSelf || mutationInventoryPanel.activeSelf;
    }
}
