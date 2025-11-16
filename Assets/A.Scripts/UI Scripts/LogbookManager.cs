using System.Collections.Generic;
using UnityEngine;
using FOW.Mutations;

namespace FOW.Logbook
{
    public class LogbookManager : MonoBehaviour
    {
        public static LogbookManager Instance;

        [Header("Enemy Database")]
        public List<EnemyInfo> enemies = new List<EnemyInfo>();   

        [Header("Panels")]
        public GameObject enemyListPanel;
        public GameObject mutationPanel;
        public GameObject mutationDetailPanel;

        [Header("Mutation UI")]
        public MutationListPopulator mutationListPopulator;
        public MutationDetailUI mutationDetailUI;

        private bool logbookOpen = false;

        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (!logbookOpen)
                    OpenEnemyListPanel();
                else
                    CloseLogbook();
            }

            if (!logbookOpen) return;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (mutationDetailPanel.activeSelf)
                {
                    BackFromMutationDetail();
                    return;
                }

                if (mutationPanel.activeSelf)
                {
                    BackFromMutationPanel();
                    return;
                }

                CloseLogbook();
            }
        }

        public void OpenEnemyListPanel()
        {
            logbookOpen = true;
            CloseAllPanels();
            enemyListPanel.SetActive(true);
        }

        public void OpenMutationPanel(EnemyInfo enemy)
        {
            if (!enemy.isUnlocked) return;

            CloseAllPanels();
            mutationPanel.SetActive(true);
            mutationListPopulator.SetEnemy(enemy);
        }

        public void OpenMutationDetail(MutationInfo mutation)
        {
            CloseAllPanels();
            mutationDetailPanel.SetActive(true);
            mutationDetailUI.ShowMutation(mutation);
        }

        public void BackFromMutationPanel()
        {
            CloseAllPanels();
            enemyListPanel.SetActive(true);
        }

        public void BackFromMutationDetail()
        {
            CloseAllPanels();
            mutationPanel.SetActive(true);
        }

        public void CloseLogbook()
        {
            logbookOpen = false;
            CloseAllPanels();
        }

        public void CloseAllPanels()
        {
            enemyListPanel.SetActive(false);
            mutationPanel.SetActive(false);
            mutationDetailPanel.SetActive(false);
        }
    }
}
