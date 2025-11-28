using System.Collections;
using UnityEngine;

namespace EnemyAI.UnityHFSM
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]

    public class Spore : MonoBehaviour, IAttacker
    {
        [SerializeField] EnemyManager enemyManager;

        private void OnEnable()
        {
            StopAllCoroutines();
            StartCoroutine(DelayDisable());
        }

        private IEnumerator DelayDisable()
        {
            if (Wait == null)
            {
                Wait = new WaitForSeconds(AutoDestroyTime);
            }

            yield return null;

            Rigidbody.AddForce(transform.forward * Force);

            yield return Wait;
            Destroy(gameObject);
        }

        private void OnDisable()
        {
            Rigidbody.angularVelocity = Vector3.zero;
            Rigidbody.linearVelocity = Vector3.zero;
        }

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                Destroy(gameObject);
            }
        }

        public DamageInfo ReturnDamageInfo()
        {
            DamageInfo damageInfo = enemyManager.GetDamageInfo();

            return damageInfo;
        }

        [SerializeField]
        private float AutoDestroyTime = 1f;

        [SerializeField] private float Force = 100;

        private WaitForSeconds Wait;
        private Rigidbody Rigidbody;
    }
}