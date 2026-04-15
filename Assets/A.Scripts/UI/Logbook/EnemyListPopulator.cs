using UnityEngine;
using FOW.Logbook;


public class EnemyListPopulator : MonoBehaviour
{
    public Transform gridParent;
    public GameObject enemyEntryPrefab;

    private void OnEnable()
    {
        if (LogbookManager.Instance == null)
        {
            Debug.LogError(" LogbookManager.Instance is NULL!");
            return;
        }

        Populate();
    }

    private void Populate()
    {
        foreach (Transform child in gridParent)
            Destroy(child.gameObject);

        var list = LogbookManager.Instance.enemies;

        if (list == null)
        {
            Debug.LogError(" LogbookManager.enemies is NULL. Wrong LogbookManager script loaded?");
            return;
        }

        foreach (var enemy in list)
        {
            var entry = Instantiate(enemyEntryPrefab, gridParent);
            entry.GetComponent<EnemyEntry>().Init(enemy, enemy.isUnlocked);
        }
    }
}
