using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private PlayerManager PlayerManager;
    private GameObject Player;

    private void Awake()
    {
        Player = GameObject.FindWithTag("Player");
        PlayerManager = Player.GetComponent<PlayerManager>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            if(PlayerManager.playerHealth < 5)
            {
                PlayerManager.playerHealth++;
                Destroy(gameObject);
            }
        }
    }
}
