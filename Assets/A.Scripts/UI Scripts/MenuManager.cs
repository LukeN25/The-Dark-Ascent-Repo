using UnityEngine;
using FOW.Mutations;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [Header("Panels")]
    public GameObject logbookPanel;
    public GameObject mutationInventoryPanel;
    public GameObject mutationDetailPanel;

    [Header("Detail UI")]
    public MutationDetailUI mutationDetailUI;

    private bool isLogbookOpen = false;
    private bool isMutationInventoryOpen = false;
    private bool isDetailOpen = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isMutationInventoryOpen && !isDetailOpen)
            ToggleLogbook();

        if (Input.GetKeyDown(KeyCode.Tab) && !isLogbookOpen && !isDetailOpen)
            ToggleMutationInventory();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isDetailOpen) CloseDetail();
            else if (isLogbookOpen) CloseLogbook();
            else if (isMutationInventoryOpen) CloseMutationInventory();
        }
    }

    void ToggleLogbook()
    {
        if (isLogbookOpen) CloseLogbook();
        else OpenLogbook();
    }

    public void OpenLogbook()
    {
        isLogbookOpen = true;
        logbookPanel.SetActive(true);
    }

    public void CloseLogbook()
    {
        isLogbookOpen = false;
        logbookPanel.SetActive(false);
        CloseDetail();
    }

    void ToggleMutationInventory()
    {
        if (isMutationInventoryOpen) CloseMutationInventory();
        else OpenMutationInventory();
    }

    public void OpenMutationInventory()
    {
        isMutationInventoryOpen = true;
        mutationInventoryPanel.SetActive(true);
    }

    public void CloseMutationInventory()
    {
        isMutationInventoryOpen = false;
        mutationInventoryPanel.SetActive(false);
        CloseDetail();
    }

    public void OpenMutationDetail(MutationInfo mutation)
    {
        isDetailOpen = true;

        mutationDetailPanel.SetActive(true);
        mutationDetailUI.ShowMutation(mutation);
    }

    public void CloseDetail()
    {
        isDetailOpen = false;
        mutationDetailPanel.SetActive(false);
    }
}
