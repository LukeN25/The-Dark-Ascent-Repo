using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InfiniteEnemySpawner<T>
{
    [System.Serializable]
    public struct Pair
    {
        public T enemy;
        public float weight;

        public Pair(T enemy, float weight)
        {
            this.enemy = enemy;
            this.weight = weight;
        }
    }

    public List<Pair> list = new List<Pair>();

    public int Count
    {
        get => list.Count;
    }

    public void Add(T enemy, float weight)
    {
        list.Add(new Pair(enemy, weight));
    }

    public T GetRandom()
    {
        float totalWeight = 0;

        foreach (Pair p in list)
        {
            totalWeight += p.weight;
        }

        float value = Random.value * totalWeight;

        float sumWeight = 0;

        foreach (Pair p in list)
        {
            sumWeight += p.weight;

            if (sumWeight >= value)
            {
                return p.enemy;
            }
        }

        return default(T);
    }
}