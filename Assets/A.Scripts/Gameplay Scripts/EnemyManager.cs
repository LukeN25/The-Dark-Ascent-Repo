using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] int damage = 1;

    public DamageInfo GetDamageInfo()
    {
        return new DamageInfo(damage);
    }
}
