using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIManager : MonoBehaviour
{
    public PlayerManager player;

    public GameObject fullHeartPrefab;   
    public GameObject emptyHeartPrefab;  
    public Transform heartContainer;

    private List<GameObject> spawnedHearts = new List<GameObject>();

    private int lastHealth = -1;
    private int lastMaxHealth = -1;

    private void Start()
    {
        if (player == null)
            player = PlayerManager.Instance;

        RefreshHearts();
    }

    private void Update()
    {
        if (player == null) return;

        if (player.GetHealth() != lastHealth ||
            player.GetMaxHealth() != lastMaxHealth)
        {
            RefreshHearts();
        }
    }

    void RefreshHearts()
    {
        int hp = player.GetHealth();
        int maxHp = player.GetMaxHealth();

        lastHealth = hp;
        lastMaxHealth = maxHp;

        foreach (var h in spawnedHearts)
            Destroy(h);
        spawnedHearts.Clear();

        for (int i = 0; i < maxHp; i++)
        {
            GameObject heartObj;

            if (i < hp)
                heartObj = Instantiate(fullHeartPrefab, heartContainer);
            else
                heartObj = Instantiate(emptyHeartPrefab, heartContainer);

            spawnedHearts.Add(heartObj);
        }
    }
}
