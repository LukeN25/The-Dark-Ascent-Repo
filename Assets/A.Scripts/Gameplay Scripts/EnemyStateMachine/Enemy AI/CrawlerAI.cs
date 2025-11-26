using UnityEngine;
using UnityEngine.AI;
using UnityHFSM;

namespace EnemyAI.UnityHFSM
{
    [RequireComponent(requiredComponent: typeof(Animator), typeof(NavMeshAgent))]

    public class CrawlerAI : Enemy
    {
        [Header("References")]
        [SerializeField] protected PlayerManager player;

        [Header("Sensors")]
        [SerializeField] protected PlayerSensor ChasePlayerSensor;
        [SerializeField] protected PlayerSensor MeleeRangeSensor;

        [Header("Debug Info")]
        [SerializeField] protected bool IsInMeleeRange;
        [SerializeField] protected bool IsInChaseRange;
        [SerializeField] protected float LastAttackTime;
        //[SerializeField] protected float LastDashTime;

        [Header("Attack Config")]
        [SerializeField]
        [Range(0.1f, 20f)]
        protected float LeapCooldown = 10;

        private void Awake()
        {
            Agent = GetComponent<NavMeshAgent>();
            Animator = GetComponent<Animator>();
            EnemyFSM = new StateMachine<EnemyState, StateEvent>();

            // States
            EnemyFSM.AddState(name: EnemyState.Idle, new IdleState(needsExitTime: false, Enemy: this));
            //EnemyFSM.AddState(name: EnemyState.Patrol, new PatrolState(needsExitTime: true, Enemy: this, player.transform));
            EnemyFSM.AddState(name: EnemyState.Chase, new ChaseState(needsExitTime: true, Enemy: this, player.transform));
            EnemyFSM.AddState(name: EnemyState.Leap, new LeapState(needsExitTime: true, Enemy: this, OnLeap));

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

            //Leap
            EnemyFSM.AddTransition(new Transition<EnemyState>(from: EnemyState.Chase, to: EnemyState.Leap, ShouldLeap, forceInstantly: true));
            EnemyFSM.AddTransition(new Transition<EnemyState>(from: EnemyState.Idle, to: EnemyState.Leap, ShouldLeap, forceInstantly: true));
            EnemyFSM.AddTransition(new Transition<EnemyState>(from: EnemyState.Leap, to: EnemyState.Chase, IsNotWithinIdleRange));
            EnemyFSM.AddTransition(new Transition<EnemyState>(from: EnemyState.Leap, to: EnemyState.Idle, IsWithinIdleRange));

            EnemyFSM.SetStartState(name: EnemyState.Idle);

            EnemyFSM.Init();
        }

        private void Start()
        {
            ChasePlayerSensor.OnPlayerEnter += ChasePlayerSensor_OnPlayerEnter;
            ChasePlayerSensor.OnPlayerExit += ChasePlayerSensor_OnPlayerExit;
            MeleeRangeSensor.OnPlayerEnter += MeleeRangeSensor_OnPlayerEnter;
            MeleeRangeSensor.OnPlayerExit += MeleeRangeSensor_OnPlayerExit;
        }

        private bool ShouldLeap(Transition<EnemyState> Transition) =>
            LastAttackTime + LeapCooldown <= Time.time
                   && IsInMeleeRange;

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

        private void OnLeap(State<EnemyState, StateEvent> state)
        {
            transform.LookAt(player.transform.position);
            LastAttackTime = Time.time;
        }

        private void MeleeRangeSensor_OnPlayerExit(Vector3 LastKnownPosition) => IsInMeleeRange = false;

        private void MeleeRangeSensor_OnPlayerEnter(Transform Player) => IsInMeleeRange = true;

        private void Update()
        {
            EnemyFSM.OnLogic();
        }
    }
}
