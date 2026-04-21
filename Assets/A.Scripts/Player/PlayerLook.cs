using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [Header("Look")]
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private Transform cameraHolder;

    private float xRotation = 0f;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Accumulate and clamp vertical rotation
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -20f, 20f);

        // Vertical tilt on the camera holder, horizontal turn on the player body
        cameraHolder.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}
