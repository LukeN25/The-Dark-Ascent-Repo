using UnityEngine;

public class AutoDespawn : MonoBehaviour
{
    float timer;

    void Update()
    {
        timer += Time.deltaTime;
        
        if(timer >= 20)
        {
            Destroy(gameObject);
        }
    }
}
