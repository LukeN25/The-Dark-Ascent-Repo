using UnityEngine;

public interface IAttacker
{
    public DamageInfo ReturnDamageInfo();

    public Vector3 GetAttackerPosition();
}
