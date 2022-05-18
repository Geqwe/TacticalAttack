namespace Troop.Enemy.StateMachine {
    public class AttackState : IState
    {
        public IState DoState(EnemyDecisions enemyDecisions) {
            // Debug.Log("ATTACK");

            DoAttack(enemyDecisions);

            return enemyDecisions.AttackState;
        }

        public void DoAttack(EnemyDecisions enemyDecisions) {
            enemyDecisions.EnemyTarget.GetComponent<TroopStats>().TakeDamage(enemyDecisions.TroopStats);
            enemyDecisions.transform.LookAt(enemyDecisions.EnemyTarget);
            enemyDecisions.TroopStats.Animator.Play("Attack");
            enemyDecisions.WaitForAttackAnimation();
        }
    }
}