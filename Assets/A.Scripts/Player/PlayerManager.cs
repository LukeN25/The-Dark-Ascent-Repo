using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    [Header("Health")]
    public int playerHealth = 5;
    [SerializeField] private int maxHealth = 5;
    private Animator playerAnimator;
    [SerializeField] private GameObject[] healthUI;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerCombat playerCombat;
    [SerializeField] private PlayerDodge playerDodge;
    [SerializeField] private PlayerHurtbox playerHurtbox;
    [SerializeField] private PlayerAnimator PlayerAnimator;
    [SerializeField] private Throwing throwing;
    [SerializeField] private HeadBobController headBobController;
    [SerializeField] public bool knockbackEnabled = true;
    [SerializeField] private float playerKnockbackStrength = 5f;
    [SerializeField] private float heartbeatPitch = 1f;
    [SerializeField] private AudioSource heartbeat;
    

    public float GetPlayerKnockbackStrength() => playerKnockbackStrength;

    void Awake()
    {
        playerAnimator = GetComponent<Animator>();

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        heartbeat.pitch = heartbeatPitch;
    }

    public void SetActiveCount(int healthToKeep)
    {
        for (int i = 0; i < healthUI.Length; i++)
        {
            healthUI[i].SetActive(i < healthToKeep);
        }
    }

    public int GetHealth() => playerHealth;
    public int GetMaxHealth() => maxHealth;

    public void TakeDamage(DamageInfo damageInfo)
    {
        Debug.Log($"Player took {damageInfo.GetDamage()} damage!");
        playerHealth -= damageInfo.GetDamage();

        heartbeatPitch = Mathf.Lerp(0.5f, 1.5f, (float)playerHealth / maxHealth);
        heartbeat.pitch = heartbeatPitch;

        if (playerHealth < 0)
            playerHealth = 0;

        if (playerHealth <= 0)
        {
            playerAnimator.SetTrigger("isDead");

            playerCombat.enabled = false;
            playerMovement.enabled = false;
            playerDodge.enabled = false;
            playerHurtbox.enabled = false;
            PlayerAnimator.enabled = false;
            throwing.enabled = false;

            if (GameOverUI.Instance != null)
                GameOverUI.Instance.ShowGameOver();
            else
                Debug.LogError("GameOverUI.Instance is NULL!");
        }
        
        SetActiveCount(playerHealth);

        if (knockbackEnabled)
            playerMovement.ApplyKnockback(damageInfo.GetKnockbackDirection(), damageInfo.GetKnockbackStrength());
        if (playerHealth > 0 && headBobController != null)
            headBobController.TriggerHitShake();
    }

    // Placeholder — damage system TBD
    public int GetAttackDamage() => 1;
    
    public int GetGrenadeDamage() => 5;

    // Placeholder — called by PlayerMutationHandler after multipliers change
    public void UpdateAttackDamage() { }
}
