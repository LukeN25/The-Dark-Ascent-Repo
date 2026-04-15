using UnityEngine;

public class PlayerRotater : MonoBehaviour
{
    [SerializeField] Transform playerObject;
    [SerializeField] GameObject cursorObject;
    [SerializeField] float rotationSpeed = 10f;

    public Camera mainCamera;
    Plane groundPlane;
    bool isInitialized = false;

    private void Start()
    {
        //mainCamera = Camera.main;

        if (mainCamera != null && playerObject != null)
        {
            groundPlane = new Plane(Vector3.up, playerObject.position);
            isInitialized = true;
        }
        else
        {
            Debug.LogWarning("PlayerRotater: Camera or player object not found on Start.");
        }
    }

    void Update()
    {
        if (!EnsureInitialized())
            return;

        if (Cursor.lockState != CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }

        Vector3 mousePos = Input.mousePosition;

        if (!IsValidMousePosition(mousePos))
        {
            mousePos = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
        }

        try
        {
            Ray ray = mainCamera.ScreenPointToRay(mousePos);

            if (groundPlane.Raycast(ray, out float distance))
            {
                Vector3 worldPosition = ray.GetPoint(distance);
                cursorObject.transform.position = worldPosition;
            }
            else
            {
                cursorObject.transform.position = playerObject.position;
            }
        }
        catch
        {
            cursorObject.transform.position = playerObject.position + playerObject.forward * 2f;
            return;
        }

        
        Vector3 lookDir = cursorObject.transform.position - playerObject.position;

        lookDir.y = 0;
        if (lookDir.magnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(lookDir.normalized, Vector3.up);

            float targetYRotation = targetRotation.eulerAngles.y;

            Quaternion yRotationOnly = Quaternion.Euler(0, targetYRotation, 0);

            playerObject.rotation = Quaternion.Slerp(
                playerObject.rotation,
                yRotationOnly,
                rotationSpeed * Time.deltaTime
            );
        }
    }

    bool EnsureInitialized()
    {
        if (isInitialized && mainCamera != null && playerObject != null)
            return true;

        if (mainCamera == null)
            mainCamera = Camera.main;

        if (mainCamera != null && playerObject != null)
        {
            groundPlane = new Plane(Vector3.up, playerObject.position);
            isInitialized = true;
            return true;
        }

        return false;
    }

    bool IsValidMousePosition(Vector3 mousePos)
    {
        if (float.IsNaN(mousePos.x) || float.IsInfinity(mousePos.x) ||
            float.IsNaN(mousePos.y) || float.IsInfinity(mousePos.y))
            return false;

        float margin = 100f;
        if (mousePos.x < -margin || mousePos.x > Screen.width + margin ||
            mousePos.y < -margin || mousePos.y > Screen.height + margin)
            return false;

        return true;
    }
}