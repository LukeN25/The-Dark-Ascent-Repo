using UnityEngine;
using UnityEngine.UI;
using FOW.Mutations;

[RequireComponent(typeof(Collider))]
public class MutationPickup : MonoBehaviour
{
    public MutationInfo mutationData;
    public float rotationSpeed = 50f;

    [Header("Pickup Prompt UI")]
    public GameObject promptRoot;
    public Image iconDisplay;
    public TMPro.TextMeshProUGUI nameText;
    public TMPro.TextMeshProUGUI slotText;
    public TMPro.TextMeshProUGUI pressEText;

    private bool playerInRange = false;

    private void Start()
    {
        if (promptRoot != null)
            promptRoot.SetActive(false);
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            MutationInventoryManager.Instance?.AddMutation(mutationData);
            Debug.Log($"Picked up mutation: {mutationData.mutationName}");
            if (promptRoot != null)
                promptRoot.SetActive(false);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = true;
        ShowPrompt();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = false;
        if (promptRoot != null)
            promptRoot.SetActive(false);
    }

    void ShowPrompt()
    {
        if (promptRoot == null || mutationData == null) return;

        promptRoot.SetActive(true);

        if (iconDisplay != null)
            iconDisplay.sprite = mutationData.icon;

        if (nameText != null)
            nameText.text = mutationData.mutationName;

        if (slotText != null)
        {
            if (mutationData.allowedSlots != null && mutationData.allowedSlots.Length > 0)
            {
                string slotList = "";
                for (int i = 0; i < mutationData.allowedSlots.Length; i++)
                {
                    slotList += mutationData.allowedSlots[i].ToString();
                    if (i < mutationData.allowedSlots.Length - 1)
                        slotList += " OR ";
                }
                slotText.text = "Can be equipped on: " + slotList;
            }
            else
            {
                slotText.text = "Can be equipped on: Unknown Slot";
            }
        }

        if (pressEText != null)
            pressEText.text = "Press <E> to pick up";
    }
}