using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace FOW.Logbook
{
    public class EnemyEntry : MonoBehaviour
    {
        [Header("UI")]
        public RawImage previewImage;
        public GameObject lockOverlay;
        public TextMeshProUGUI enemyNameText;
        public Button button;

        [Header("3D Preview")]
        public Transform previewPivot;
        public Camera previewCamera;
        public float rotationSpeed = 30f;
        public Vector3 modelOffset = Vector3.zero;
        public Vector3 modelRotation = new Vector3(0, 180, 0);

        private EnemyInfo enemyInfo;
        private GameObject previewModel;
        private RenderTexture rt;
        private bool unlocked;

        public void Init(EnemyInfo info, bool isUnlocked)
        {
            enemyInfo = info;
            unlocked = isUnlocked;

            enemyNameText.text = unlocked ? info.enemyName : "???";
            lockOverlay.SetActive(!unlocked);

            if (unlocked)
            {
                SetupPreview();
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() =>
                    LogbookManager.Instance.OpenMutationPanel(enemyInfo)
                );
            }
            else
            {
                previewImage.texture = null;
                button.interactable = false;
            }
        }

        private void SetupPreview()
        {
            if (previewCamera == null || previewPivot == null || enemyInfo.enemyModelPrefab == null)
                return;

            rt = new RenderTexture(512, 512, 16);
            previewCamera.targetTexture = rt;
            previewImage.texture = rt;

            previewModel = Instantiate(enemyInfo.enemyModelPrefab, previewPivot);
            previewModel.transform.localPosition = modelOffset;
            previewModel.transform.localRotation = Quaternion.Euler(modelRotation);

            SetLayerRecursively(previewModel, LayerMask.NameToLayer("LogbookPreview"));
            previewCamera.cullingMask = 1 << LayerMask.NameToLayer("LogbookPreview");

            var anim = previewModel.GetComponent<Animator>();
            if (anim) anim.enabled = false;
        }

        private void Update()
        {
            if (unlocked)
                previewPivot.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }

        private void OnDestroy()
        {
            if (previewModel) Destroy(previewModel);
            if (rt != null) rt.Release();
        }

        private void SetLayerRecursively(GameObject obj, int layer)
        {
            obj.layer = layer;
            foreach (Transform child in obj.transform)
                SetLayerRecursively(child.gameObject, layer);
        }
    }
}
