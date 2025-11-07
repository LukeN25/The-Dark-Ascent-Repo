using System.Collections.Generic;
using UnityEngine;
using FOW.Mutations;


namespace FOW.Logbook
{
    public class LogbookManager : MonoBehaviour
    {
        public static LogbookManager Instance { get; private set; }

        [Header("Enemy Database")]
        public List<EnemyInfo> enemies = new List<EnemyInfo>();

        [Header("Panels")]
        public GameObject enemyListPanel;       
        public GameObject mutationPanel;        
        public GameObject mutationDetailPanel;  

        [Header("Mutation UI")]
        public MutationListPopulator mutationListPopulator;
        public MutationDetailUI mutationDetailUI; 

        private void Awake()
        {
            Instance = this;
            Debug.Log(" LogbookManager Awake() ran and instance was set.");
        }

       
        public void OpenEnemyListPanel()
        {
            CloseAllPanels();

            if (enemyListPanel != null)
                enemyListPanel.SetActive(true);
        }

       
        public void OpenMutationPanel(EnemyInfo enemy)
        {
            if (!enemy.isUnlocked)
            {
                Debug.LogWarning(" Tried to open mutation panel for locked enemy: " + enemy.enemyName);
                return;
            }

            CloseAllPanels();

            Debug.Log($" Opening mutation panel for: {enemy.enemyName}");

            if (mutationPanel != null)
                mutationPanel.SetActive(true);

            if (mutationListPopulator != null)
                mutationListPopulator.SetEnemy(enemy);
            else
                Debug.LogError(" MutationListPopulator is not assigned in LogbookManager.");
        }

    
        public void OpenMutationDetail(MutationInfo mutation)
        {
            if (mutation == null)
            {
                Debug.LogError(" MutationInfo was NULL when trying to open mutation detail.");
                return;
            }

            CloseAllPanels();

            Debug.Log($" Opening mutation detail for: {mutation.mutationName}");

            if (mutationDetailPanel != null)
                mutationDetailPanel.SetActive(true);

            if (mutationDetailUI != null)
                mutationDetailUI.ShowMutation(mutation);
            else
                Debug.LogWarning(" No MutationDetailUI script assigned, detail panel will not update.");
        }

        
        public void CloseAllPanels()
        {
            if (enemyListPanel != null)
                enemyListPanel.SetActive(false);

            if (mutationPanel != null)
                mutationPanel.SetActive(false);

            if (mutationDetailPanel != null)
                mutationDetailPanel.SetActive(false);
        }
    }
}
