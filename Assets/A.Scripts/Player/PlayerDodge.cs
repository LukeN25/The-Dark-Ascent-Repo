using System.Collections;
using UnityEngine;

public class PlayerDodge : MonoBehaviour
{
    [Header("Dodging")]
    public float dodgeSpeed;
    public float dodgeCooldown;
    public float dodgeLength;

    public bool IsDodging { get; private set; }

    private CharacterController controller;
    private PlayerAnimator playerAnimator;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerAnimator = GetComponent<PlayerAnimator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            StartCoroutine(Dodge());
    }

    private IEnumerator Dodge()
    {
        float timer = 0;

        while (timer < dodgeLength)
        {
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
