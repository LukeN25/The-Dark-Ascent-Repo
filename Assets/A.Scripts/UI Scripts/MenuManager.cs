using UnityEngine;
using FOW.Mutations;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [Header("Panels")]
    [Tooltip("Root object for the logbook UI (CanvasLogbook or similar).")]
    public GameObject logbookPanel;

    [Tooltip("Root object for the mutation inventory UI (MutationCanvas root).")]
    public GameObject mutationInventoryPanel;

    [Header("Mutation Inventory UI")]
    [Tooltip("The MutationInventoryUI script on your mutation inventory root.")]
    public MutationInventoryUI mutationInventoryUI;

    [Header("Logbook Mutation Detail (optional)")]
    [Tooltip("Detail panel used by the LOGBOOK (not the mutation inventory).")]
    public GameObject mutationDetailPanel;
    public MutationDetailUI mutationDetailUI;

    private bool isLogbookOpen = false;
    private bool isMutationInventoryOpen = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isMutationInventoryOpen)
        {
            ToggleLogbook();
        }

        if (Input.GetKeyDown(KeyCode.Tab) && !isLogbookOpen && !isMutationInventoryOpen)
        {
            OpenMutationInventory();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isMutationInventoryOpen)
            {
                CloseMutationInventory();
            }
            else if (isLogbookOpen)
            {
                CloseLogbook();
            }
        }
    }


    private void ToggleLogbook()
    {
        if (isLogbookOpen) CloseLogbook();
        else OpenLogbook();
    }

    public void OpenLogbook()
    {
        isLogbookOpen = true;

        if (logbookPanel != null)
            logbookPanel.SetActive(true);
    }

    public void CloseLogbook()
    {
        isLogbookOpen = false;

        if (logbookPanel != null)
            logbookPanel.SetActive(false);

        if (mutationDetailPanel != null)
            mutationDetailPanel.SetActive(false);
    }


    public void OpenMutationInventory()
    {
        isMutationInventoryOpen = true;

        if (mutationInventoryUI != null)
        {
            mutationInventoryUI.Show();
        }
        else if (mutationInventoryPanel != null)
        {
            mutationInventoryPanel.SetActive(true);
        }

    }

    public void CloseMutationInventory()
    {
        isMutationInventoryOpen = false;

        if (mutationInventoryUI != null)
        {
            mutationInventoryUI.Hide();
        }
        else if (mutationInventoryPanel != null)
        {
            mutationInventoryPanel.SetActive(false);
        }
    }


    public void OpenMutationDetail(MutationInfo mutation)
    {
        if (mutationDetailPanel != null)
            mutationDetailPanel.SetActive(true);

        if (mutationDetailUI != null)
            mutationDetailUI.ShowMutation(mutation);
    }

    public void CloseDetail()
    {
        if (mutationDetailPanel != null)
            mutationDetailPanel.SetActive(false);
    }
}
