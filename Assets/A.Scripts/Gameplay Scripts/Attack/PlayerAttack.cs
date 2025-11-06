using System.Collections;
using UnityEngine;

namespace FOW.Demos
{
    public class PlayerAttack : MonoBehaviour
    {
        [Header("Attack Settings")]
        public GameObject slashPrefab;         
        public float slashSpawnDistance = 1.2f;
        public float attackCooldown = 0.5f;

        private bool canAttack = true;

        void Update()
        {
            if (Input.GetMouseButtonDown(0) && canAttack)
            {
                StartCoroutine(PerformSlash());
            }
        }

        IEnumerator PerformSlash()
        {
            canAttack = false;

            
            Vector3 spawnPos = transform.position + transform.forward * slashSpawnDistance + Vector3.up * 0.5f;
            Quaternion spawnRot = Quaternion.LookRotation(transform.forward);

            
            GameObject slash = Instantiate(slashPrefab, spawnPos, spawnRot);
            Destroy(slash, 0.4f); 

            yield return new WaitForSeconds(attackCooldown);
            canAttack = true;
        }
    }
}
