using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{

    public string nameOfScene;

    public void OnCutsceneEnd()
    {
        SceneManager.LoadScene(nameOfScene); // or use build index
    }
}
