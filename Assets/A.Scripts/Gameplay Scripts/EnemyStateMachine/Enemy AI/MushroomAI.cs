using UnityEngine;
using UnityEngine.AI;
using UnityHFSM;
using System;
using System.Collections;

namespace EnemyAI.UnityHFSM
{
    [RequireComponent(requiredComponent: typeof(Animator), typeof(NavMeshAgent))]

    public class MushroomAI : Enemy
    {
        [Header("References")]
        [SerializeField] protected PlayerManager player;
        [SerializeField] private Spore SporePrefab;

        [Header("Sensors")]
        [SerializeField] protected PlayerSensor ChasePlayerSensor;
        [SerializeField] protected PlayerSensor ProjectileRangeSensor;

        [Header("Debug Info")]
        [SerializeField] protected bool IsInProjectileRange;
        [SerializeField] protected bool IsInChaseRange;
        [SerializeField] protected float LastAttackTime;

        [Header("Attack Config")]
        [SerializeField]
        [Range(0.1f, 5f)]
        protected float AttackCooldown = 2;

        private void Awake()
        {
            Agent = GetComponent<NavMeshAgent>();
            Animator = GetComponent<Animator>();
            EnemyFSM = new StateMachine<EnemyState, StateEvent>();

            // States
            EnemyFSM.AddState(name: EnemyState.Idle, new IdleState(needsExitTime: false, Enemy: this));
            EnemyFSM.AddState(name: EnemyState.Patrol, new PatrolState(needsExitTime: true, Enemy: this, player.transform));
            EnemyFSM.AddState(name: EnemyState.Chase, new ChaseState(needsExitTime: true, Enemy: this, player.transform));
            EnemyFSM.AddState(name: EnemyState.Spore, new SporeState(needsExitTime: true, Enemy: this, SporePrefab, OnAttack));

            // Transitions

            // Idle to Chase
            EnemyFSM.AddTriggerTransition(trigger: StateEvent.PlayerDetected, transition: new Transition<EnemyState>(EnemyState.Idle, EnemyState.Chase));
            EnemyFSM.AddTriggerTransition(trigger: StateEvent.PlayerLost, transition: new Transition<EnemyState>(EnemyState.Chase, EnemyState.Idle));
            EnemyFSM.AddTransition(new Transition<EnemyState>(from: EnemyState.Idle, to: EnemyState.Chase,
                condition: (transition) => IsInChaseRange
                    && Vector3.Distance(a: player.transform.position, b: transform.position) > Agent.stoppingDistance)
            );
            EnemyFSM.AddTransition(new Transition<EnemyState>(from: EnemyState.Chase, to: EnemyState.Idle,
                condition: (transition) => !IsInChaseRange
                    || Vector3.Distance(a: player.transform.position, b: transform.position) <= Agent.stoppingDistance)
            );

            //Spore Throw
            EnemyFSM.AddTransition(new Transition<EnemyState>(EnemyState.Chase, EnemyState.Spore, ShouldShoot, forceInstantly: true));
            EnemyFSM.AddTransition(new Transition<EnemyState>(EnemyState.Idle, EnemyState.Spore, ShouldShoot, forceInstantly: true));
            EnemyFSM.AddTransition(new Transition<EnemyState>(EnemyState.Spore, EnemyState.Chase, IsNotWithinIdleRange));
            EnemyFSM.AddTransition(new Transition<EnemyState>(EnemyState.Spore, EnemyState.Idle, IsWithinIdleRange));

            EnemyFSM.SetStartState(name: EnemyState.Idle);

            EnemyFSM.Init();
        }

        private void Start()
        {
            ChasePlayerSensor.OnPlayerEnter += ChasePlayerSensor_OnPlayerEnter;
            ChasePlayerSensor.OnPlayerExit += ChasePlayerSensor_OnPlayerExit;
            ProjectileRangeSensor.OnPlayerEnter += ProjectileRangeSensor_OnPlayerEnter;
            ProjectileRangeSensor.OnPlayerExit += ProjectileRangeSensor_OnPlayerExit;
        }

        private bool ShouldShoot(Transition<EnemyState> Transition) =>
            LastAttackTime + AttackCooldown <= Time.time
                   && IsInProjectileRange;

        private void ChasePlayerSensor_OnPlayerExit(Vector3 LastKnownPosition)
        {
            EnemyFSM.Trigger(StateEvent.PlayerLost);
            IsInChaseRange = false;
        }

        private void ChasePlayerSensor_OnPlayerEnter(Transform Player)
        {
            EnemyFSM.Trigger(StateEvent.PlayerDetected);
            IsInChaseRange = true;
        }

        private bool IsWithinIdleRange(Transition<EnemyState> Transition) =>
            Agent.remainingDistance <= Agent.stoppingDistance;

        private bool IsNotWithinIdleRange(Transition<EnemyState> Transition) =>
            !IsWithinIdleRange(Transition);

        private void OnAttack(State<EnemyState, StateEvent> state)
        {
            transform.LookAt(player.transform.position);
            LastAttackTime = Time.time;
        }

        private void ProjectileRangeSensor_OnPlayerExit(Vector3 LastKnownPosition) => IsInProjectileRange = false;

        private void ProjectileRangeSensor_OnPlayerEnter(Transform Player) => IsInProjectileRange = true;

        private void Update()
        {
            EnemyFSM.OnLogic();
        }
    }
}