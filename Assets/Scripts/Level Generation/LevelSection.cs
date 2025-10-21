using System.Collections.Generic;
using UnityEngine;

public class LevelSection : MonoBehaviour
{
    Dictionary<Vector3, bool> possibleDirections = new Dictionary<Vector3, bool>()
    {
        { Vector3.forward, true },
        { Vector3.back, true },
        { Vector3.left, true },
        { Vector3.right, true }
    };

    void CheckDirections()
    {
        foreach (Vector3 direction in Directions.AllDirections)
        {
            Ray ray = new Ray(transform.position + (direction * 10f) + new Vector3(0, 5, 0), Vector3.down);

            if (Physics.Raycast(ray, out RaycastHit hit, 10f))
            {
                possibleDirections[direction] = false;
            }
        }
    }

    public List<Vector3> GetAvailableDirections()
    {
        List<Vector3> availableDirections = new List<Vector3>();
        foreach (var direction in possibleDirections)
        {
            if (direction.Value)
            {
                availableDirections.Add(direction.Key);
            }
        }
        //shuffle result
        for (int i = availableDirections.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            Vector3 temp = availableDirections[i];
            availableDirections[i] = availableDirections[j];
            availableDirections[j] = temp;
        }

        return availableDirections;
    }

    public void SpawnNewSections(Transform prefab, int x)
    {
        Debug.Log(x + ": Spawning new sections");
        CheckDirections();

        List<Vector3> availableDirections = GetAvailableDirections();

        Debug.Log(x + ": Available directions: " + availableDirections.Count);

        int sectionsToSpawn;

        sectionsToSpawn = Random.Range(1,  availableDirections.Count + 1);

        for (int i = sectionsToSpawn; i > 0; i--)
        {
            Vector3 direction = availableDirections[i - 1];

            Transform obj = Instantiate(prefab, transform.position + (direction * 10f), Quaternion.identity);
            LevelSection section = obj.GetComponent<LevelSection>();
            LevelGenerationManager.instance.AddSection(section);
            section.SetSpawnerDirection(direction);

            availableDirections.RemoveAt(i - 1);
        }

        LevelGenerationManager.instance.DecreaseNumberOfSections(sectionsToSpawn);
    }

    public void SetSpawnerDirection(Vector3 direction)
    {
        possibleDirections[-direction] = false;
    }
}
