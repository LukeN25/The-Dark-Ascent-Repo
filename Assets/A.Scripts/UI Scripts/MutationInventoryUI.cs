using FOW.Mutations;
using UnityEngine;

public class MutationInventoryUI : MonoBehaviour
{
    public static MutationInventoryUI Instance;

    [Header("Root")]
    public GameObject rootCanvas;

    [Header("Slots")]
    public MutationSlotUI leftArmSlot;
    public MutationSlotUI rightArmSlot;

    private bool isOpen = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        if (rootCanvas != null)
            rootCanvas.SetActive(false);

        isOpen = false;
    }

    private void OnEnable()
    {
        RefreshSlots();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !isOpen)
        {
            Show();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && isOpen)
        {
            Hide();
        }
    }

    public void Show()
    {
        isOpen = true;

        RefreshSlots();

        if (rootCanvas != null)
            rootCanvas.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Hide()
    {
        isOpen = false;

        if (rootCanvas != null)
            rootCanvas.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void RefreshSlots()
    {
        var inv = MutationInventoryManager.Instance;
        if (inv == null) return;

        if (leftArmSlot != null)
            leftArmSlot.SetSlot(inv.GetEquipped(MutationSlotType.LeftArm));

        if (rightArmSlot != null)
            rightArmSlot.SetSlot(inv.GetEquipped(MutationSlotType.RightArm));
    }
}
