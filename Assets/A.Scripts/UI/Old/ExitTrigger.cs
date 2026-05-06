using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    public GameObject exitCanvas;   

    private bool uiOpen = false;

    private void Start()
    {
        if (exitCanvas != null)
            exitCanvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (uiOpen) return;

        if (other.CompareTag("Player"))
        {
            uiOpen = true;
            exitCanvas.SetActive(true);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            Time.timeScale = 0f;
        }
    }

    public void CloseUI()
    {
        exitCanvas.SetActive(false);
        uiOpen = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
