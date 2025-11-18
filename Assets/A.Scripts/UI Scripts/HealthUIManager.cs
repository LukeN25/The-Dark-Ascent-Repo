using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIManager : MonoBehaviour
{
    public PlayerManager player;
    public GameObject heartPrefab;
    public Transform heartContainer;

    private List<Image> heartImages = new List<Image>();
    private int lastHealth = -1;

    private void Start()
    {
        if (player == null)
            player = PlayerManager.Instance;

        RefreshHearts();
    }

    private void Update()
    {
        if (player == null) return;

        if (playerHealthChanged())
        {
            RefreshHearts();
        }
    }

    bool playerHealthChanged()
    {
        int current = playerHealth();
        if (current != lastHealth)
        {
            lastHealth = current;
            return true;
        }
        return false;
    }

    int playerHealth()
    {
        var field = typeof(PlayerManager).GetField("playerHealth",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        int hp = (int)field.GetValue(player);

        
        if (hp < 0) hp = 0;

        return hp;
    }

    void RefreshHearts()
    {
        int heartsNeeded = playerHealth();

        
        heartsNeeded = Mathf.Max(0, heartsNeeded);

        
        while (heartImages.Count < heartsNeeded)
        {
            var newHeart = Instantiate(heartPrefab, heartContainer);
            heartImages.Add(newHeart.GetComponent<Image>());
        }

        
        while (heartImages.Count > heartsNeeded)
        {
            Destroy(heartImages[heartImages.Count - 1].gameObject);
            heartImages.RemoveAt(heartImages.Count - 1);
        }
    }
}
