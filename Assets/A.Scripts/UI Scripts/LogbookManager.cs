using System.Collections.Generic;
using UnityEngine;
using FOW.Mutations; 


namespace FOW.Logbook
{
    public class LogbookManager : MonoBehaviour
    {
        public static LogbookManager Instance { get; private set; }


        [Header("Enemy Settings")]
        public Transform enemyPreviewContainer; 
        public GameObject enemyPreviewPrefab;  

        [Header("Enemy List")]
        public List<EnemyInfo> enemies = new List<EnemyInfo>();

        private void Awake()
        {
            Instance = this;
        }

       
        public void UnlockEnemy(string enemyName)
        {
            EnemyInfo enemy = enemies.Find(e => e.enemyName == enemyName);
            if (enemy != null)
                enemy.isUnlocked = true;
        }

       
        public void OpenMutationPanel(EnemyInfo enemy)
        {
            if (!enemy.isUnlocked) return;
            Debug.Log($"Opening mutation panel for {enemy.enemyName}");
            // TODO: show mutation panel UI
        }

      
        public void OpenMutationDetail(MutationInfo mutation)
        {
            Debug.Log($"Opening details for mutation {mutation.mutationName}");
            // TODO: show mutation details UI
        }
    }
}
