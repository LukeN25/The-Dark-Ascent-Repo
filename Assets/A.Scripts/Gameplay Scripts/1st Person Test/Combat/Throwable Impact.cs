using UnityEngine;

public class ThrowableImpact : MonoBehaviour
{

    [SerializeField] private GameObject AOE;

    
    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 position = contact.point;
        Instantiate(AOE, position, rotation);
        Destroy(gameObject);
    }
}
