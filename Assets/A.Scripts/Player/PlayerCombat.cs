using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField]
    AudioClip attackSound;
    [SerializeField]
    AudioSource audioSource;

    [SerializeField] public float attackCooldown = 0.5f;
    private PlayerAnimator playerAnimator;
    private float lastAttackTime = -Mathf.Infinity;

    private void Awake()
    {
        playerAnimator = GetComponent<PlayerAnimator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= lastAttackTime + attackCooldown)
        {
            LightAttack();
        }
    }

    void LightAttack()
    {
        lastAttackTime = Time.time;
        playerAnimator.TriggerLightAttack(attackCooldown);
    }

    public void PlayAttackSound()
    {
        audioSource.pitch = Random.Range(0.5f, 1.2f);
        audioSource.PlayOneShot(attackSound);
    }
}
