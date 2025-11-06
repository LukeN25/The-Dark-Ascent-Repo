using UnityEngine;

namespace FOW.Logbook
{
    [System.Serializable]
    public class EnemyInfo
    {
        public string enemyName;
        public Sprite enemyIcon;
        public GameObject enemyModelPrefab; 
        public string description;
        public bool isUnlocked;
    }
}
