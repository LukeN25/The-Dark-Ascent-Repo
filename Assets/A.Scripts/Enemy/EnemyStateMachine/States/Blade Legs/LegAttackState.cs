using UnityEngine;
using UnityHFSM;
using System;

namespace EnemyAI.UnityHFSM
{
    public class LegAttackState : EnemyStateBase
    {
        public LegAttackState(
            bool needsExitTime,
            Enemy Enemy,
            Action<State<EnemyState, StateEvent>> onEnter,
            float ExitTime = 0.8f) : base(needsExitTime, Enemy, ExitTime, onEnter) { }

        public override void OnEnter()
        {
            Agent.isStopped = true;
            base.OnEnter();
            Animator.Play(stateName: "Attack");
        }
    }
}
