using UnityEngine;

public class HurtBox : MonoBehaviour, IHittable
{
    [SerializeField] EnemyManager enemyManager;

    public void GetHit(DamageInfo damageInfo)
    {
        enemyManager.TakeDamage(damageInfo);
    }
}
