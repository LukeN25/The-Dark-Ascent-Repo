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

        [Header("Logbook Panels")]
        public GameObject enemyListPanel;
        public GameObject mutationPanel;
        public GameObject mutationDetailPanel;

        [Header("Mutation UI")]
        public MutationListPopulator mutationListPopulator;
        public MutationDetailUI mutationDetailUI;

        [Header("Mutation Inventory")]
        public MutationInventoryUI mutationInventoryUI;

        [Header("Root Canvases")]
        public GameObject canvasLogbook;
        public GameObject canvasMutationInventory;

        private bool logbookOpen = false;
        private bool mutationInventoryOpen = false;

        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            HandleLogbookInput();
            HandleInventoryInput();
            HandleEscapeKey();
        }

       

        private void HandleLogbookInput()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (logbookOpen)
                    CloseLogbook();
                else if (!mutationInventoryOpen)
                    OpenLogbook();
            }
        }

        private void HandleInventoryInput()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (mutationInventoryOpen)
                    CloseMutationInventory();
                else if (!logbookOpen)
                    OpenMutationInventory();
            }
        }

        private void HandleEscapeKey()
        {
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

                if (logbookOpen)
                {
                    CloseLogbook();
                    return;
                }

                if (mutationInventoryOpen)
                {
                    CloseMutationInventory();
                    return;
                }
            }
        }

        

        public void OpenLogbook()
        {
            logbookOpen = true;
            mutationInventoryOpen = false;

            canvasMutationInventory.SetActive(false);
            canvasLogbook.SetActive(true);

            CloseAllPanels();
            enemyListPanel.SetActive(true);
        }

        public void CloseLogbook()
        {
            logbookOpen = false;
            CloseAllPanels();
            canvasLogbook.SetActive(false);
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

        

        public void OpenMutationInventory()
        {
            mutationInventoryOpen = true;
            logbookOpen = false;

            canvasLogbook.SetActive(false);
            canvasMutationInventory.SetActive(true);

            mutationInventoryUI.Show();
        }

        public void CloseMutationInventory()
        {
            mutationInventoryOpen = false;
            mutationInventoryUI.Hide();

            canvasMutationInventory.SetActive(false);
        }

        

        public void CloseAllPanels()
        {
            enemyListPanel.SetActive(false);
            mutationPanel.SetActive(false);
            mutationDetailPanel.SetActive(false);
        }
    }
}
