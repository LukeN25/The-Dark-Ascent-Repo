using UnityEngine;
using UnityEngine.AI;
using UnityHFSM;

namespace EnemyAI.UnityHFSM
{
    [RequireComponent(requiredComponent: typeof(Animator), typeof(NavMeshAgent))]

    public class ButcherAI : Enemy
    {
        [Header("References")]
        [SerializeField] protected GameObject player;

        [Header("Sensors")]
        [SerializeField] protected PlayerSensor ChasePlayerSensor;
        [SerializeField] protected PlayerSensor MeleeRangeSensor;
        [SerializeField] protected PlayerSensor DashRangeSensor;

        [Header("Debug Info")]
        [SerializeField] protected bool IsInMeleeRange;
        [SerializeField] protected bool IsInDashRange;
        [SerializeField] protected bool IsInChaseRange;
        [SerializeField] protected float LastAttackTime;
        [SerializeField] protected float LastDashTime;

        [Header("Attack Config")]
        [SerializeField]
        [Range(0.1f, 5f)]
        protected float AttackCooldown = 2;
        [SerializeField]
        [Range(1, 20f)]
        protected float DashCooldown = 21;

        private void Awake()
        {
            Agent = GetComponent<NavMeshAgent>();
            Animator = GetComponent<Animator>();
            EnemyFSM = new StateMachine<EnemyState, StateEvent>();
            player = GameObject.FindWithTag("Player");

            // States
            EnemyFSM.AddState(name: EnemyState.Idle, new IdleState(needsExitTime: false, Enemy: this));
            EnemyFSM.AddState(name: EnemyState.Patrol, new PatrolState(needsExitTime: true, Enemy: this, player.transform));
            EnemyFSM.AddState(name: EnemyState.Chase, new ChaseState(needsExitTime: true, Enemy: this, player.transform));
            EnemyFSM.AddState(name: EnemyState.Attack, new AttackState(needsExitTime: true, Enemy: this, OnAttack));
            EnemyFSM.AddState(name: EnemyState.Dash, new DashState(needsExitTime: true, Enemy: this, OnDash));

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

            // Dash
            EnemyFSM.AddTransition(new Transition<EnemyState>(from: EnemyState.Chase, to: EnemyState.Dash, ShouldDash, forceInstantly: true));
            EnemyFSM.AddTransition(new Transition<EnemyState>(from: EnemyState.Idle, to: EnemyState.Dash, ShouldDash, forceInstantly: true));
            EnemyFSM.AddTransition(new Transition<EnemyState>(from: EnemyState.Dash, to: EnemyState.Chase, IsNotWithinIdleRange));
            EnemyFSM.AddTransition(new Transition<EnemyState>(from: EnemyState.Dash, to: EnemyState.Idle, IsWithinIdleRange));

            // Attack
            EnemyFSM.AddTransition(new Transition<EnemyState>(EnemyState.Chase, EnemyState.Attack, ShouldMelee, forceInstantly: true));
            EnemyFSM.AddTransition(new Transition<EnemyState>(EnemyState.Idle, EnemyState.Attack, ShouldMelee, forceInstantly: true));
            EnemyFSM.AddTransition(new Transition<EnemyState>(EnemyState.Attack, EnemyState.Chase, IsNotWithinIdleRange));
            EnemyFSM.AddTransition(new Transition<EnemyState>(EnemyState.Attack, EnemyState.Idle, IsWithinIdleRange));

            EnemyFSM.SetStartState(name: EnemyState.Idle);

            EnemyFSM.Init();
        }

        private void Start()
        {
            ChasePlayerSensor.OnPlayerEnter += ChasePlayerSensor_OnPlayerEnter;
            ChasePlayerSensor.OnPlayerExit += ChasePlayerSensor_OnPlayerExit;
            MeleeRangeSensor.OnPlayerEnter += MeleeRangeSensor_OnPlayerEnter;
            MeleeRangeSensor.OnPlayerExit += MeleeRangeSensor_OnPlayerExit;
            DashRangeSensor.OnPlayerEnter += DashRangeSensor_OnPlayerEnter;
            DashRangeSensor.OnPlayerExit += DashRangeSensor_OnPlayerExit;
        }

        private bool ShouldDash(Transition<EnemyState> Transition) =>
            LastDashTime + DashCooldown <= Time.time
                   && IsInDashRange;

        private bool ShouldMelee(Transition<EnemyState> Transition) =>
            LastAttackTime + AttackCooldown <= Time.time
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

        private void OnAttack(State<EnemyState, StateEvent> state) 
        {
            transform.LookAt(player.transform.position);
            LastAttackTime = Time.time;
        }

        private void OnDash(State<EnemyState, StateEvent> State)
        {
            //DashImpactSensor.gameObject.SetActive(true);
            transform.LookAt(player.transform.position);
            LastDashTime = Time.time;
        }
        
        private void MeleeRangeSensor_OnPlayerExit(Vector3 LastKnownPosition) => IsInMeleeRange = false;

        private void MeleeRangeSensor_OnPlayerEnter(Transform Player) => IsInMeleeRange = true;

        private void DashRangeSensor_OnPlayerExit(Vector3 LastKnownPosition) => IsInDashRange = false;

        private void DashRangeSensor_OnPlayerEnter(Transform Player) => IsInDashRange = true;

        private void Update()
        {
            EnemyFSM.OnLogic();
        }
    }
}