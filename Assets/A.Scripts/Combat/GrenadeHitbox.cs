using UnityEngine;

public class GrenadeHitbox : MonoBehaviour, IAttacker
{
    public DamageInfo ReturnDamageInfo()
    {
        return new DamageInfo(PlayerManager.Instance.GetGrenadeDamage(), 0f);
    }
}