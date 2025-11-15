using UnityEngine;


public class CanvasController : MonoBehaviour
{
    [Header("Canvases")]
    public GameObject logbookCanvas;      
    public GameObject mutationCanvas;     

    private bool logbookOpen = false;
    private bool mutationOpen = false;

    void Start()
    {
       
        logbookCanvas.SetActive(false);
        mutationCanvas.SetActive(false);
    }

    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!logbookOpen && !mutationOpen)
                OpenLogbook();
            else if (logbookOpen)
                CloseLogbook();
        }

        
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!logbookOpen && !mutationOpen)
                OpenMutationInventory();
            else if (mutationOpen)
                CloseMutationInventory();
        }

       
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (logbookOpen)
                CloseLogbook();
            else if (mutationOpen)
                CloseMutationInventory();
        }
    }

    

    public void OpenLogbook()
    {
        logbookOpen = true;
        mutationOpen = false;

        mutationCanvas.SetActive(false);
        logbookCanvas.SetActive(true);
    }

    public void CloseLogbook()
    {
        logbookOpen = false;
        logbookCanvas.SetActive(false);
    }

    public void OpenMutationInventory()
    {
        mutationOpen = true;
        logbookOpen = false;

        logbookCanvas.SetActive(false);
        mutationCanvas.SetActive(true);
    }

    public void CloseMutationInventory()
    {
        mutationOpen = false;
        mutationCanvas.SetActive(false);
    }
}
