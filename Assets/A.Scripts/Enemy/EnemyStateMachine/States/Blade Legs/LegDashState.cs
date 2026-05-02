using UnityEngine;
using UnityHFSM;
using System;

namespace EnemyAI.UnityHFSM
{
    public class LegDashState : EnemyStateBase
    {
        public LegDashState(bool needsExitTime, Enemy Enemy, Action<State<EnemyState, StateEvent>> onEnter, float ExitTime = 6.5f) : base(needsExitTime, Enemy, ExitTime, onEnter) { }

        public override void OnEnter()
        {
            //Agent.isStopped = true;
            base.OnEnter();
            Animator.Play(stateName: "Dash");
        }

        public override void OnLogic()
        {
            Agent.Move(offset: 3.5f * Agent.speed * Time.deltaTime * Agent.transform.forward);
            base.OnLogic();
        }
    }
}
