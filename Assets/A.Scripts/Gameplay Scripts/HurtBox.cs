using UnityEngine;

public class HurtBox : MonoBehaviour, IHittable
{
    [SerializeField] EnemyManager enemyManager;
    [SerializeField] DamageIndicator DamageIndicator;

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IAttacker>(out IAttacker component))
        {
            GetHit(component.ReturnDamageInfo());
        }
    }

    public void GetHit(DamageInfo damageInfo)
    {
        enemyManager.TakeDamage(damageInfo);
        if (DamageIndicator != null)
            DamageIndicator.Flash();
    }
}
