using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float gravity = -20f;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip walkSound;

    [Header("Knockback")]
    [SerializeField] private float knockbackDecay = 10f;
    private LayerMask knockbackCollisionMask;

    private CharacterController controller;
    private PlayerDodge playerDodge;
    private Rigidbody rb;

    private float verticalVelocity;
    private Vector3 knockbackVelocity;

    public Vector3 Velocity => controller.velocity;
    public bool IsMoving => Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerDodge = GetComponent<PlayerDodge>();
        rb = GetComponent<Rigidbody>();
        knockbackCollisionMask = LayerMask.GetMask("Default", "Wall");
    }

    void Update()
    {
        HandleMovement();
        HandleGravity();

        /*if(controller.velocity.magnitude >= 1)
        {
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.PlayOneShot(walkSound);
        }*/
    }

    private void HandleMovement()
    {
        if (playerDodge.IsDodging) return;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = transform.right * h + transform.forward * v;
        controller.Move(move * speed * Time.deltaTime);

        if (knockbackVelocity.sqrMagnitude > 0f)
        {
            Vector3 delta = knockbackVelocity * Time.deltaTime;

            float worldRadius     = controller.radius * Mathf.Max(transform.lossyScale.x, transform.lossyScale.z);
            float worldHalfHeight = Mathf.Max(0f, controller.height * 0.5f * transform.lossyScale.y - worldRadius);
            Vector3 worldCenter   = transform.TransformPoint(controller.center);
            Vector3 capsuleTop    = worldCenter + Vector3.up * worldHalfHeight;
            Vector3 capsuleBottom = worldCenter - Vector3.up * worldHalfHeight;

            if (delta.sqrMagnitude > 0f &&
                Physics.CapsuleCast(capsuleBottom, capsuleTop, worldRadius,
                    delta.normalized, out RaycastHit hit, delta.magnitude,
                    knockbackCollisionMask, QueryTriggerInteraction.Ignore))
            {
                knockbackVelocity = Vector3.zero;
                delta = Vector3.zero;
            }

            controller.Move(delta);
            knockbackVelocity = Vector3.Lerp(knockbackVelocity, Vector3.zero, knockbackDecay * Time.deltaTime);
        }
    }

    private void HandleGravity()
    {
        if (controller.isGrounded && verticalVelocity < 0f)
            verticalVelocity = -2f;
        else if (!controller.isGrounded)
            verticalVelocity += gravity * Time.deltaTime;

        controller.Move(Vector3.up * verticalVelocity * Time.deltaTime);
    }

    public void ApplyKnockback(Vector3 direction, float strength)
    {
        knockbackVelocity = direction * strength;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.normal.y > 0.7f) return;
        if (knockbackVelocity.sqrMagnitude < 0.01f) return;
        knockbackVelocity = Vector3.zero;
    }
}
