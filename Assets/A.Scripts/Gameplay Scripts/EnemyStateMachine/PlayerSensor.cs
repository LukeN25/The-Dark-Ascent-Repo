using UnityEngine;

namespace EnemyAI.UnityHFSM
{
    [RequireComponent(typeof(SphereCollider))]
    public class PlayerSensor : MonoBehaviour
    {
        public delegate void PlayerEnterEvent(Transform player);

        public delegate void PlayerExitEvent(Vector3 lastKnownPosition);

        public event PlayerEnterEvent OnPlayerEnter;

        public event PlayerExitEvent OnPlayerExit;


        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Player")
            {
                OnPlayerEnter?.Invoke(other.GetComponent<Transform>());
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                OnPlayerExit?.Invoke(other.transform.position);
            }
        }
    }
}

