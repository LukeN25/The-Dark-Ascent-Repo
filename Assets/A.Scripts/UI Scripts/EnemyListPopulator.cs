using UnityEngine;
using FOW.Logbook;

public class EnemyListPopulator : MonoBehaviour
{
    public Transform gridParent;
    public GameObject enemyEntryPrefab;

    private void OnEnable()
    {
        Populate();
    }

    private void Populate()
    {
        foreach (Transform child in gridParent)
            Destroy(child.gameObject);

        foreach (var enemy in LogbookManager.Instance.enemies)
        {
            var entry = Instantiate(enemyEntryPrefab, gridParent);
            entry.GetComponent<EnemyEntry>().Init(enemy, enemy.isUnlocked);
        }
    }
}
