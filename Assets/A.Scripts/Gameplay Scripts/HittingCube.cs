using UnityEngine;

public class HittingCube : MonoBehaviour, IHittable
{
    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Hitbox>(out Hitbox component))
        {
            component.PassAttack(this);
        }
    }
    
    public void GetHit()
    {
        Debug.Log("Hit");
        Destroy(this.gameObject);
    }
}
