using UnityEngine;

public class LevelGenPrefabHolder : MonoBehaviour
{
    public static LevelGenPrefabHolder instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField]
    private Transform BlockPrefab;

    public Transform GetBlockPrefab()
    {
        return BlockPrefab;
    }
}
