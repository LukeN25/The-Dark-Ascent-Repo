using System.Collections.Generic;
using UnityEngine;
using FOW.Mutations;

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

        [Header("Mutations this enemy can drop")]
        public List<MutationInfo> possibleMutations = new List<MutationInfo>();
    }
}
