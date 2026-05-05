using UnityEngine;
using UnityEngine.AI;
using UnityHFSM;
using System;
using System.Collections;

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

        public bool IsKnockedBack { get; set; }

        private void Update()
        {
            if (!IsKnockedBack)
                EnemyFSM.OnLogic();
        }

        public Coroutine RunCoroutine(IEnumerator routine)
        {
            return StartCoroutine(routine);
        }
    }
}


