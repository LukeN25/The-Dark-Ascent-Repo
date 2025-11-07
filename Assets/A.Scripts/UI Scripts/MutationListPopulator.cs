using UnityEngine;
using FOW.Logbook;
using FOW.Mutations;

public class MutationListPopulator : MonoBehaviour
{
    public Transform gridParent;
    public GameObject mutationEntryPrefab;

    private EnemyInfo currentEnemy;

    private void OnEnable()
    {
        if (currentEnemy != null)
            Populate(currentEnemy);
    }

    public void SetEnemy(EnemyInfo enemy)
    {
        currentEnemy = enemy;
        Populate(enemy);
    }

    public void Populate(EnemyInfo enemy)
    {
        if (enemy == null) return;

        foreach (Transform child in gridParent)
            Destroy(child.gameObject);

        foreach (var mutation in enemy.possibleMutations)
        {
            bool unlocked = MutationInventoryManager.Instance.collectedMutations.Contains(mutation);

            GameObject entry = Instantiate(mutationEntryPrefab, gridParent);
            entry.GetComponent<MutationEntry>().Initialize(mutation, unlocked);
        }
    }
}
