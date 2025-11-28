using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] Spawnees;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            for (int i = 0; i < Spawnees.Length; i++)
            {
                Spawnees[i].SetActive(true);
            }
        }
    }
}
