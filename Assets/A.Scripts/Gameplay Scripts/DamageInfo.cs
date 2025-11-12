using UnityEngine;

public struct DamageInfo
{
    int damage;

    public DamageInfo(int damage)
    {
        this.damage = damage;
    }

    public int GetDamage()
    {
        return damage;
    }
}
