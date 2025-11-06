using UnityEngine;
using UnityEngine.AI;
using UnityHFSM;

namespace EnemyAI.UnityHFSM
{
    [RequireComponent(requiredComponent:typeof(Animator), typeof(NavMeshAgent))]

    public class Enemy : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject Player; //change this to player script

        private StateMachine<EnemyState, StateEvent> EnemyFSM;
        private Animator Animator;
        private NavMeshAgent Agent;

        private void Awake()
        {
            Agent = GetComponent<NavMeshAgent>();
            Animator = GetComponent<Animator>();
            EnemyFSM = new StateMachine<EnemyState, StateEvent>();

            EnemyFSM.AddState(name:EnemyState.Idle, new IdleState(needsExitTime:false, Enemy:this));
            EnemyFSM.AddState(name:EnemyState.Patrol, new PatrolState(needsExitTime:true, Enemy:this, Player.transform));
            EnemyFSM.AddState(name:EnemyState.Chase, new ChaseState(needsExitTime:true, Enemy:this, Player.transform));
            EnemyFSM.AddState(name:EnemyState.Attack, new AttackState(needsExitTime:true, Enemy:this, OnAttack));
            EnemyFSM.AddState(name:EnemyState.Dash, new DashState(needsExitTime:true, Enemy:this, OnDash));

            EnemyFSM.SetStartState(name: EnemyState.Idle);

            EnemyFSM.Init();

            
        }

        private void OnAttack(State<EnemyState, StateEvent> state) { }

        private void OnDash(State<EnemyState, StateEvent> state) { }

        private void Update()
        {
            EnemyFSM.OnLogic();
        }
    }
}


