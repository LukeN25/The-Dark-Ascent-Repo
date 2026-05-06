using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI Instance;

    [Header("UI Root")]
    public GameObject gameOverCanvas;
    public GameObject pauseCanvas;

    private void Awake()
    {
        Instance = this;
        gameOverCanvas.SetActive(false);
    }

    public void ShowGameOver()
    {
        StartCoroutine(PlayDeathAnim());
    }

    IEnumerator PlayDeathAnim()
    {
        yield return new WaitForSeconds(2);

        gameOverCanvas.SetActive(true);
        pauseCanvas.SetActive(false);
        Time.timeScale = 0f; 
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
