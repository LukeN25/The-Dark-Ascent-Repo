using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float gravity = -20f;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip walkSound;

    private CharacterController controller;
    private PlayerDodge playerDodge;
    private Rigidbody rb;

    

    private float verticalVelocity;

    // Used by PlayerAnimator to determine idle vs run state
    public Vector3 Velocity => controller.velocity;

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
