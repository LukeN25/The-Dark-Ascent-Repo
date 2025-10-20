using UnityEngine;

[RequireComponent(typeof(Collider))]
public class MutationPart : MonoBehaviour
{
    public string partName;
    public Transform focusPoint;

    private void OnMouseDown()
    {
        if (!MenuManager.Instance) return;
        MenuManager.Instance.OnPartClicked(this);
    }
}
