using UnityEngine;
using TMPro;                

namespace FOW.Mutations
{
    [RequireComponent(typeof(Collider))]
    public class MutationPickup : MonoBehaviour
    {
        [Header("Mutation Data")]
        public MutationInfo mutationData;
        public float rotationSpeed = 50f;

        [Header("Prompt UI")]

        public GameObject promptRoot;
        public TextMeshProUGUI promptNameText;
        public TextMeshProUGUI promptSlotText;
        public string interactKeyText = "E";

        private bool playerInRange = false;

        private void Reset()
        {

            var col = GetComponent<Collider>();
            col.isTrigger = true;
        }

        private void Start()
        {
            if (promptRoot != null)
                promptRoot.SetActive(false);
        }

        private void Update()
        {

            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

            if (!playerInRange) return;

            if (Input.GetKeyDown(KeyCode.E))
            {
                Pickup();
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
            HidePrompt();
        }

        private void ShowPrompt()
        {
            if (promptRoot == null) return;

            promptRoot.SetActive(true);

            if (mutationData != null)
            {
                if (promptNameText != null)
                    promptNameText.text = mutationData.mutationName;

                if (promptSlotText != null)
                {
                    string slotsText = "";
                    if (mutationData.allowedSlots != null && mutationData.allowedSlots.Length > 0)
                    {
                        slotsText = string.Join(", ", mutationData.allowedSlots);
                    }
                    else
                    {
                        slotsText = "Any Slot";
                    }

                    promptSlotText.text = $"Slot: {slotsText}\nPress {interactKeyText} to pick up";
                }
            }
        }

        private void HidePrompt()
        {
            if (promptRoot != null)
                promptRoot.SetActive(false);
        }

        private void Pickup()
        {
            if (mutationData == null) return;

            MutationInventoryManager.Instance?.AddMutation(mutationData);
            Debug.Log($"Picked up mutation: {mutationData.mutationName}");

            HidePrompt();
            Destroy(gameObject);
        }
    }
}
