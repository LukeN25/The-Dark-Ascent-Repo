using UnityEngine;
using UnityHFSM;
using System;

namespace EnemyAI.UnityHFSM
{
    public class GatherState : EnemyStateBase
    {
        public GatherState(
            bool needsExitTime,
            Enemy Enemy,
            Action<State<EnemyState, StateEvent>> onEnter,
            float ExitTime = 2f) : base(needsExitTime, Enemy, ExitTime, onEnter) { }

        public override void OnEnter()
        {
            base.OnEnter();
            Agent.isStopped = true;
            Animator.Play(stateName: "Gather");
        }
    }
}
