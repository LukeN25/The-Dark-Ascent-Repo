using UnityEngine;
using FOW.Mutations;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    [SerializeField] private PlayerMutationHandler m_MutationHandler;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    [SerializeField] public int playerHealth = 5;

    [SerializeField] private int maxHealth = 5;

    [SerializeField] private int attackDamage;
    [SerializeField] private float knockbackForce;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float acceleration;

    private void Update()
    {
        Mathf.Clamp(playerHealth, 0, maxHealth);
    }

    public void TakeDamage(DamageInfo damageInfo)
    {
        playerHealth -= damageInfo.GetDamage();

        if (playerHealth < 0)
            playerHealth = 0;

        if (playerHealth <= 0)
        {
            if (GameOverUI.Instance != null)
            {
                GameOverUI.Instance.ShowGameOver();
            }
            else
            {
                Debug.LogError("GameOverUI.Instance is NULL!");
            }
        }
    }

    public void UpdateAttackDamage()
    {
        attackDamage = Mathf.RoundToInt(attackDamage * m_MutationHandler.damageMultiplier);
    }


    public int GetAttackDamage() => attackDamage;
    public float GetKnockbackForce() => knockbackForce;
    public float GetMoveSpeed() => moveSpeed;
    public float GetAcceleration() => acceleration;

    public int GetHealth() => playerHealth;
    public int GetMaxHealth() => maxHealth;
}
