using System.Collections;
using UnityEngine;
using FOW.Mutations; 

namespace FOW.Demos
{
    public class PlayerAttack : MonoBehaviour
    {
        public GameObject slashPrefab;
        public float slashSpawnDistance = 1.2f;
        public float baseDamage = 10f;
        public float attackCooldown = 0.5f;

        private bool canAttack = true;
        private PlayerMutationHandler mutationHandler;

        private void Awake()
        {
            mutationHandler = GetComponent<PlayerMutationHandler>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && canAttack)
                StartCoroutine(PerformSlash());
        }

        private IEnumerator PerformSlash()
        {
            canAttack = false;

            float dmgMult = mutationHandler ? mutationHandler.damageMultiplier : 1f;
            float rangeMult = mutationHandler ? mutationHandler.rangeMultiplier : 1f;

            float damage = baseDamage * dmgMult;

            Vector3 spawnPos = transform.position + transform.forward * slashSpawnDistance + Vector3.up * 0.5f;
            Quaternion spawnRot = Quaternion.LookRotation(transform.forward);

            var slash = Instantiate(slashPrefab, spawnPos, spawnRot);
            slash.transform.localScale *= rangeMult;

            var s = slash.GetComponent<SlashAttack>();
            if (s) s.damage = damage;

            Destroy(slash, 0.4f);
            yield return new WaitForSeconds(attackCooldown);
            canAttack = true;
        }
    }
}
