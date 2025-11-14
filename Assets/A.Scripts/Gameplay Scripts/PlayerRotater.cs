using UnityEngine;

public class PlayerRotater : MonoBehaviour
{
    //Turns player to face the mouse cursor

    [SerializeField] Transform playerObject;

    [SerializeField] Transform cursorObject;

    [SerializeField] float rotationSpeed = 10f;

    Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Cursor.lockState != CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        cursorObject.position = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.transform.position.y - playerObject.position.y));

        Vector3 lookDir = cursorObject.position - playerObject.position;

        playerObject.rotation = Quaternion.Slerp(playerObject.rotation, Quaternion.LookRotation(lookDir.normalized, Vector3.up), rotationSpeed * Time.deltaTime);
    }
}
