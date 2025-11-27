using Unity.VisualScripting;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour, IAttacker
{
    public DamageInfo ReturnDamageInfo()
    {
        DamageInfo damageInfo = new DamageInfo(PlayerManager.Instance.GetAttackDamage());

        return damageInfo;
        Debug.Log("holy it works???");
    }
}
