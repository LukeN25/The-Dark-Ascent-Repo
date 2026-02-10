using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    private GameObject playerBody;

    float xRotation = 0f;

    void Start()
    {
        playerBody = GameObject.Find ("Player");

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.transform.Rotate(Vector3.up * mouseX);
    }
}
