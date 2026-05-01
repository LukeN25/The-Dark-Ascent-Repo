using UnityEngine;

public class GrenadePickup : MonoBehaviour
{
    private Throwing Throwing;
    private GameObject Player;

    private void Awake()
    {
        Player = GameObject.FindWithTag("Player");
        Throwing = Player.GetComponent<Throwing>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            if (Throwing.totalThrows < 5)
            {
                Throwing.totalThrows++;
                Destroy(gameObject);
            }
        }
    }
}
