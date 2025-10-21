using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
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

    [SerializeField] Transform sectionPrefab;
    [SerializeField] int numberOfSections = 10;
    [SerializeField] LevelSection startSection;

    List<LevelSection> placedSections = new List<LevelSection>();

    private void Start()
    {
        startSection.SpawnNewSections(sectionPrefab, 1000);

        int index = 0;
        while (numberOfSections > 0)
        {
            Debug.Log(index);
            placedSections[index].SpawnNewSections(sectionPrefab, index);
            index++;
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
}
