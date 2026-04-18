using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public const string IDLE         = "Idle";
    public const string IDLEINSPECT  = "Idle Inspect";
    public const string RUN          = "Run";
    public const string ATTACK1      = "Attack 1";
    public const string ATTACK2      = "Attack 2";
    public const string HEAVYATTACK  = "Heavy Attack";
    public const string DODGELEFT    = "Dodge Left";
    public const string DODGERIGHT   = "Dodge Right";

    private string currentAnimationState;
    private Animator animator;
    private PlayerMovement playerMovement;

    private float idleTimer = 0f;
    private bool idleInspect = false;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        idleTimer += Time.deltaTime;
        if (idleTimer >= 10f)
        {
            idleTimer = 0f;
            idleInspect = true;
        }

        SetAnimations();
    }

    private void SetAnimations()
    {
        Vector3 vel = playerMovement.Velocity;
        if (vel.x == 0 && vel.z == 0)
        {
            ChangeAnimationState(IDLE);

            if (idleInspect)
                ChangeAnimationState(IDLEINSPECT);
        }
        else
        {
            ChangeAnimationState(RUN);
            idleInspect = false;
        }
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentAnimationState == newState) return;
        currentAnimationState = newState;
        animator.CrossFadeInFixedTime(currentAnimationState, 0.2f);
    }
}
