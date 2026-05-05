using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField]
    AudioClip attackSound;
    [SerializeField]
    AudioSource audioSource;

    [SerializeField] private Animator armsAnimator;
    private PlayerAnimator playerAnimator;

    private void Awake()
    {
        playerAnimator = GetComponent<PlayerAnimator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LightAttack();
        }
    }

    void LightAttack()
    {
        armsAnimator.SetInteger("LightAtkVar", Random.Range(0, 2));
        armsAnimator.SetTrigger("LightAtk");
    }

    public void PlayAttackSound()
    {
        audioSource.pitch = Random.Range(0.5f, 1.2f);
        audioSource.PlayOneShot(attackSound);
    }
}
