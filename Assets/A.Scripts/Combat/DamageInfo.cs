using UnityEngine;

public struct DamageInfo
{
    int damage;
    float knockbackStrength;
    Vector3 knockbackDirection;

    public DamageInfo(int damage, float knockbackStrength)
    {
        this.damage = damage;
        this.knockbackStrength = knockbackStrength;
        this.knockbackDirection = Vector3.zero;
    }

    public int GetDamage() => damage;
    public float GetKnockbackStrength() => knockbackStrength;
    public Vector3 GetKnockbackDirection() => knockbackDirection;
    public void SetKnockbackDirection(Vector3 direction) { knockbackDirection = direction; }
}
