using Unity.VisualScripting;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour, IAttacker
{
    public DamageInfo ReturnDamageInfo()
    {
        DamageInfo damageInfo = new DamageInfo(FirstPersonController.Instance.GetAttackDamage());

        return damageInfo;
    }
}
