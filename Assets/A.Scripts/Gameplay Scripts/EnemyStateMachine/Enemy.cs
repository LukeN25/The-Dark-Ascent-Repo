using UnityEngine;
using UnityEngine.AI;
using UnityHFSM;

namespace EnemyAI.UnityHFSM
{
    [RequireComponent(requiredComponent: typeof(Animator), typeof(NavMeshAgent))]

    public class Enemy : MonoBehaviour
    {
        protected StateMachine<EnemyState, StateEvent> EnemyFSM;
        protected Animator Animator;
        protected NavMeshAgent Agent;

        private void Awake()
        {
            Agent = GetComponent<NavMeshAgent>();
            Animator = GetComponent<Animator>();
            EnemyFSM = new StateMachine<EnemyState, StateEvent>();

            EnemyFSM.Init();
        }

        private void Start()
        {
            
        }

        private void Update()
        {
            EnemyFSM.OnLogic();
        }
    }
}


