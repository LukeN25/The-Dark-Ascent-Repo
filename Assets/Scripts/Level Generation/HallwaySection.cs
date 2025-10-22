using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HallwaySection : LevelSection
{
    public override void FinalizeSection()
    {
        List<Transform> prefabsWithoutHallways = LevelGenerationManager.instance.GetSectionPrefabsWithoutType(SectionType.Hallway);

        FinalizeHalls(prefabsWithoutHallways.ToArray());
    }

    public void FinalizeHalls(Transform[] prefabs)
    {
        List<Vector3> availableDirections = new List<Vector3>();

        foreach (Vector3 dir in possibleDirections)
        {
        } 

        foreach (Vector3 direction in possibleDirections)
        {
            Ray ray = new Ray(transform.position + (direction * 10f) + new Vector3(0, 5, 0), Vector3.down);
            if (!Physics.Raycast(ray, out RaycastHit hit, 10f))
            {
                availableDirections.Add(direction);
                Debug.DrawRay(transform.position + (direction * 10f) + new Vector3(0, 5, 0), Vector3.down * 10f, Color.green, 100f);
            }
            else
            {
                Debug.DrawRay(transform.position + (direction * 10f) + new Vector3(0, 5, 0), Vector3.down * 10f, Color.red, 100f);
            }
        }

        //shuffle result
        for (int i = availableDirections.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            Vector3 temp = availableDirections[i];
            availableDirections[i] = availableDirections[j];
            availableDirections[j] = temp;
        }

        if (availableDirections.Count == 0)
        {
            return;
        }

        int sectionsToSpawn;
        sectionsToSpawn = availableDirections.Count;

        for (int i = sectionsToSpawn; i > 0; i--)
        {
            LevelSection section = SpawnSection(prefabs, availableDirections, i - 1);

            availableDirections.RemoveAt(i - 1);
        }
        Debug.Log("-----Finished Finalizing Hallway Section at: " + transform.position);
    }
}
