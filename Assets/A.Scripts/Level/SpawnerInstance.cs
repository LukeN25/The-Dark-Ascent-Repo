using UnityEngine;

public class SpawnerInstance : MonoBehaviour
{
    [SerializeField] private InfiniteEnemySpawner<Transform> enemyTable;
    private LevelTimer levelTimer;
    private float timer = 0;
    private float timeBetweenSpawns;


    void Start()
    {
        levelTimer = FindFirstObjectByType(typeof(LevelTimer)) as LevelTimer;
    }

    void Update()
    {
        timer += Time.deltaTime;
        timeBetweenSpawns = levelTimer.scalingTime;
        if (timer >= timeBetweenSpawns)
        {
            timer = 0;
            SpawnEnemies();
        }
    }

    private void SpawnEnemies()
    {
        Transform enemy = enemyTable.GetRandom();
        Instantiate(enemy, transform.position, transform.rotation);
    }
}
