using UnityEngine;

public class CharacterIdleRotation : MonoBehaviour
{
    public float rotationSpeed = 8f;
    private bool active = true;

    void Update()
    {
        if (active)
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
    }

    public void SetActive(bool state)
    {
        active = state;
    }
}
