using UnityEngine;
using EnemyAI.UnityHFSM;

public class EnemyHitbox : MonoBehaviour, IAttacker, IParriable
{
    [SerializeField]
    EnemyManager enemyManager;

    public DamageInfo ReturnDamageInfo()
    {
        return enemyManager.GetDamageInfo();
    }

    public void Parried()
    {
        enemyManager.Parried();
        GetComponentInParent<Enemy>()?.TriggerParried();
    }
}