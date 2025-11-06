using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [Header("References")]
    public MenuCameraController cameraController;
    public GameObject uiCanvas;
    public GameObject slotPanel; 
    public Button backButton;
    public MutationDetailPanel detailPanel;
    public RemoveConfirmationPanel removePanel;
    public CharacterIdleRotation characterIdleRotation;

    [Header("Settings")]
    public KeyCode toggleKey = KeyCode.Tab;
    public KeyCode backKey = KeyCode.Escape;

    private bool isOpen = false;
    private bool viewingLimb = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        if (backButton != null)
        {
            backButton.onClick.AddListener(BackToMainView);
            backButton.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            if (isOpen) CloseMenu();
            else OpenMenu();
        }

        if (isOpen && viewingLimb && Input.GetKeyDown(backKey))
        {
            BackToMainView();
        }
    }

    public void OpenMenu()
    {
        isOpen = true;
        viewingLimb = false;
        uiCanvas.SetActive(true);
        slotPanel.SetActive(true);
        if (backButton != null) backButton.gameObject.SetActive(false);
        cameraController.ReturnToDefault();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (characterIdleRotation != null)
            characterIdleRotation.SetActive(true);
    }

    public void CloseMenu()
    {
        isOpen = false;
        viewingLimb = false;
        uiCanvas.SetActive(false);
        cameraController.ReturnToDefault();
        detailPanel.ClosePanel();
        removePanel.ClosePanel();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (characterIdleRotation != null)
            characterIdleRotation.SetActive(false);
    }

    public void OnSlotClicked(MutationSlot slot)
    {
        if (slot == null) return;

        
        slotPanel.SetActive(false);
        if (backButton != null) backButton.gameObject.SetActive(true);
        viewingLimb = true;

        
        detailPanel.ShowDetails(slot);

        
        if (cameraController != null && slot.focusPoint != null)
            cameraController.FocusOn(slot.focusPoint);

        if (characterIdleRotation != null)
            characterIdleRotation.SetActive(false);
    }

    public void BackToMainView()
    {
        viewingLimb = false;
        slotPanel.SetActive(true);
        detailPanel.ClosePanel();
        if (backButton != null) backButton.gameObject.SetActive(false);
        cameraController.ReturnToDefault();

        if (characterIdleRotation != null)
            characterIdleRotation.SetActive(true);
    }

    public void RequestUnequip(MutationSlot slot)
    {
        if (slot == null || removePanel == null) return;

        removePanel.ShowConfirmation(confirmed =>
        {
            if (confirmed)
            {
                slot.UnequipMutation();
                detailPanel.ShowEmptySlot(slot.partName);
            }
        });
    }
}
