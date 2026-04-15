using UnityEngine;
using UnityHFSM;
using System;

namespace EnemyAI.UnityHFSM
{
    public class IdleState : EnemyStateBase
    {
        public IdleState(bool needsExitTime, Enemy Enemy) : base(needsExitTime, Enemy) { }

        public override void OnEnter()
        {
            base.OnEnter();
            Agent.isStopped = true;
            Animator.Play(stateName: "Idle");
        }
    }
}
