using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator playerAnimator;
    [Tooltip("How long (seconds) the button must be held before we consider it a charged attack.")]
    [SerializeField] float chargeThreshold = 0.5f;

    bool isHolding = false;
    bool isCharging = false;
    float holdTimer = 0f;

    void Awake()
    {
        playerAnimator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // Start press
        if (Input.GetMouseButtonDown(0))
        {
            isHolding = true;
            isCharging = false;
            holdTimer = 0f;
        }

        // While held, accumulate time and enter charging state when threshold reached
        if (isHolding && Input.GetMouseButton(0))
        {
            holdTimer += Time.deltaTime;
            if (!isCharging && holdTimer >= chargeThreshold)
            {
                isCharging = true;
                playerAnimator.SetBool("IsCharging", true);
            }
        }

        // On release, decide quick vs charged attack, then reset state
        if (isHolding && Input.GetMouseButtonUp(0))
        {
            if (isCharging)
            {
                // Charged attack
                playerAnimator.SetBool("IsCharging", false);
                playerAnimator.SetTrigger("Attack");
            }
            else
            {
                // Quick attack
                playerAnimator.SetTrigger("Attack");
            }

            isHolding = false;
            isCharging = false;
            holdTimer = 0f;
        }
    }
}
