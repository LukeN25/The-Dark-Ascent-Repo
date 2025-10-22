using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerationManager : MonoBehaviour
{
    public static LevelGenerationManager instance;
    private void Awake()
    {
        instance = this;

        if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] Transform[] sectionPrefabs;
    [SerializeField] Transform bossRoomPrefab;
    [SerializeField] LevelSection startSection;

    [SerializeField] int numberOfSections = 10;

    List<LevelSection> placedSections = new List<LevelSection>();

    private void Start()
    {
        GenerateLevel();
    }

    private void GenerateLevel()
    {
        startSection.SpawnNewSections(sectionPrefabs);

        int index = 0;
        while (numberOfSections > 0)
        {
            placedSections[index].SpawnNewSections(sectionPrefabs);
            index++;
        }

        placedSections[index].SpawnSingleSection(bossRoomPrefab);

        foreach (LevelSection section in placedSections)
        {
            section.FinalizeSection();
        }
    }

    public void DecreaseNumberOfSections(int amount)
    {
        numberOfSections -= amount;
    }

    public int GetNumberOfSections()
    {
        return numberOfSections;
    }

    public void AddSection(LevelSection section)
    {
        placedSections.Add(section);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

    public Transform[] GetSectionPrefabs()
    {
        return sectionPrefabs;
    }

    public List<Transform> GetSectionPrefabsWithoutType(SectionType type)
    {
        List<Transform> filteredPrefabs = new List<Transform>();

        foreach (Transform prefab in sectionPrefabs)
        {
            LevelSection section = prefab.GetComponent<LevelSection>();
            if (section.GetSectionType() != type)
            {
                filteredPrefabs.Add(prefab);
            }
        }

        return filteredPrefabs;
    }
}
