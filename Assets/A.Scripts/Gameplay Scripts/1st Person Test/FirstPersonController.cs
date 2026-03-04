using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FirstPersonController : MonoBehaviour
{
    public static FirstPersonController Instance { get; private set; }

    private CharacterController controller;
    private Transform cam;
    AudioSource audioSource;
    Animator animator;

    [Header("Movement")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float gravity = -9.81f;

    bool isGrounded;

    [Header("Attacking")]
    public float attackDistance;
    public float attackDelay;
    public float attackSpeed;
    public int attackDamage;
    public LayerMask attackLayer;

    public GameObject hitEffect;
    public GameObject swingEffect;
    public AudioClip hitSound;
    public AudioClip swingSound;

    bool attacking = false;
    bool canAttack = true;
    int attackCount;

    [Header("Dodging")]
    public float dodgeSpeed;
    public float dodgeCooldown;
    public float dodgeLength;

    bool isDodging = false;

    //ANIMATIONS
    public const string IDLE = "Idle";
    public const string RUN = "Run";
    public const string ATTACK1 = "Attack 1";
    public const string ATTACK2 = "Attack 2";
    public const string HEAVYATTACK = "Heavy Attack";

    string currentAnimationState;

    Vector3 velocity;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        controller = GetComponent<CharacterController>();
        cam = transform.Find("Camera Holder");
        audioSource = GetComponent<AudioSource>();
        animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        if (!isDodging)
        {
            controller.Move(Time.deltaTime * speed * (transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal")));
        }
        
        velocity.y += gravity * Time.deltaTime;
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity = controller.velocity;

        // Attack Input
        if (Input.GetMouseButton(0))
        {
            Debug.Log("Left Click");
            Attack();
        }
        
        if (Input.GetMouseButton(1))
        {
            Debug.Log("Right Click");
            HeavyAttack();
        }

        // Dodge Input
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(Dodge());
        }

        SetAnimations();
    }

    //--------------
    //Attack Methods
    //--------------

    public void Attack()
    {
        if (!canAttack || attacking) return;

        canAttack = false;
        attacking = true;

        Invoke(nameof (ResetAttack), attackSpeed);
        Invoke(nameof (AttackRaycast), attackDelay);

        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(swingSound);

        if(attackCount == 0)
        {
            ChangeAnimationState(ATTACK1);
            attackCount++;
        }
        else
        {
            ChangeAnimationState(ATTACK2);
            attackCount = 0;
        }
    }

    public void HeavyAttack()
    {
        if(!canAttack || attacking) return;

        canAttack = false;
        attacking = true;

        Invoke(nameof (ResetAttack), attackSpeed);
        Invoke(nameof (AttackRaycast), attackDelay);

        audioSource.pitch = Random.Range(1.1f, 1.3f);
        audioSource.PlayOneShot(swingSound);
    }

    public void ResetAttack()
    {
        attacking = false;
        canAttack = true;
    }

    void AttackRaycast()
    {
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, attackDistance, attackLayer))
        {
            HitTarget(hit.point);
        }
    } 

    void HitTarget(Vector3 pos)
    {
        audioSource.pitch = 1;
        audioSource.PlayOneShot(hitSound);

        GameObject HIT = Instantiate(hitEffect, pos, Quaternion.identity);
        Destroy(HIT, 20);
    }

    //--------------
    //DODGE METHODS
    //--------------

    IEnumerator Dodge()
    {
        float timer = 0;

        while(timer < dodgeLength)
        {
            isDodging = true;

            // Get movement input direction
            Vector3 moveDir = transform.forward * Input.GetAxisRaw("Vertical") +
                              transform.right * Input.GetAxisRaw("Horizontal");

            // If no input, dodge forward
            if (moveDir == Vector3.zero)
                moveDir = transform.forward;

            moveDir.Normalize();

            controller.Move(moveDir * dodgeSpeed * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }
        isDodging = false;
    }

    void SetAnimations()
    {
        //NOT ATTACKING
        if (!attacking)
        {
            if(velocity.x == 0 && velocity.z == 0)
            {
                ChangeAnimationState(IDLE);
            }
            else
            {
                ChangeAnimationState(RUN);
            }
        }
    }

    void ChangeAnimationState(string newState)
    {
        //Stop Self Interruption
        if (currentAnimationState == newState) return;

        //Play Animation
        currentAnimationState = newState;
        animator.CrossFadeInFixedTime(currentAnimationState, 0.2f);
    }

    public int GetAttackDamage() => attackDamage;

}
