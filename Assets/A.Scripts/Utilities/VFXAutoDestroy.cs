using UnityEngine;

public class VFXAutoDestroy : MonoBehaviour
{
    [Tooltip("How long before this VFX object destroys itself.")]
    public float lifetime = 1.5f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
