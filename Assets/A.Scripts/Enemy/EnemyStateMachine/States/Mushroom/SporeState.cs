using UnityHFSM;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace EnemyAI.UnityHFSM
{
    public class SporeState : EnemyStateBase
    {
        private Spore Prefab;
        private ObjectPool<Spore> Pool; 

        public SporeState(
            bool needsExitTime,
            Enemy Enemy,
            Spore Prefab,
            Action<State<EnemyState, StateEvent>> onEnter,
            float ExitTime = 2f) : base(needsExitTime, Enemy, ExitTime, onEnter)
        {
            this.Prefab = Prefab;
            Pool = new(CreateObject, GetObject, ReleaseObject);
        }

        private Spore CreateObject()
        {
            var Instance = GameObject.Instantiate(Prefab);
            Instance.gameObject.SetActive(false);
            return Instance;
        }

        private void GetObject(Spore Instance)
        {
            Instance.transform.forward = Enemy.transform.forward;
            Instance.transform.position = Enemy.transform.position + Enemy.transform.forward + Vector3.up * 1.5f;
            Enemy.RunCoroutine(ThrowDelay(Instance));
        }

        private void ReleaseObject(Spore Instance)
        {
            Instance.gameObject.SetActive(false);
        }

        public override void OnEnter()
        {
            Agent.isStopped = true;
            base.OnEnter();
            Animator.Play("Spore");
            Pool.Get();
        }

        private IEnumerator ThrowDelay(Spore Instance)
        {
            yield return new WaitForSeconds(1f);
            Instance.gameObject.SetActive(true);
        }
    }
}
