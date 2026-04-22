using UnityEngine;
using EnemyAI.UnityHFSM;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] int damage = 1;

    [SerializeField] int health = 3;

    [SerializeField] private WeightedRandomList<Transform> lootTable;
    [SerializeField] private Transform itemHolder;
    [SerializeField] private DamageIndicator damageIndicator;

    public DamageInfo GetDamageInfo()
    {
        return new DamageInfo(damage);
    }

    public void Parried()
    {
        damageIndicator?.FlashWithColour(Color.blue);
    }

    public void TakeDamage(DamageInfo damageInfo)
    {
        health -= damageInfo.GetDamage();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (lootTable != null && itemHolder != null)
        {
            DropLoot();
        }

        Destroy(gameObject);
    }

    private void DropLoot()
    {
        Transform item = lootTable.GetRandom();
        Instantiate(item, itemHolder.position, itemHolder.rotation);
    }
}
