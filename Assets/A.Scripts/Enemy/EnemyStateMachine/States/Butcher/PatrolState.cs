using UnityEngine;

namespace EnemyAI.UnityHFSM
{
    public class PatrolState : EnemyStateBase
    {
        private Transform Target;

        public PatrolState(bool needsExitTime, Enemy Enemy, Transform Target) : base(needsExitTime, Enemy)
        {
            this.Target = Target;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Agent.enabled = true;
            Agent.isStopped = false;
            //Animator.Play("Walk");
        }

        public override void OnLogic()
        {
            base.OnLogic();
            if (!RequestedExit)
            {
                //Change this to target waypoints later
                Agent.SetDestination(Target.position);
            }
            else if (Agent.remainingDistance <= Agent.stoppingDistance)
            {
                fsm.StateCanExit();
            }
        }
    }
}