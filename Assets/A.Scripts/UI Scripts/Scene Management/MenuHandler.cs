using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
}
