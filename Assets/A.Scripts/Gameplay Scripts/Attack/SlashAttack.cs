
using UnityEngine;

namespace FOW.Demos
{
    public class SlashAttack : MonoBehaviour
    {
        [Header("Slash Settings")]
        public float damage = 10f;
        public float lifetime = 0.4f;
        public float swingAngle = 120f; 
        public float swingSpeed = 400f; 
        public Vector3 swingAxis = Vector3.up; 

        private float elapsed;

        void Start()
        {
            Destroy(gameObject, lifetime);
        }

        void Update()
        {
            AnimateSlash();
        }

        private void AnimateSlash()
        {
            elapsed += Time.deltaTime;
            float rotateAmount = swingSpeed * Time.deltaTime;
            transform.Rotate(swingAxis * rotateAmount, Space.Self);

            
            float scalePulse = 1 + Mathf.Sin(elapsed * 20f) * 0.05f;
            transform.localScale = Vector3.one * scalePulse;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                Debug.Log($"Hit enemy {other.name}, dealing {damage} damage!");

                // Placeholder damage call:
                // other.GetComponent<EnemyHealth>()?.TakeDamage(damage);
            }
        }
    }
}
