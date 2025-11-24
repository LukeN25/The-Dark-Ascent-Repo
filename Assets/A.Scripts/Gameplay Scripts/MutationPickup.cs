using UnityEngine;
using UnityEngine.UI;
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
        public GameObject promptCanvas;        
        public TextMeshProUGUI promptNameText; 
        public TextMeshProUGUI promptSlotText; 
        public TextMeshProUGUI pressEText;     
        public Image promptIcon;               

        private bool playerInRange = false;

        private void Start()
        {
            if (promptCanvas != null)
                promptCanvas.SetActive(false);
        }

        private void Update()
        {

            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

            if (playerInRange && Input.GetKeyDown(KeyCode.E))
            {
                MutationInventoryManager.Instance?.AddMutation(mutationData);
                Debug.Log($"Picked up mutation: {mutationData.mutationName}");

                HidePrompt();
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
            HidePrompt();
        }

        private void ShowPrompt()
        {
            if (promptCanvas == null) return;

            promptCanvas.SetActive(true);

            if (promptNameText != null)
                promptNameText.text = mutationData.mutationName;

            if (promptSlotText != null)
            {
                if (mutationData.allowedSlots != null && mutationData.allowedSlots.Length > 0)
                    promptSlotText.text = mutationData.allowedSlots[0].ToString();
                else
                    promptSlotText.text = "Unknown Slot";
            }

            
            if (promptIcon != null)
                promptIcon.sprite = mutationData.icon;

            
            if (pressEText != null)
                pressEText.text = "Press E to pick up";
        }

        private void HidePrompt()
        {
            if (promptCanvas != null)
                promptCanvas.SetActive(false);
        }
    }
}
