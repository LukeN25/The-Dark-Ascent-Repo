using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

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

    [SerializeField] private int playerHealth;

    [SerializeField] private int attackDamage;

    [SerializeField] private float moveSpeed;

    [SerializeField] private float acceleration;

    public int GetAttackDamage() => attackDamage;
    public float GetMoveSpeed() => moveSpeed;
    public float GetAcceleration() => acceleration;
}
