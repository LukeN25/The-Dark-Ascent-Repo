using UnityEngine;
using FOW.Logbook;

public class EnemyListPopulator : MonoBehaviour
{
    [Header("UI References")]
    public Transform gridParent;
    public GameObject enemyEntryPrefab;

    private void OnEnable()
    {
        Populate();
    }

    public void Populate()
    {
       
        if (LogbookManager.Instance == null)
        {
            Debug.LogError(" LogbookManager.Instance is NULL. Add LogbookManager to the scene.");
            return;
        }

        if (gridParent == null)
        {
            Debug.LogError(" gridParent is NULL. Assign your GridLayoutGroup object in the inspector.");
            return;
        }

        if (enemyEntryPrefab == null)
        {
            Debug.LogError(" enemyEntryPrefab is NULL. Assign your EnemyEntry prefab in the inspector.");
            return;
        }

        if (LogbookManager.Instance.enemies == null || LogbookManager.Instance.enemies.Count == 0)
        {
            Debug.LogWarning(" No enemies found in LogbookManager.enemies list.");
            return;
        }

        
        foreach (Transform child in gridParent)
            Destroy(child.gameObject);

        
        foreach (var enemy in LogbookManager.Instance.enemies)
        {
            GameObject entryObj = Instantiate(enemyEntryPrefab, gridParent);

            if (entryObj.GetComponent<FOW.Logbook.EnemyEntry>() == null)
            {
                Debug.LogError(" EnemyEntry prefab is missing the EnemyEntry component.");
                return;
            }

            entryObj.GetComponent<FOW.Logbook.EnemyEntry>().Init(enemy, enemy.isUnlocked);
        }

        Debug.Log($" Populated Enemy Grid with {LogbookManager.Instance.enemies.Count} enemies.");
    }
}
