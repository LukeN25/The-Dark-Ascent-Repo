using UnityEngine;

public class UISoundManager : MonoBehaviour
{
    public static UISoundManager Instance;

    [Header("UI Sounds")]
    public AudioClip hoverSound;
    public AudioClip clickSound;

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = gameObject.AddComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }

    public void PlayHover()
    {
        if (hoverSound != null)
            audioSource.PlayOneShot(hoverSound);
    }

    public void PlayClick()
    {
        if (clickSound != null)
            audioSource.PlayOneShot(clickSound);
    }
}
