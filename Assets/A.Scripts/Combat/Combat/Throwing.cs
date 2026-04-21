using UnityEngine;

public class Throwing : MonoBehaviour
{

    [Header("References")]
    private Transform cam;
    private Transform attackOrigin;
    public GameObject objectToThrow;
    private PlayerAnimator playerAnimator;

    [Header("Settings")]
    [SerializeField] private int totalThrows;
    [SerializeField] private float throwCooldown;

    [Header("Throwing")]
    [SerializeField] private KeyCode throwKey = KeyCode.Mouse1;
    [SerializeField] private float throwForce;
    [SerializeField] private float throwUpwardForce;

    bool readyToThrow;

    void Awake()
    {
        cam = transform.Find("Camera Holder");
        attackOrigin = cam.Find("Throw Origin");
        playerAnimator = GetComponent<PlayerAnimator>();
    }

    private void Start()
    {
        readyToThrow = true;
    }

    private void Update()
    {
        if(Input.GetKeyDown(throwKey) && readyToThrow && totalThrows > 0)
        {
            Throw();
        }
    }

    private void Throw()
    {
        readyToThrow = false;
        playerAnimator.ChangeAnimationState(PlayerAnimator.THROW);

        // Instantiate throwable
        GameObject projectile = Instantiate(objectToThrow, attackOrigin.position, cam.rotation);

        Rigidbody projectileRB = projectile.GetComponent<Rigidbody>();

        Vector3 forceToAdd = cam.transform.forward * throwForce + transform.up * throwUpwardForce;

        projectileRB.AddForce(forceToAdd, ForceMode.Impulse);

        totalThrows--;

        // Cooldown
        Invoke(nameof(ResetThrow), throwCooldown);
    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }
}
