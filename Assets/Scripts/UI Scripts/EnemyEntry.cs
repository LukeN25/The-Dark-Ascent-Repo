using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class EnemyEntry : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private RawImage previewImage;
    [SerializeField] private GameObject lockOverlay;
    [SerializeField] private TMP_Text enemyNameText;
    [SerializeField] private Button button; 

    
    private EnemyInfo enemyInfo;
    private LogbookManager manager;
    private bool unlocked;

    
    public void Init(EnemyInfo info, bool isUnlocked, LogbookManager logbook)
    {
        enemyInfo = info;
        manager = logbook;
        unlocked = isUnlocked;

        enemyNameText.text = isUnlocked ? info.enemyName : "???";
        lockOverlay?.SetActive(!isUnlocked);
        if (previewImage != null)
            previewImage.enabled = isUnlocked;

       
        if (button != null)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(OnClick);
            button.interactable = isUnlocked;
        }
    }

    
    public void OnClick()
    {
        if (!unlocked) return;
        if (manager != null)
            manager.OpenMutationPanel(enemyInfo);
        else
            Debug.LogWarning("LogbookManager reference missing on EnemyEntry.");
    }
}
