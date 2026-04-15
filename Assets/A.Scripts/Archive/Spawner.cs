using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] Spawnees;
    private Collider Col;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("player has entered room");
            for (int i = 0; i < Spawnees.Length; i++)
            {
                Spawnees[i].SetActive(true);
                Col = (gameObject.GetComponent(typeof(Collider)) as Collider);
                Col.enabled = false;
            }
        }
    }
}
