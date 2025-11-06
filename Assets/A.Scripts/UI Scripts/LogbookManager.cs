using System.Collections.Generic;
using UnityEngine;
using FOW.Mutations; 


namespace FOW.Logbook
{
    public class LogbookManager : MonoBehaviour
    {
        public static LogbookManager Instance { get; private set; }

        [Header("Enemy List")]
        public List<EnemyInfo> enemies = new List<EnemyInfo>();

        private void Awake() => Instance = this;

        public void UnlockEnemy(string enemyName)
        {
            var enemy = enemies.Find(e => e.enemyName == enemyName);
            if (enemy != null) enemy.isUnlocked = true;
        }

        public void OpenMutationPanel(EnemyInfo enemy)
        {
            if (!enemy.isUnlocked) return;
            Debug.Log($"Open mutation panel for {enemy.enemyName}");
            // TODO: Show mutation panel for this enemy
        }

        public void OpenMutationDetail(FOW.Mutations.MutationInfo mutation)
        {
            Debug.Log($"Open mutation detail: {mutation.mutationName}");
            // TODO: Show details panel for this mutation
        }
    }
}
