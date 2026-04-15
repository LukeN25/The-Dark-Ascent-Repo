using UnityEngine;

namespace FOW.Demos
{
    public class SlashAttack : MonoBehaviour
    {
        public float damage = 10f;
        public float lifetime = 0.4f;
        public float swingSpeed = 400f;
        public Vector3 swingAxis = Vector3.up;

        private void Start() => Destroy(gameObject, lifetime);

        private void Update()
        {
            transform.Rotate(swingAxis * swingSpeed * Time.deltaTime, Space.Self);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                Debug.Log($"Hit enemy {other.name} for {damage} dmg");
                // other.GetComponent<EnemyHealth>()?.TakeDamage(damage);
            }
        }
    }
}
