using Unity.VisualScripting;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour, IAttacker
{
    public DamageInfo ReturnDamageInfo()
    {
        PlayerManager playerManager = PlayerManager.Instance;

        int attackDamage = playerManager.GetAttackDamage();
        float knockbackForce = playerManager.GetKnockbackForce();

        DamageInfo damageInfo = new DamageInfo(attackDamage, knockbackForce);

        return damageInfo;
    }

    public Vector3 GetAttackerPosition()
    {
        return PlayerManager.Instance.gameObject.transform.position;
    }
}
