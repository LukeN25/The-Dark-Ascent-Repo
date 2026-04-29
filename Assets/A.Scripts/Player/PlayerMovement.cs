using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float gravity = -20f;

    private CharacterController controller;
    private PlayerDodge playerDodge;

    private float verticalVelocity;

    // Used by PlayerAnimator to determine idle vs run state
    public Vector3 Velocity => controller.velocity;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerDodge = GetComponent<PlayerDodge>();
    }

    void Update()
    {
        HandleMovement();
        HandleGravity();
    }

    private void HandleMovement()
    {
        if (playerDodge.IsDodging) return;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = transform.right * h + transform.forward * v;
        controller.Move(move * speed * Time.deltaTime);
    }

    private void HandleGravity()
    {
        if (controller.isGrounded && verticalVelocity < 0f)
            verticalVelocity = -2f;
        else if (!controller.isGrounded)
            verticalVelocity += gravity * Time.deltaTime;

        controller.Move(Vector3.up * verticalVelocity * Time.deltaTime);
    }
}
