using System.Collections;
using UnityEngine;

public class PlayerDodge : MonoBehaviour
{
    [Header("Dodging")]
    public float dodgeSpeed;
    public float dodgeCooldown;
    public float dodgeLength;

    public bool IsDodging { get; private set; }
    bool canDodge = true;
    float cooldownTimer;

    private CharacterController controller;
    private PlayerAnimator playerAnimator;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerAnimator = GetComponent<PlayerAnimator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDodge)
        {
            StartCoroutine(Dodge());
            cooldownTimer = 0f;
        }

        cooldownTimer += Time.deltaTime;

        if(cooldownTimer >= dodgeCooldown) 
        {
            canDodge = true;
        }
    }

    private IEnumerator Dodge()
    {
        float timer = 0;
        playerAnimator.LockActionAnimation(dodgeLength);

        while (timer < dodgeLength)
        {
            canDodge = false;
            IsDodging = true;

            Vector3 moveDir = transform.forward * Input.GetAxisRaw("Vertical") +
                              transform.right * Input.GetAxisRaw("Horizontal");

            if (Input.GetAxisRaw("Horizontal") > 0)
                playerAnimator.ChangeAnimationState(PlayerAnimator.DODGELEFT);
            else if (Input.GetAxisRaw("Horizontal") < 0)
                playerAnimator.ChangeAnimationState(PlayerAnimator.DODGERIGHT);

            if (moveDir == Vector3.zero)
                moveDir = transform.forward;

            moveDir.Normalize();
            controller.Move(moveDir * dodgeSpeed * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }

        IsDodging = false;
    }
}
