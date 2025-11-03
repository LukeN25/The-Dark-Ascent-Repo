using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MutationEntry : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image mutationIcon;
    [SerializeField] private GameObject lockOverlay;
    [SerializeField] private Button button; 

    
    private string enemyName;
    private MutationInfo mutationInfo;
    private LogbookManager manager;
    private bool unlocked;

    public void Init(string enemy, MutationInfo info, bool isUnlocked, LogbookManager logbook)
    {
        enemyName = enemy;
        mutationInfo = info;
        unlocked = isUnlocked;
        manager = logbook;

        if (mutationIcon != null)
            mutationIcon.sprite = isUnlocked ? info.mutationIcon : null;

        lockOverlay?.SetActive(!isUnlocked);

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
            manager.OpenMutationDetail(mutationInfo);
        else
            Debug.LogWarning("LogbookManager reference missing on MutationEntry.");
    }
}
