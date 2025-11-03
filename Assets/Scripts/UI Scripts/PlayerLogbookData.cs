using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerLogbookData
{
    public List<string> unlockedEnemies = new List<string>();
    public Dictionary<string, List<string>> unlockedMutations = new Dictionary<string, List<string>>();

    public bool IsEnemyUnlocked(string enemyName) => unlockedEnemies.Contains(enemyName);

    public bool IsMutationUnlocked(string enemyName, string mutationName)
    {
        return unlockedMutations.ContainsKey(enemyName) && unlockedMutations[enemyName].Contains(mutationName);
    }

    public void UnlockEnemy(string enemyName)
    {
        if (!unlockedEnemies.Contains(enemyName))
            unlockedEnemies.Add(enemyName);
    }

    public void UnlockMutation(string enemyName, string mutationName)
    {
        if (!unlockedMutations.ContainsKey(enemyName))
            unlockedMutations[enemyName] = new List<string>();
        if (!unlockedMutations[enemyName].Contains(mutationName))
            unlockedMutations[enemyName].Add(mutationName);
    }
}
