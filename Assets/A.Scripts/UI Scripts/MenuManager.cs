using UnityEngine;
using FOW.Mutations;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [Header("Panels")]
    [Tooltip("Root panel for the mutation inventory UI (same as MutationInventoryUI.rootPanel).")]
    public GameObject mutationInventoryPanel;

    [Header("Skeleton Preview")]
    public GameObject mutationSkeletonRoot;
    public Camera mutationSkeletonCamera;

    private MutationInventoryUI mutationInventoryUI;

    private void Awake()
    {
        Instance = this;
        if (mutationInventoryPanel != null)
            mutationInventoryUI = mutationInventoryPanel.GetComponent<MutationInventoryUI>();
    }

    private void Start()
    {
        CloseMutationInventory();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!mutationInventoryPanel.activeSelf)
                OpenMutationInventory();
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (mutationInventoryPanel.activeSelf)
                CloseMutationInventory();
        }
    }

    public void OpenMutationInventory()
    {

        if (mutationInventoryUI != null)
            mutationInventoryUI.Show();
        else if (mutationInventoryPanel != null)
            mutationInventoryPanel.SetActive(true);

        if (mutationSkeletonRoot != null)
            mutationSkeletonRoot.SetActive(true);

        if (mutationSkeletonCamera != null)
            mutationSkeletonCamera.enabled = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }

    public void CloseMutationInventory()
    {
        if (mutationInventoryUI != null)
            mutationInventoryUI.Hide();
        else if (mutationInventoryPanel != null)
            mutationInventoryPanel.SetActive(false);

        if (mutationSkeletonRoot != null)
            mutationSkeletonRoot.SetActive(false);

        if (mutationSkeletonCamera != null)
            mutationSkeletonCamera.enabled = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
    }
}
