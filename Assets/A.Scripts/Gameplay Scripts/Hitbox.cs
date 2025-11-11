using Unity.VisualScripting;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    IAttacker attacker;

    void Awake()
    {
        attacker = GetComponentInParent<IAttacker>();
    }

    public void PassAttack(IHittable target)
    {
        attacker.Attack(target);
    }
}
