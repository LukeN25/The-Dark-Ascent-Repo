using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [Header("Panels")]
    public GameObject logbookCanvas;
    public GameObject mutationInventoryCanvas;

    [Header("Slot Panel + Detail Panel")]
    public GameObject mutationSlotPanel;
    public GameObject mutationDetailPanel;

    private bool logbookOpen = false;
    private bool mutationInventoryOpen = false;

    private void Awake()
    {
        Instance = this;
        CloseAll();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!logbookOpen && !mutationInventoryOpen)
                OpenLogbook();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!mutationInventoryOpen && !logbookOpen)
                OpenMutationInventory();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (mutationDetailPanel.activeSelf)
            {
                CloseMutationDetail();
                return;
            }

            if (mutationInventoryOpen)
            {
                CloseMutationInventory();
                return;
            }

            if (logbookOpen)
            {
                CloseLogbook();
                return;
            }
        }
    }

    public void OpenLogbook()
    {
        CloseAll();
        logbookOpen = true;

        logbookCanvas.SetActive(true);
        ShowCursor(true);
    }

    public void CloseLogbook()
    {
        logbookOpen = false;
        logbookCanvas.SetActive(false);
        ShowCursor(false);
    }

    public void OpenMutationInventory()
    {
        CloseAll();
        mutationInventoryOpen = true;

        mutationInventoryCanvas.SetActive(true);
        mutationSlotPanel.SetActive(true);
        mutationDetailPanel.SetActive(false);

        ShowCursor(true);
    }

    public void CloseMutationInventory()
    {
        mutationInventoryOpen = false;

        mutationInventoryCanvas.SetActive(false);
        mutationSlotPanel.SetActive(false);
        mutationDetailPanel.SetActive(false);

        ShowCursor(false);
    }

    public void CloseMutationDetail()
    {
        mutationDetailPanel.SetActive(false);
        mutationSlotPanel.SetActive(true);

        var cam = FindObjectOfType<MenuCameraController>();
        if (cam != null)
            cam.ReturnToDefault();
    }

    public void CloseAll()
    {
        logbookCanvas.SetActive(false);
        mutationInventoryCanvas.SetActive(false);
        mutationSlotPanel.SetActive(false);
        mutationDetailPanel.SetActive(false);
    }

    private void ShowCursor(bool show)
    {
        Cursor.visible = show;
        Cursor.lockState = show ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
