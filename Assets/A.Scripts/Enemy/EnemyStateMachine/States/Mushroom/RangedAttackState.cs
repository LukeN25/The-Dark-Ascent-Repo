using UnityEngine;
using UnityHFSM;
using System;

namespace EnemyAI.UnityHFSM
{
    public class RangedAttackState : EnemyStateBase
    {
        public RangedAttackState(
            bool needsExitTime,
            Enemy Enemy,
            Action<State<EnemyState, StateEvent>> onEnter,
            float ExitTime = 2f) : base(needsExitTime, Enemy, ExitTime, onEnter) { }

        public override void OnEnter()
        {
            Agent.isStopped = true;
            base.OnEnter();
            Animator.Play(stateName: "RangedAttack");
        }
    }
}
