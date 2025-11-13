using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonSceneChanger : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string sceneToLoad2;

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void LoadScene2()
    {
        SceneManager.LoadScene(sceneToLoad2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
