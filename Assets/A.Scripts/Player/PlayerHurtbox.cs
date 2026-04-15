using UnityEngine;

public class PlayerHurtbox : MonoBehaviour, IHittable
{
    [SerializeField] FirstPersonController firstPersonController;

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IAttacker>(out IAttacker component))
        {
            if(other.tag != "Player")
            {
                GetHit(component.ReturnDamageInfo());
            }
        }
    }

    public void GetHit(DamageInfo damageInfo)
    {
        firstPersonController.TakeDamage(damageInfo);
    }
}
