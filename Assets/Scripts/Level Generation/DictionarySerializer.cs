using System;
using System.Collections.Generic;
using UnityEngine;

public class DictionarySerializer : MonoBehaviour
{
    [SerializeField]
    public DirDictionary dict;
}

[Serializable]
public class DirDictionaryItem
{
    [SerializeField]
    public Vector3 key;
    [SerializeField]
    public bool available;
}

[Serializable]
public class DirDictionary
{
    [SerializeField]
    public DirDictionaryItem[] items;

    public Dictionary<Vector3, bool> ToDictionary()
    {
        Dictionary<Vector3, bool> dict = new Dictionary<Vector3, bool>();
        foreach (var item in items )
        {
            dict.Add(item.key, item.available);
        }

        return dict;
    }
}
