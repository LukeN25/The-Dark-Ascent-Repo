using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading;
using System.Collections;

public class LevelSection : MonoBehaviour
{
    // Unity cannot serialize Dictionary types, so we use a serializable class to store the data and convert it to a dictionary at runtime
    [SerializeField]
    protected List<Vector3> possibleDirections = new List<Vector3>()
    {
        Vector3.forward,
        Vector3.back,
        Vector3.left,
        Vector3.right
    };

    [SerializeField]
    Vector3 gridPosition = Vector3.zero;

    protected Vector3 orientation = Vector3.forward;

    [SerializeField]
    SectionType sectionType;

    bool isFinalized = false;

    public List<Vector3> GetAvailableDirections()
    {
        List<Vector3> availableDirections = new List<Vector3>();

        foreach (Vector3 direction in possibleDirections)
        {
            Ray ray = new Ray(transform.position + (direction * (10f * transform.localScale.y)) + new Vector3(0, 5, 0), Vector3.down);
            if (!Physics.Raycast(ray, out RaycastHit hit, 10f))
            {
                //Debug.DrawRay(transform.position + (direction * (10f * transform.localScale.y)) + new Vector3(0, 5, 0), Vector3.down * 10f, Color.green, 5f);
                availableDirections.Add(direction);
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

        return availableDirections;
    }

    public virtual void SpawnNewSections(Transform[] prefabs)
    {
        List<Vector3> availableDirections = GetAvailableDirections();

        if( availableDirections.Count == 0)
        {
            return;
        }

        int sectionsToSpawn;
        sectionsToSpawn = UnityEngine.Random.Range(1,  availableDirections.Count + 1);

        for (int i = sectionsToSpawn; i > 0; i--)
        {
            int prefabIndex = UnityEngine.Random.Range(0, prefabs.Length);

            LevelSection section = SpawnSection(prefabs[prefabIndex], availableDirections, i - 1);

            LevelGenerationManager.instance.AddSection(section);
            LevelGenerationManager.instance.RegisterSectionInGrid(section);
            availableDirections.RemoveAt(i - 1);
        }

        LevelGenerationManager.instance.DecreaseNumberOfSections(sectionsToSpawn);
    }

    public void SpawnSingleSection(Transform prefab)
    {
        List<Vector3> availableDirections = GetAvailableDirections();

        if (availableDirections.Count == 0)
        {
            return;
        }

        int sectionsToSpawn;
        sectionsToSpawn = 1;

        LevelSection section = SpawnSection(prefab, availableDirections, 0);
        section.FinalizeSection();

        LevelGenerationManager.instance.DecreaseNumberOfSections(sectionsToSpawn);
    }

    protected LevelSection SpawnSection(Transform prefab, List<Vector3> availableDirections, int i)
    {
        Vector3 direction = availableDirections[i];

        Transform obj = Instantiate(prefab, transform.position + (direction * (10f * transform.localScale.y)), Quaternion.Euler(DetermineRotation(direction)));

        LevelSection section = obj.GetComponent<LevelSection>();

        section.SetGridPosition(this.gridPosition + direction);

        section.SetDirection(direction);
        return section;
    }

    protected Vector3 DetermineRotation(Vector3 direction)
    {
        Vector3 temp = Vector3.zero;

        if (direction == Vector3.forward)
        {
            temp = Vector3.zero;
        }
        else if (direction == Vector3.back)
        {
            temp = new Vector3(0, 180, 0);
        }
        else if (direction == Vector3.left)
        {
            temp = new Vector3(0, -90, 0);
        }
        else
        {
            temp = new Vector3(0, 90, 0);
        }

        return temp;
    }

    public virtual void FinalizeSection()
    {
        foreach (Vector3 dir in possibleDirections)
        {
            if (!LevelGenerationManager.instance.IsGridPositionOccupied(gridPosition + dir))
            {
                //If there is no section in this direction, place a block to fill the gap
                Transform block = Instantiate(LevelGenPrefabHolder.instance.GetBlockPrefab(), transform.position + (dir * (5f * transform.localScale.y)), Quaternion.Euler(DetermineRotation(dir * -1)));
            }
            else if(!LevelGenerationManager.instance.DoRoomsConnect(gridPosition, gridPosition + dir))
            {
                //If there is a section in this direction but they don't connect, place a block to fill the gap
                Transform block = Instantiate(LevelGenPrefabHolder.instance.GetBlockPrefab(), transform.position + (dir * (5f * transform.localScale.y)), Quaternion.Euler(DetermineRotation(dir * -1)));
            }
        }

        isFinalized = true;
    }

    public void SetDirection(Vector3 dir)
    {
        orientation = dir;
        possibleDirections = Directions.LocalizePossibleDirections(orientation, possibleDirections);
    }   

    public SectionType GetSectionType()
    {
        return sectionType;
    }

    public Vector3 GetGridPosition()
    {
        return gridPosition;
    }
    public void SetGridPosition(Vector3 pos)
    {
        gridPosition = pos;
    }
    public List<Vector3> GetPossibleDirections()
    {
        return possibleDirections;
    }
    public bool IsFinalized()
    {
        return isFinalized;
    }
}

public enum SectionType
{
    Basic,
    Hallway
}