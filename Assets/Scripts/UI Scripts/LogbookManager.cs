using UnityEngine;
using UnityEngine.UI;

public class LogbookManager : MonoBehaviour
{
    public EnemyDatabase enemyDatabase;
    public GameObject enemyPanel;
    public GameObject mutationPanel;
    public GameObject mutationDetailPanel;

    private PlayerLogbookData playerData;
    private EnemyInfo currentEnemy;
    private MutationInfo currentMutation;

    private bool isOpen = false;

    void Start()
    {
        playerData = LoadPlayerData();
        CloseAllPanels();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isOpen)
                CloseAllPanels();
            else
                OpenEnemyPanel();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && isOpen)
            NavigateBack();
    }

    void OpenEnemyPanel()
    {
        isOpen = true;
        enemyPanel.SetActive(true);
        mutationPanel.SetActive(false);
        mutationDetailPanel.SetActive(false);

        
        PopulateEnemyPanel();
    }

    public void OpenMutationPanel(EnemyInfo enemy)
    {
        currentEnemy = enemy;
        enemyPanel.SetActive(false);
        mutationPanel.SetActive(true);
        mutationDetailPanel.SetActive(false);

        PopulateMutationPanel(enemy);
    }

    public void OpenMutationDetail(MutationInfo mutation)
    {
        currentMutation = mutation;
        mutationPanel.SetActive(false);
        mutationDetailPanel.SetActive(true);

       
    }

    void NavigateBack()
    {
        if (mutationDetailPanel.activeSelf)
        {
            mutationDetailPanel.SetActive(false);
            mutationPanel.SetActive(true);
        }
        else if (mutationPanel.activeSelf)
        {
            mutationPanel.SetActive(false);
            enemyPanel.SetActive(true);
        }
        else
        {
            CloseAllPanels();
        }
    }

    void CloseAllPanels()
    {
        isOpen = false;
        enemyPanel.SetActive(false);
        mutationPanel.SetActive(false);
        mutationDetailPanel.SetActive(false);
    }

    void PopulateEnemyPanel()
    {

    }

    void PopulateMutationPanel(EnemyInfo enemy)
    {
       
    }

    PlayerLogbookData LoadPlayerData()
    {
        
        return new PlayerLogbookData();
    }
}
