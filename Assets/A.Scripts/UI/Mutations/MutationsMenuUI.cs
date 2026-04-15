using UnityEngine;

namespace FOW.Mutations
{
    public class MutationMenuUI : MonoBehaviour
    {
        public static MutationMenuUI Instance;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        public void RefreshMenu()
        {
            Debug.Log("MutationMenuUI refreshed (placeholder).");
        }
    }
}
