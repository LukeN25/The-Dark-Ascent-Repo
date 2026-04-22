using UnityEngine;

public class PlayerHurtbox : MonoBehaviour, IHittable
{
    [SerializeField] PlayerManager playerManager;

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IAttacker>(out IAttacker attacker))
        {
            if (other.tag != "Player")
            {
                if (PlayerManager.Instance.IsParrying && other.TryGetComponent<IParriable>(out IParriable parriable))
                    parriable.Parried();
                else
                    GetHit(attacker.ReturnDamageInfo());
            }
        }
    }

    public void GetHit(DamageInfo damageInfo)
    {
        Debug.Log($"Player hit with {damageInfo.GetDamage()} damage!");
        playerManager.TakeDamage(damageInfo);
    }
}
