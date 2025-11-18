using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] int damage = 1;

    [SerializeField] int health = 3;

    public DamageInfo GetDamageInfo()
    {
        return new DamageInfo(damage);
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
        Destroy(gameObject);
    }
}
