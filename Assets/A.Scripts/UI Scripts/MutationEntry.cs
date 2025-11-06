using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FOW.Logbook;
using FOW.Mutations;


namespace FOW.Mutations
{
    public class MutationEntry : MonoBehaviour
    {
        [Header("UI References")]
        public Image mutationIcon;
        public Button mutationButton;
        public GameObject lockedOverlay;
        public TextMeshProUGUI mutationNameText;

        private MutationInfo mutationInfo;
        private bool isUnlocked;

        public void Initialize(MutationInfo info, bool unlocked)
        {
            mutationInfo = info;
            isUnlocked = unlocked;

            mutationNameText.text = unlocked ? info.mutationName : "???";
            mutationIcon.sprite = unlocked ? info.icon : null; 
            mutationIcon.enabled = unlocked;
            lockedOverlay.SetActive(!unlocked);

            mutationButton.onClick.RemoveAllListeners();

            if (unlocked)
                mutationButton.onClick.AddListener(OpenMutationDetail);
            else
                mutationButton.interactable = false;
        }

        private void OpenMutationDetail()
        {
            if (mutationInfo != null)
                LogbookManager.Instance.OpenMutationDetail(mutationInfo); 
        }
    }
}
