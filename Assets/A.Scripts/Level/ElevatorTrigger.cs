using UnityEngine;
using UnityEngine.SceneManagement;

public class ElevatorTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            SceneManager.LoadScene("End Cutscene");
        }
    }
}
