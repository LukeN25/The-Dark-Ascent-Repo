using UnityEngine;
using FOW.Mutations;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [Header("Panels")]
    public GameObject mutationInventoryPanel;  
    public GameObject mutationDetailPanel;    

    [Header("Slot Containers")]
    public GameObject slotContainer;          

    [Header("Skeleton Preview")]
    public GameObject mutationSkeletonRoot;
    public Camera mutationSkeletonCamera;

    private MutationInventoryUI inventoryUI;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (mutationInventoryPanel != null)
            inventoryUI = mutationInventoryPanel.GetComponent<MutationInventoryUI>();

        CloseAll();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (mutationInventoryPanel != null && !mutationInventoryPanel.activeSelf)
                OpenMutationInventory();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (mutationInventoryPanel != null && mutationInventoryPanel.activeSelf)
            {
                if (inventoryUI != null && mutationDetailPanel != null && mutationDetailPanel.activeSelf)
                {
                    inventoryUI.CloseDetail();
                    return;
                }

                CloseMutationInventory();
            }
        }
    }

    public void OpenMutationInventory()
    {
        if (mutationInventoryPanel != null)
            mutationInventoryPanel.SetActive(true);

        if (slotContainer != null)
            slotContainer.SetActive(true);

        if (mutationDetailPanel != null)
            mutationDetailPanel.SetActive(false);

        if (inventoryUI != null)
            inventoryUI.Show();

        if (mutationSkeletonRoot != null)
            mutationSkeletonRoot.SetActive(true);

        if (mutationSkeletonCamera != null)
            mutationSkeletonCamera.enabled = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseMutationInventory()
    {
        if (inventoryUI != null)
            inventoryUI.Hide();

        if (mutationInventoryPanel != null)
            mutationInventoryPanel.SetActive(false);

        if (slotContainer != null)
            slotContainer.SetActive(false);

        if (mutationDetailPanel != null)
            mutationDetailPanel.SetActive(false);

        if (mutationSkeletonRoot != null)
            mutationSkeletonRoot.SetActive(false);

        if (mutationSkeletonCamera != null)
            mutationSkeletonCamera.enabled = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void CloseAll()
    {
        if (mutationInventoryPanel != null)
            mutationInventoryPanel.SetActive(false);

        if (slotContainer != null)
            slotContainer.SetActive(false);

        if (mutationDetailPanel != null)
            mutationDetailPanel.SetActive(false);

        if (mutationSkeletonRoot != null)
            mutationSkeletonRoot.SetActive(false);

        if (mutationSkeletonCamera != null)
            mutationSkeletonCamera.enabled = false;
    }
}
