using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject pauseMenu;

    public AudioMixer audioMixer;
    public PlayerLook playerLook;

    bool paused = false;

    void Awake()
    {
        pauseMenu.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (paused == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Pause();
            }            
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
               Resume();
            }
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        paused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    
    public void Resume()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        paused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

    public void SetSensitivity(float sens)
    {
        playerLook.mouseSensitivity = sens;
    }
}
