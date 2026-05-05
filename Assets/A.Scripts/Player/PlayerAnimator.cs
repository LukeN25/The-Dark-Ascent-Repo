using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public const string IDLE         = "Idle Arms";
    public const string IDLEINSPECT  = "Weapon Inspect";
    public const string RUN          = "Run";
    public const string ATTACK1      = "Light Attack 1";
    public const string ATTACK2      = "Light Attack 2";
    public const string HEAVYATTACK  = "Heavy Attack";
    public const string DODGELEFT    = "Dodge Left";
    public const string DODGERIGHT   = "Dodge Right";
    public const string THROW        = "Throw";

    [SerializeField] private Animator armsAnimator;
    private string currentAnimationState;
    private PlayerMovement playerMovement;

    private float idleTimer = 0f;
    private bool idleInspect = false;
    private bool isActionLocked = false;
    private bool isInspecting = false;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (!playerMovement.IsMoving && !isActionLocked)
            idleTimer += Time.deltaTime;
        else
            idleTimer = 0f;

        if (idleTimer >= 10f)
        {
            idleTimer = 0f;
            idleInspect = true;
        }

        SetAnimations();
    }

    private void SetAnimations()
    {
        bool moving = playerMovement.IsMoving;

        if (isInspecting && moving)
        {
            isInspecting = false;
            isActionLocked = false;
            currentAnimationState = string.Empty;
            CancelInvoke(nameof(UnlockActionAnimation));
        }

        if (isActionLocked) return;

        armsAnimator.SetBool("isRunning", moving);

        if (!moving)
        {
            if (idleInspect)
            {
                idleInspect = false;
                ChangeAnimationState(IDLEINSPECT);
                LockActionAnimation(8f);
                isInspecting = true;
            }
            else
            {
                ChangeAnimationState(IDLE);
            }
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
        armsAnimator.CrossFadeInFixedTime(currentAnimationState, 0.2f);
    }

    public void TriggerLightAttack(float lockDuration)
    {
        LockActionAnimation(lockDuration);
        string attackState = Random.Range(0, 2) == 0 ? ATTACK1 : ATTACK2;
        ChangeAnimationState(attackState);
    }

    public void LockActionAnimation(float duration)
    {
        isActionLocked = true;
        isInspecting = false;
        currentAnimationState = string.Empty;
        CancelInvoke(nameof(UnlockActionAnimation));
        Invoke(nameof(UnlockActionAnimation), duration);
    }

    private void UnlockActionAnimation()
    {
        isActionLocked = false;
        isInspecting = false;
        currentAnimationState = string.Empty;
    }
}
