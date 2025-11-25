using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float WalkingSpeed = 5;
    float Acceleration = 25;

    [SerializeField]
    float dashDistance = 5f;
    [SerializeField]
    float dashCooldown = 3f;
    float dashCooldownTimer = 0f;
    bool canDash = true;
    bool isDashing = false;

    private CharacterController cc;
    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Start()
    {
        WalkingSpeed = PlayerManager.Instance.GetMoveSpeed();
        Acceleration = PlayerManager.Instance.GetAcceleration();
    }

    void Update()
    {
        DashTimer();
        if (!isDashing)
        {
            SetInput();
            Move();
        }
        else
        {
            Dash();
        }
    }

    Vector2 inputDirection = Vector2.zero;
    Vector2 velocityXZ = Vector2.zero;
    Vector3 velocity = Vector3.zero;
    float speedTarget;


    public void SetInput()
    {
        bool[] inputs = new bool[]
            {
                Input.GetKey(KeyCode.W),
                Input.GetKey(KeyCode.A),
                Input.GetKey(KeyCode.S),
                Input.GetKey(KeyCode.D),
                Input.GetKey(KeyCode.Space)
            };

        speedTarget = 0;
        inputDirection = Vector2.zero;
        if (inputs[0])
        {
            inputDirection.y += 1;
            speedTarget = WalkingSpeed;
        }
        if (inputs[1])
        {
            inputDirection.x -= 1;
            speedTarget = WalkingSpeed;
        }
        if (inputs[2])
        {
            inputDirection.y -= 1;
            speedTarget = WalkingSpeed;
        }
        if (inputs[3])
        {
            inputDirection.x += 1;
            speedTarget = WalkingSpeed;
        }
        if (inputs[4] && canDash && speedTarget > 0 && !inputs[0])
        {
            isDashing = true;
            canDash = false;
            speedTarget = WalkingSpeed;
        }
    }
    void Move()
    {
        if (cc.isGrounded)
        {
            velocity.y = 0;
        }
        Vector2 forward = new Vector2(transform.forward.x, transform.forward.z);
        Vector2 right = new Vector2(transform.right.x, transform.right.z);
        Vector2 inputDir = Vector3.Normalize(right * inputDirection.x + forward * inputDirection.y);

        velocityXZ = Vector2.MoveTowards(velocityXZ, inputDir.normalized * speedTarget, Time.deltaTime * Acceleration);
        velocity.x = velocityXZ.x * Time.deltaTime;
        velocity.z = velocityXZ.y * Time.deltaTime;
        velocity.y += -9.81f * Time.deltaTime * Time.deltaTime;

        cc.enabled = true;
        cc.Move(velocity);
        cc.enabled = false;

        // If dashing, set dash destination
        if (isDashing)
        {
            dashDestination = transform.position + new Vector3(velocityXZ.x, 0, velocityXZ.y).normalized * dashDistance;

            Debug.DrawRay(transform.position, (dashDestination - transform.position), Color.red, 5f);
        }
    }

    Vector3 dashDestination = Vector3.zero;
    void Dash()
    {
        // Get new position towards dash destination
        Vector3 newPos = Vector3.MoveTowards(transform.position, dashDestination, WalkingSpeed * dashDistance * Time.deltaTime);

        // Check for collisions
        Ray ray = new Ray(transform.position, (dashDestination - transform.position).normalized);
        if (Physics.Raycast(ray, out RaycastHit hit, Vector3.Distance(transform.position, newPos)))
        {
            // If we hit something, stop dashing
            isDashing = false;
            return;
        }

        transform.position = newPos;

        if (Vector3.Distance(transform.position, dashDestination) < 0.1f)
        {
            isDashing = false;
        }
    }

    private void DashTimer()
    {
        if (!canDash)
        {
            dashCooldownTimer += Time.fixedDeltaTime;
            if (dashCooldownTimer >= dashCooldown)
            {
                canDash = true;
                dashCooldownTimer = 0f;
            }
        }
    }
}
