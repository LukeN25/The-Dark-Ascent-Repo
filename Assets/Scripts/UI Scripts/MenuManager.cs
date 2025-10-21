using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [Header("References")]
    public MenuCameraController cameraController;
    public GameObject uiCanvas;                     
    public MutationDetailPanel detailPanel;
    public RemoveConfirmationPanel removePanel;
    public CharacterIdleRotation characterIdleRotation;

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
        if (uiCanvas != null) uiCanvas.SetActive(true);
        if (cameraController != null) cameraController.ReturnToDefault();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (characterIdleRotation != null) characterIdleRotation.SetActive(true);
        
    }

    public void CloseMenu()
    {
        isOpen = false;
        if (uiCanvas != null) uiCanvas.SetActive(false);
        if (cameraController != null) cameraController.ReturnToDefault();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (detailPanel != null) detailPanel.ClosePanel();
        if (removePanel != null) removePanel.ClosePanel();
        if (characterIdleRotation != null) characterIdleRotation.SetActive(false);
       
    }

    
    public void OnSlotClicked(MutationSlot slot)
    {
        if (slot == null) return;

        if (cameraController != null && slot.focusPoint != null)
            cameraController.FocusOn(slot.focusPoint);

        if (detailPanel != null) detailPanel.ShowDetails(slot);
       
        if (characterIdleRotation != null) characterIdleRotation.SetActive(false);
    }

    
    public void RequestUnequip(MutationSlot slot)
    {
        if (slot == null || removePanel == null) return;

        removePanel.ShowConfirmation(confirmed =>
        {
            if (confirmed)
            {
                slot.UnequipMutation();
                if (detailPanel != null) detailPanel.ShowEmptySlot(slot.partName);
            }
        });
    }
}
