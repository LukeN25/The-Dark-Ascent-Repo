using UnityEngine;

public class HurtBox : MonoBehaviour, IHittable
{
    [SerializeField] EnemyManager enemyManager;
    [SerializeField] DamageIndicator DamageIndicator;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyHitBox")) return;

        if (other.TryGetComponent<IAttacker>(out IAttacker component))
        {
            DamageInfo damageInfo = component.ReturnDamageInfo();
            Vector3 raw = transform.position - other.transform.position;
            raw.y = 0f;
            damageInfo.SetKnockbackDirection(raw.normalized);
            GetHit(damageInfo);
        }
    }

    public void GetHit(DamageInfo damageInfo)
    {
        enemyManager.TakeDamage(damageInfo);
        if (DamageIndicator != null)
            DamageIndicator.Flash();
    }
}
