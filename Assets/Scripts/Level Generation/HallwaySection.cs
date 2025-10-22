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
        List<Vector3> availableDirections = GetAvailableDirections();

        if (availableDirections.Count == 0)
        {
            return;
        }

        int sectionsToSpawn;
        sectionsToSpawn = availableDirections.Count;

        for (int i = sectionsToSpawn; i > 0; i--)
        {
            int randomIndex = UnityEngine.Random.Range(0, prefabs.Length);
            LevelSection section = SpawnSection(prefabs[randomIndex], availableDirections, i - 1);

            availableDirections.RemoveAt(i - 1);
        }
    }
}
