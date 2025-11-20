using UnityEngine;
using FOW.Mutations;

public class MutationPickup : MonoBehaviour
{
    public MutationInfo mutation;

    private void OnTriggerEnter(Collider other)
    {
        var handler = other.GetComponent<PlayerMutationHandler>();
        if (handler != null)
        {
            MutationInventoryManager.Instance.AddMutation(mutation);
            handler.ApplyMutation(mutation);

            Debug.Log("Player picked up: " + mutation.mutationName);
            Destroy(gameObject);
        }
    }
}
