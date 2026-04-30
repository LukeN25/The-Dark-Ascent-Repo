using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    public void OnCutsceneEnd()
    {
        SceneManager.LoadScene("Playtest"); // or use build index
    }
}
