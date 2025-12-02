using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] int damage = 1;

    [SerializeField] int health = 3;

    [SerializeField] private WeightedRandomList<Transform> lootTable;
    [SerializeField] private Transform itemHolder; 

    [SerializeField] KnockbackHandler knockbackHandler;

    public DamageInfo GetDamageInfo()
    {
        return new DamageInfo(damage, 0f);
    }

    public void TakeDamage(DamageInfo damageInfo)
    {
        health -= damageInfo.GetDamage();
        if (health <= 0)
        {
            Die();
        }
        else
        {
            ProcessKnockback(damageInfo);
        }
    }

    private void ProcessKnockback(DamageInfo damageInfo)
    {
        // move object in direction of knockback
        Vector3 knockbackDirection = damageInfo.GetKnockbackDirection();
        knockbackDirection.y = 0; // ignore vertical component
        float knockbackForce = damageInfo.GetKnockbackForce();

        Vector3 knockBackEndPosition = transform.position + knockbackDirection.normalized * knockbackForce;
        knockbackHandler.ApplyKnockBack(knockBackEndPosition);
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
