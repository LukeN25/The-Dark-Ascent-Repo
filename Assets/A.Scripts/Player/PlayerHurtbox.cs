using UnityEngine;

public class PlayerHurtbox : MonoBehaviour, IHittable
{
    [SerializeField] PlayerManager playerManager;

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IAttacker>(out IAttacker component))
        {
            if (other.tag != "Player")
                GetHit(component.ReturnDamageInfo());
        }
    }

    public void GetHit(DamageInfo damageInfo)
    {
        playerManager.TakeDamage(damageInfo);
    }
}
