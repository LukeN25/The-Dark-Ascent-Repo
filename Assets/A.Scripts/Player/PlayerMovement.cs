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

        controller.Move(knockbackVelocity * Time.deltaTime);
        knockbackVelocity = Vector3.Lerp(knockbackVelocity, Vector3.zero, knockbackDecay * Time.deltaTime);
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
}
