using UnityEngine;
using FOW.Logbook;
using FOW.Mutations;

public class MutationListPopulator : MonoBehaviour
{
    public Transform gridParent;
    public GameObject mutationEntryPrefab;

    public void SetEnemy(EnemyInfo enemy)
    {
        Populate(enemy);
    }

    private void Populate(EnemyInfo enemy)
    {
        foreach (Transform child in gridParent)
            Destroy(child.gameObject);

        foreach (MutationInfo m in enemy.possibleMutations)
        {
            var entry = Instantiate(mutationEntryPrefab, gridParent);
            bool unlocked = MutationInventoryManager.Instance.collectedMutations.Contains(m);
            entry.GetComponent<MutationEntry>().Initialize(m, unlocked);
        }
    }
}
