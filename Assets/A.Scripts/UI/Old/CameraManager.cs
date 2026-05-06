using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    [Header("Registered Cameras")]
    public Camera gameplayCamera;
    public Camera menuCamera;
    public Camera logbookCamera;
    public Camera mutationInventoryCamera;

    private void Awake()
    {
        Instance = this;
        DisableAllCameras();
        EnableGameplayCamera();
    }

    public void DisableAllCameras()
    {
        if (gameplayCamera) gameplayCamera.enabled = false;
        if (menuCamera) menuCamera.enabled = false;
        if (logbookCamera) logbookCamera.enabled = true;
        if (mutationInventoryCamera) mutationInventoryCamera.enabled = true;
    }

    public void EnableGameplayCamera()
    {
        DisableAllCameras();
        if (gameplayCamera) gameplayCamera.enabled = true;
    }

    public void EnableMenuCamera()
    {
        DisableAllCameras();
        if (menuCamera) menuCamera.enabled = true;
    }

    public void EnableLogbookCamera()
    {
        DisableAllCameras();
        if (logbookCamera) logbookCamera.enabled = true;
    }

    public void EnableMutationInventoryCamera()
    {
        DisableAllCameras();
        if (mutationInventoryCamera) mutationInventoryCamera.enabled = true;
    }
}
