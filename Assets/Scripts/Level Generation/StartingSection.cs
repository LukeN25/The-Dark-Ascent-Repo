using UnityEngine;

public class StartingSection : LevelSection
{
    public override void SpawnNewSections(Transform[] prefabs)
    {
        base.SpawnNewSections(prefabs);
        FinalizeSection();
    }

    public override void FinalizeSection()
    {
        LevelGenerationManager.instance.RegisterSectionInGrid(this);
        base.FinalizeSection();
    }
}
