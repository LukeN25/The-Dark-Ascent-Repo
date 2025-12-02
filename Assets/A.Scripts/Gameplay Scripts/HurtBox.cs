using UnityEngine;

public class HurtBox : MonoBehaviour, IHittable
{
    [SerializeField] EnemyManager enemyManager;
    [SerializeField] DamageIndicator DamageIndicator;

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IAttacker>(out IAttacker component))
        {
            DamageInfo damageInfo = component.ReturnDamageInfo();

            // Get the direction the cube was hit from
            Vector3 hitDirection = enemyManager.transform.position - component.GetAttackerPosition();

            Debug.Log("Hit Direction: " + hitDirection.normalized);
            damageInfo.AddKnockbackDirection(hitDirection.normalized);

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
