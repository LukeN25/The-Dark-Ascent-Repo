using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EnemyDatabase", menuName = "Game/EnemyDatabase")]
public class EnemyDatabase : ScriptableObject
{
    public List<EnemyInfo> enemies;
}

[System.Serializable]
public class EnemyInfo
{
    public string enemyName;
    public GameObject enemyModel; 
    public Sprite enemyIcon;
    public List<MutationInfo> mutations;
}

[System.Serializable]
public class MutationInfo
{
    public string mutationName;
    public Sprite mutationIcon;
    [TextArea]
    public string description;
}
