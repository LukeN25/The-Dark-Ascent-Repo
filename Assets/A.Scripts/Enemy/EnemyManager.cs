using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] int damage = 1;
    [SerializeField] float knockbackStrength = 5f;

    [SerializeField] int health = 3;

    [SerializeField] private WeightedRandomList<Transform> lootTable;
    [SerializeField] private Transform itemHolder;

    [SerializeField] public bool knockbackEnabled = true;

    [Header("Knockback Collision")]
    [SerializeField] private LayerMask knockbackCollisionMask = 1;
    [SerializeField] private float navMeshSnapRadius = 2f;

    private NavMeshAgent agent;
    private CapsuleCollider bodyCollider;
    private Coroutine knockbackCoroutine;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        foreach (var col in GetComponents<CapsuleCollider>())
        {
            if (!col.isTrigger) { bodyCollider = col; break; }
        }
    }

    public DamageInfo GetDamageInfo()
    {
        return new DamageInfo(damage, knockbackStrength);
    }

    public void TakeDamage(DamageInfo damageInfo)
    {
        health -= damageInfo.GetDamage();
        if (health <= 0)
        {
            Die();
            return;
        }

        if (knockbackEnabled)
            ApplyKnockback(damageInfo.GetKnockbackDirection(), damageInfo.GetKnockbackStrength());
    }

    public void ApplyKnockback(Vector3 direction, float strength)
    {
        if (knockbackCoroutine != null) StopCoroutine(knockbackCoroutine);
        knockbackCoroutine = StartCoroutine(KnockbackRoutine(direction, strength));
    }

    private IEnumerator KnockbackRoutine(Vector3 direction, float strength)
    {
        agent.enabled = false;
        Vector3 knockbackVelocity = direction * strength;
        float decay = 10f;

        while (knockbackVelocity.magnitude > 0.1f)
        {
            Vector3 delta = knockbackVelocity * Time.deltaTime;

            if (bodyCollider != null)
            {
                float worldRadius = bodyCollider.radius *
                    Mathf.Max(transform.lossyScale.x, transform.lossyScale.z);
                float worldHalfHeight = Mathf.Max(0f,
                    bodyCollider.height * 0.5f * transform.lossyScale.y - worldRadius);
                Vector3 worldCenter   = transform.TransformPoint(bodyCollider.center);
                Vector3 capsuleTop    = worldCenter + Vector3.up * worldHalfHeight;
                Vector3 capsuleBottom = worldCenter - Vector3.up * worldHalfHeight;

                if (Physics.CapsuleCast(capsuleBottom, capsuleTop, worldRadius,
                        delta.normalized, out RaycastHit hit, delta.magnitude,
                        knockbackCollisionMask, QueryTriggerInteraction.Ignore))
                {
                    knockbackVelocity -= Vector3.Dot(knockbackVelocity, hit.normal) * hit.normal;
                    delta = knockbackVelocity * Time.deltaTime;
                }
            }
            else
            {
                // Fallback for enemies without a solid CapsuleCollider (e.g. Crawler)
                Vector3 sphereOrigin = transform.position + Vector3.up * agent.radius;
                if (Physics.SphereCast(sphereOrigin, agent.radius, delta.normalized,
                        out RaycastHit hit, delta.magnitude,
                        knockbackCollisionMask, QueryTriggerInteraction.Ignore))
                {
                    knockbackVelocity -= Vector3.Dot(knockbackVelocity, hit.normal) * hit.normal;
                    delta = knockbackVelocity * Time.deltaTime;
                }
            }

            transform.position += delta;
            knockbackVelocity = Vector3.Lerp(knockbackVelocity, Vector3.zero, decay * Time.deltaTime);
            yield return null;
        }

        if (NavMesh.SamplePosition(transform.position, out NavMeshHit navHit, navMeshSnapRadius, NavMesh.AllAreas))
            agent.Warp(navHit.position);

        agent.enabled = true;
    }

    private void Die()
    {
        if (lootTable != null && itemHolder != null)
        {
            DropLoot();
        }

        Destroy(gameObject);
    }

    private void DropLoot()
    {
        Transform item = lootTable.GetRandom();
        Instantiate(item, itemHolder.position, itemHolder.rotation);
    }
}
