using System.Collections.Generic;
using UnityEngine;

public class LevelSection : MonoBehaviour
{
    Vector3 gridPosition;

    private void Start()
    {
        CheckDirections();
    }

    Dictionary<Vector3, bool> possibleDirections = new Dictionary<Vector3, bool>()
    {
        { Vector3.forward, true },
        { Vector3.back, true },
        { Vector3.left, true },
        { Vector3.right, true }
    };

    void CheckDirections()
    {
        foreach (var direction in possibleDirections.Keys)
        {
            Ray ray = new Ray((direction * 10f) + new Vector3(0, 5, 0), Vector3.down);

            if (Physics.Raycast(ray, out RaycastHit hit, 10f))
            {
                possibleDirections[direction] = false;
            }
            Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red, 100f);
            Debug.Log(direction + ": " + possibleDirections[direction]);
        }
    }

    public Vector3[] GetAvailableDirections()
    {
        List<Vector3> availableDirections = new List<Vector3>();
        foreach (var direction in possibleDirections)
        {
            if (direction.Value)
            {
                availableDirections.Add(direction.Key);
            }
        }
        return availableDirections.ToArray();
    }

    public Vector3 GetPosition => gridPosition;
}
