using UnityEngine;
using TMPro;

public class Throwing : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform attackOrigin;
    [SerializeField] private Transform cameraHolder;
    [SerializeField] private GameObject objectToThrow;
    [SerializeField] private TextMeshProUGUI grenadeNumber;
    private PlayerAnimator playerAnimator;
    
    [Header("Settings")]
    public int totalThrows;
    [SerializeField] private float throwCooldown;
    [SerializeField] private KeyCode throwKey = KeyCode.Mouse1;

    [Header("Throw Force")]
    [SerializeField] private float throwForce = 20f;
    [SerializeField] private float throwUpwardForce = 2f;

    [SerializeField] private float throwLength;

    bool readyToThrow;

    void Awake()
    {
        playerAnimator = GetComponent<PlayerAnimator>();
    }

    private void Start()
    {
        readyToThrow = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(throwKey) && readyToThrow && totalThrows > 0)
            Throw();
        grenadeNumber.text = totalThrows.ToString();
    }

    private void Throw()
    {
        readyToThrow = false;
        playerAnimator.LockActionAnimation(throwLength > 0 ? throwLength : throwCooldown);
        playerAnimator.ChangeAnimationState(PlayerAnimator.THROW);

        GameObject projectile = Instantiate(objectToThrow, attackOrigin.position, attackOrigin.rotation);

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 force = cameraHolder.forward * throwForce + Vector3.up * throwUpwardForce;
            rb.AddForce(force, ForceMode.Impulse);
        }

        totalThrows--;
        Invoke(nameof(ResetThrow), throwCooldown);
    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }
}
