using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Troop.Enemy.StateMachine {
    public class IdleState : IState
    {
        public IState DoState(EnemyDecisions enemyDecisions) {

            //check for enemies in attack range -> attack
            if(enemyDecisions.EnemyInAttackRange()) {
                return enemyDecisions.AttackState;
            }

            //check for enemies in movement range -> move towards them
            if(enemyDecisions.EnemyInMovementRange()) {
                return enemyDecisions.MoveTowardsPlayerState;
            }

            enemyDecisions.FindWeakestEnemy();
            return enemyDecisions.MoveTowardsPlayerState;
            
        }

    }
}
