using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Troop.Enemy.StateMachine {
    public class IdleState : IState
    {
        public IState DoState(EnemyDecisions enemyDecisions) {
            // Debug.Log("IDLE");
            if(enemyDecisions.EnemyInAttackRange()) {
                return enemyDecisions.AttackState;
            }

            if(enemyDecisions.EnemyInMovementRange()) {
                return enemyDecisions.MoveTowardsPlayerState;
            }

            enemyDecisions.FindWeakestEnemy();
            return enemyDecisions.MoveTowardsPlayerState;
            
        }

    }
}
