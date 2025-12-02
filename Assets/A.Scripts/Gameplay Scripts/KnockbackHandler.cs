using UnityEngine;

public class KnockbackHandler : MonoBehaviour
{
    Transform transform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    bool beingKnockedBack = false;

    void Start()
    {
        transform = GetComponent<Transform>();
    }

    Vector3 knockbackTargetPosition;
    public void ApplyKnockBack(Vector3 knockbackTarget)
    {
        beingKnockedBack = true;
        knockbackTargetPosition = knockbackTarget;
    }

    void Update()
    {
        if (beingKnockedBack)
        {
            float step = 10f * Time.deltaTime; // adjust speed as necessary
            Vector3 newPos = Vector3.MoveTowards(transform.position, knockbackTargetPosition, step);

            if (Vector3.Distance(transform.position, knockbackTargetPosition) < 0.001f)
            {
                beingKnockedBack = false;
            }

            Ray ray = new Ray(transform.position, (knockbackTargetPosition - transform.position).normalized);
            if (Physics.Raycast(ray, out RaycastHit hit, Vector3.Distance(transform.position, newPos)))
            {
                beingKnockedBack = false;
                return;
            }

            transform.position = newPos;
        }
    }
}
