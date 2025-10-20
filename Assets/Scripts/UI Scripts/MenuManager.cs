using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [Header("References")]
    public MenuCameraController cameraController;
    public GameObject uiCanvas;
    public MutationDetailPanel detailPanel;
    public RemoveConfirmationPanel removePanel;

    [Header("Settings")]
    public KeyCode toggleKey = KeyCode.Tab;

    private bool isOpen = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            if (isOpen) CloseMenu();
            else OpenMenu();
        }
    }

    public void OpenMenu()
    {
        isOpen = true;
        uiCanvas.SetActive(true);
        cameraController.ReturnToDefault();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
       
    }

    public void CloseMenu()
    {
        isOpen = false;
        uiCanvas.SetActive(false);
        cameraController.ReturnToDefault();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        detailPanel.ClosePanel();
        removePanel.ClosePanel();
        
    }

    public void OnPartClicked(MutationPart part)
    {
        cameraController.FocusOn(part.focusPoint);
        detailPanel.ShowDetails(part);
    }

    public void RequestRemoveMutation(MutationPart part)
    {
        removePanel.ShowConfirmation(confirmed =>
        {
            if (confirmed)
            {
                Debug.Log($"Removed mutation from {part.partName}");
                detailPanel.ShowEmptySlot();
            }
        });
    }
}
