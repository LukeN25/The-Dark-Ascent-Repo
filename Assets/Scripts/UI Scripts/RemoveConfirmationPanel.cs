using UnityEngine;
using UnityEngine.UI;
using System;

public class RemoveConfirmationPanel : MonoBehaviour
{
    public Button confirmButton;
    public Button cancelButton;

    private Action<bool> resultCallback;

    void Awake()
    {
        gameObject.SetActive(false);
        confirmButton.onClick.AddListener(() => { Confirm(true); });
        cancelButton.onClick.AddListener(() => { Confirm(false); });
    }

    public void ShowConfirmation(Action<bool> callback)
    {
        resultCallback = callback;
        gameObject.SetActive(true);
    }

    void Confirm(bool confirm)
    {
        gameObject.SetActive(false);
        resultCallback?.Invoke(confirm);
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}
