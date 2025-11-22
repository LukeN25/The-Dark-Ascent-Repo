using UnityEngine;

namespace FOW.Mutations
{
    [RequireComponent(typeof(Collider))]
    public class MutationPickup : MonoBehaviour
    {
        public MutationInfo mutationData;
        public float rotationSpeed = 50f;

        private void Update()
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            var inv = MutationInventoryManager.Instance;

            inv.AddMutation(mutationData);
            inv.Equip(mutationData);

            Debug.Log($"Picked up and equipped mutation: {mutationData.mutationName}");

            Destroy(gameObject);
        }
    }
}
