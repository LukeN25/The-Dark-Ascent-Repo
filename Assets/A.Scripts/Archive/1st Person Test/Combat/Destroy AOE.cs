using UnityEngine;

public class DestroyAOE : MonoBehaviour
{
    [SerializeField] private float Lifetime;
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if(Lifetime <= timer)
        {
            Destroy(gameObject);
        }
    }
}
