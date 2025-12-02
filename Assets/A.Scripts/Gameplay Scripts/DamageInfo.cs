using UnityEngine;

public struct DamageInfo
{
    int damage;
    Vector3 knockbackDirection;
    float knockbackForce;

    public DamageInfo(int damage, float force)
    {
        this.damage = damage;
        this.knockbackDirection = Vector3.zero;
        this.knockbackForce = force;
    }

    public int GetDamage()
    {
        return damage;
    }
    public Vector3 GetKnockbackDirection()
    {
        return knockbackDirection;
    }
    public void AddKnockbackDirection(Vector3 direction)
    {
        knockbackDirection += direction;
    }
    public float GetKnockbackForce()
    {
        return knockbackForce;
    }
}
