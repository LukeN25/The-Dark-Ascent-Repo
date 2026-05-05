using UnityEngine;

public class PlayerHurtbox : MonoBehaviour, IHittable
{
    [SerializeField] PlayerManager playerManager;

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IAttacker>(out IAttacker component))
        {
            if (other.tag != "Player")
            {
                DamageInfo damageInfo = component.ReturnDamageInfo();
                Vector3 raw = transform.position - other.transform.position;
                raw.y = 0f;
                damageInfo.SetKnockbackDirection(raw.normalized);
                GetHit(damageInfo);
            }
        }
    }

    public void GetHit(DamageInfo damageInfo)
    {
        playerManager.TakeDamage(damageInfo);
    }
}
