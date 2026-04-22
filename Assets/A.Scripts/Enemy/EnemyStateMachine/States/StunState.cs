namespace EnemyAI.UnityHFSM
{
    public class StunState : EnemyStateBase
    {
        public StunState(bool needsExitTime, Enemy Enemy, float ExitTime = 2f)
            : base(needsExitTime, Enemy, ExitTime) { }

        public override void OnEnter()
        {
            base.OnEnter();
            Agent.isStopped = true;
            Animator.speed = 0f;
        }

        public override void OnExit()
        {
            Agent.isStopped = false;
            Animator.speed = 1f;
        }
    }
}
