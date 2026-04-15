using UnityEngine;

public class HittingCube : MonoBehaviour, IHittable
{
    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IAttacker>(out IAttacker component))
        {
            GetHit(component.ReturnDamageInfo());
        }
    }
    
    public void GetHit(DamageInfo damageInfo)
    {
        print("Cube got hit for " + damageInfo.GetDamage() + " damage.");
        Destroy(gameObject);
    }
}
