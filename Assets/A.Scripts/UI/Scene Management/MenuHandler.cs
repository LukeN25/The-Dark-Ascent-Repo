using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    void Awake()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
}
