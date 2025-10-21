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
        if (confirmButton != null) confirmButton.onClick.AddListener(() => Confirm(true));
        if (cancelButton != null) cancelButton.onClick.AddListener(() => Confirm(false));
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
