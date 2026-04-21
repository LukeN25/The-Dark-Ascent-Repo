using UnityEngine;

public class PlayerHitbox : MonoBehaviour, IAttacker
{
    public DamageInfo ReturnDamageInfo()
    {
        return new DamageInfo(PlayerManager.Instance.GetAttackDamage());
    }
}
