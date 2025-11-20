using UnityEngine;
using UnityHFSM;
using System;

namespace EnemyAI.UnityHFSM
{
    public class LeapState : EnemyStateBase
    {
        public LeapState(
            bool needsExitTime,
            Enemy Enemy,
            Action<State<EnemyState, StateEvent>> onEnter,
            float ExitTime = 4f) : base(needsExitTime, Enemy, ExitTime, onEnter) { }

        public override void OnEnter()
        {
            Agent.isStopped = true;
            base.OnEnter();
            Animator.Play(stateName: "Leap");
        }

        public override void OnLogic()
        {
            Agent.Move(offset: 1.5f * Agent.speed * Time.deltaTime * Agent.transform.forward);
            base.OnLogic();
        }
    }
}
