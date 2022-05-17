using UnityEngine;

namespace Troop.Enemy.StateMachine {
    public class AttackState : IState
    {
        public IState DoState(EnemyDecisions enemyDecisions) {
            DoAttack(enemyDecisions);

            enemyDecisions.EndTurn();

            // Debug.Log("ATTACK -> MOVETOWARDSPLAYER");
            return enemyDecisions.IdleState;
        }

        public void DoAttack(EnemyDecisions enemyDecisions) {
            enemyDecisions.EnemyTarget.GetComponent<TroopStats>().TakeDamage(enemyDecisions.TroopStats.AttackDamage);
            // enemyAI.EnemyStats.Animator.Play("ZombieAttack");
        }
    }
}