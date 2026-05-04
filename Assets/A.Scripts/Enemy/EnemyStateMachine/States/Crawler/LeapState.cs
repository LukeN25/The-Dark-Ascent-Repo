using UnityEngine;
using UnityHFSM;
using System;
using System.Collections;

namespace EnemyAI.UnityHFSM
{
    public class LeapState : EnemyStateBase
    {
        public LeapState(
            bool needsExitTime,
            Enemy Enemy,
            Action<State<EnemyState, StateEvent>> onEnter,
            float ExitTime = 3.2f) : base(needsExitTime, Enemy, ExitTime, onEnter) { }

        public override void OnEnter()
        {
            Agent.isStopped = true;
            base.OnEnter();
            Animator.Play(stateName: "Leap");
        }

        public override void OnLogic()
        {
            Enemy.RunCoroutine(LeapDelay());
            base.OnLogic();
        }

        private IEnumerator LeapDelay()
        {
            yield return new WaitForSeconds(0.66f);
            //Agent.Move(offset: 1.5f * Agent.speed * Time.deltaTime * Agent.transform.forward);
            Agent.isStopped = false;
        }
    }
}
