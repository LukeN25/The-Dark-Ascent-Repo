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
}
