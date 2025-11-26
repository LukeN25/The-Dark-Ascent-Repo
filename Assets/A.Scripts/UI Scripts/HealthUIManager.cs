using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIManager : MonoBehaviour
{
    public PlayerManager player;

    [Header("Prefabs & Containers")]
    public GameObject heartPrefab;      
    public GameObject greyHeartPrefab;   
    public Transform heartContainer;

    private List<Image> redHearts = new List<Image>();
    private List<Image> greyHearts = new List<Image>();

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

        if (HealthChanged())
        {
            RefreshHearts();
        }
    }

    bool HealthChanged()
    {
        int cur = GetCurrentHealth();
        int max = GetMaxHealth();

        if (cur != lastHealth || max != lastMaxHealth)
        {
            lastHealth = cur;
            lastMaxHealth = max;
            return true;
        }

        return false;
    }

    int GetCurrentHealth()
    {
        var field = typeof(PlayerManager).GetField("playerHealth",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        int hp = (int)field.GetValue(player);
        return Mathf.Max(0, hp);
    }

    int GetMaxHealth()
    {

        return 5; 
    }

    void RefreshHearts()
    {
        int current = GetCurrentHealth();
        int max = GetMaxHealth();

        while (greyHearts.Count < max)
        {
            var newGrey = Instantiate(greyHeartPrefab, heartContainer);
            greyHearts.Add(newGrey.GetComponent<Image>());
        }

        while (greyHearts.Count > max)
        {
            Destroy(greyHearts[greyHearts.Count - 1].gameObject);
            greyHearts.RemoveAt(greyHearts.Count - 1);
        }

        while (redHearts.Count < current)
        {
            var newRed = Instantiate(heartPrefab, heartContainer);
            redHearts.Add(newRed.GetComponent<Image>());
        }

        while (redHearts.Count > current)
        {
            Destroy(redHearts[redHearts.Count - 1].gameObject);
            redHearts.RemoveAt(redHearts.Count - 1);
        }

        for (int i = 0; i < greyHearts.Count; i++)
            greyHearts[i].transform.SetSiblingIndex(i);

        for (int i = 0; i < redHearts.Count; i++)
            redHearts[i].transform.SetSiblingIndex(i);
    }
}
