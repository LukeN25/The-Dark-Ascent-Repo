using UnityEngine;

public class EnemyHitbox : MonoBehaviour, IAttacker
{
    [SerializeField]
    EnemyManager enemyManager;
    public DamageInfo ReturnDamageInfo()
    {
        DamageInfo damageInfo = enemyManager.GetDamageInfo();

        return damageInfo;
    }

    public Vector3 GetAttackerPosition()
    {
        return enemyManager.gameObject.transform.position;
    }
}