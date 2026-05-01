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
        [SerializeField] protected GameObject player;
        [SerializeField] private Spore SporePrefab;

        [Header("Sensors")]
        [SerializeField] protected PlayerSensor MeleeRangeSensor;
        [SerializeField] protected PlayerSensor ProjectileRangeSensor;

        [Header("Debug Info")]
        [SerializeField] protected bool IsInProjectileRange;
        [SerializeField] protected bool IsInMeleeRange;
        [SerializeField] protected bool IsInChaseRange;
        [SerializeField] private bool CanThrow;
        [SerializeField] protected float LastAttackTime;

        [Header("Attack Config")]
        [SerializeField]
        [Range(0.1f, 10f)]
        protected float AttackCooldown = 8;
        [SerializeField]
        [Range(1, 20f)]
        protected float ThrowCooldown = 2;

        private void Awake()
        {
            Agent = GetComponent<NavMeshAgent>();
            Animator = GetComponent<Animator>();
            EnemyFSM = new StateMachine<EnemyState, StateEvent>();
            player = GameObject.FindWithTag("Player");

            // States
            EnemyFSM.AddState(name: EnemyState.Idle, new IdleState(needsExitTime: false, Enemy: this));
            EnemyFSM.AddState(name: EnemyState.Patrol, new PatrolState(needsExitTime: true, Enemy: this, player.transform));
            EnemyFSM.AddState(name: EnemyState.RangedAttack, new RangedAttackState(needsExitTime: true, Enemy: this, OnAttack));
            EnemyFSM.AddState(name: EnemyState.Chase, new ChaseState(needsExitTime: true, Enemy: this, player.transform));
            EnemyFSM.AddState(name: EnemyState.Spore, new SporeState(needsExitTime: true, Enemy: this, SporePrefab, OnAttack));
            EnemyFSM.AddState(name: EnemyState.Gather, new GatherState(needsExitTime: true, Enemy: this, OnGather));
            EnemyFSM.AddState(name: EnemyState.Stun, new StunState(needsExitTime: true, Enemy: this, ExitTime: 2f));

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

            //Spore Gather

            EnemyFSM.AddTransition(new Transition<EnemyState>(EnemyState.Chase, EnemyState.Gather, ShouldGather, forceInstantly: true));
            EnemyFSM.AddTransition(new Transition<EnemyState>(EnemyState.Idle, EnemyState.Gather, ShouldGather, forceInstantly: true));
            EnemyFSM.AddTransition(new Transition<EnemyState>(EnemyState.Gather, EnemyState.Chase, IsNotWithinIdleRange));
            EnemyFSM.AddTransition(new Transition<EnemyState>(EnemyState.Gather, EnemyState.Idle, IsWithinIdleRange));
            EnemyFSM.AddTransition(new Transition<EnemyState>(EnemyState.Gather, EnemyState.Spore, ShouldShoot));

            //Spore Throw
            EnemyFSM.AddTransition(new Transition<EnemyState>(EnemyState.Chase, EnemyState.Spore, ShouldShoot, forceInstantly: true));
            EnemyFSM.AddTransition(new Transition<EnemyState>(EnemyState.Idle, EnemyState.Spore, ShouldShoot, forceInstantly: true));
            EnemyFSM.AddTransition(new Transition<EnemyState>(EnemyState.Spore, EnemyState.Chase, IsNotWithinIdleRange));
            EnemyFSM.AddTransition(new Transition<EnemyState>(EnemyState.Spore, EnemyState.Idle, IsWithinIdleRange));

            // Stun (parry)
            EnemyFSM.AddTriggerTransitionFromAny(StateEvent.Parried, new Transition<EnemyState>(EnemyState.Stun, EnemyState.Stun, forceInstantly: true));
            EnemyFSM.AddTransition(new TransitionAfter<EnemyState>(EnemyState.Stun, EnemyState.Idle, 2f));

            //Ranged Attack
            EnemyFSM.AddTransition(new Transition<EnemyState>(EnemyState.Chase, EnemyState.RangedAttack, ShouldAttack, forceInstantly: true));
            EnemyFSM.AddTransition(new Transition<EnemyState>(EnemyState.Idle, EnemyState.RangedAttack, ShouldAttack, forceInstantly: true));
            EnemyFSM.AddTransition(new Transition<EnemyState>(EnemyState.RangedAttack, EnemyState.Chase, IsNotWithinIdleRange));
            EnemyFSM.AddTransition(new Transition<EnemyState>(EnemyState.RangedAttack, EnemyState.Idle, IsWithinIdleRange));
            EnemyFSM.AddTransition(new Transition<EnemyState>(EnemyState.RangedAttack, EnemyState.Gather, ShouldGather));

            EnemyFSM.SetStartState(name: EnemyState.Idle);

            EnemyFSM.Init();
        }

        private void Start()
        {
            MeleeRangeSensor.OnPlayerEnter += MeleeRangeSensor_OnPlayerEnter;
            MeleeRangeSensor.OnPlayerExit += MeleeRangeSensor_OnPlayerExit;
            ProjectileRangeSensor.OnPlayerEnter += ProjectileRangeSensor_OnPlayerEnter;
            ProjectileRangeSensor.OnPlayerExit += ProjectileRangeSensor_OnPlayerExit;
        }

        private bool ShouldGather(Transition<EnemyState> Transition) =>
            !CanThrow;

        private bool ShouldShoot(Transition<EnemyState> Transition) =>
            LastAttackTime + ThrowCooldown <= Time.time
                   && IsInProjectileRange && CanThrow;

        private bool ShouldAttack(Transition<EnemyState> Transition) =>
            LastAttackTime + AttackCooldown <= Time.time
                   && IsInMeleeRange;

        /*private void ChasePlayerSensor_OnPlayerExit(Vector3 LastKnownPosition)
        {
            EnemyFSM.Trigger(StateEvent.PlayerLost);
            IsInChaseRange = false;
        }

        private void ChasePlayerSensor_OnPlayerEnter(Transform Player)
        {
            EnemyFSM.Trigger(StateEvent.PlayerDetected);
            IsInChaseRange = true;
        }*/

        private bool IsWithinIdleRange(Transition<EnemyState> Transition) =>
            Agent.remainingDistance <= Agent.stoppingDistance;

        private bool IsNotWithinIdleRange(Transition<EnemyState> Transition) =>
            !IsWithinIdleRange(Transition);

        private void OnGather(State<EnemyState, StateEvent> state)
        {
            CanThrow = true;
        }

        private void OnAttack(State<EnemyState, StateEvent> state)
        {
            transform.LookAt(player.transform.position);
            LastAttackTime = Time.time;
            CanThrow = false;
        }

        private void MeleeRangeSensor_OnPlayerExit(Vector3 LastKnownPosition) => IsInMeleeRange = false;

        private void MeleeRangeSensor_OnPlayerEnter(Transform Player) => IsInMeleeRange = true;

        private void ProjectileRangeSensor_OnPlayerExit(Vector3 LastKnownPosition) => IsInProjectileRange = false;

        private void ProjectileRangeSensor_OnPlayerEnter(Transform Player) => IsInProjectileRange = true;

        private void Update()
        {
            EnemyFSM.OnLogic();
        }
    }
}