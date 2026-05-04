using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    [Header("Health")]
    public int playerHealth = 5;
    [SerializeField] private int maxHealth = 5;
    private Animator playerAnimator;
    [SerializeField] private GameObject[] healthUI;

    void Awake()
    {
        playerAnimator = GetComponent<Animator>();

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
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

        if (playerHealth < 0)
            playerHealth = 0;

        if (playerHealth <= 0)
        {
            playerAnimator.SetTrigger("isDead");

            if (GameOverUI.Instance != null)
                GameOverUI.Instance.ShowGameOver();
            else
                Debug.LogError("GameOverUI.Instance is NULL!");
        }
        
        SetActiveCount(playerHealth);
    }

    // Placeholder — damage system TBD
    public int GetAttackDamage() => 1;

    // Placeholder — called by PlayerMutationHandler after multipliers change
    public void UpdateAttackDamage() { }
}
