using UnityEngine;
using UnityEngine.AI;

namespace Troop.Enemy.StateMachine {
    public class MoveTowardsState : IState
    {
        private bool _movingTowardsPlayer, _movingTowardsInBetween;
        private float _distanceLeft; 
        private Vector3 _previousLocation;

        public IState DoState(EnemyDecisions enemyDecisions) {
            // Debug.Log("MOVE between "+_movingTowardsInBetween+" player "+_movingTowardsPlayer+" should end turn "+enemyDecisions.ShouldEndTurn);

            DoMoveTowardsPlayer(enemyDecisions);

            if(enemyDecisions.ShouldEndTurn) {
                _movingTowardsInBetween = false;
                _movingTowardsPlayer = false;
                enemyDecisions.EndTurn();
                return enemyDecisions.IdleState;
            }

            if(enemyDecisions.EnemyInAttackRange()) {
                _movingTowardsInBetween = false;
                _movingTowardsPlayer = false;
                enemyDecisions.Agent.isStopped = true;
                return enemyDecisions.AttackState;
            }
            else {
                return enemyDecisions.MoveTowardsPlayerState;
            }
        }

        private void DoMoveTowardsPlayer(EnemyDecisions enemyDecisions) {
            if(_movingTowardsPlayer) {
                if(Vector3.Distance(enemyDecisions.transform.position, enemyDecisions.EnemyTarget.position) <= enemyDecisions.TroopStats.AttackRadius) {
                    enemyDecisions.Agent.isStopped = true;
                    _movingTowardsPlayer = false;
                }
                return;
            }

            if(_movingTowardsInBetween) {
                _distanceLeft -= Vector3.Distance(enemyDecisions.transform.position, _previousLocation);
                _previousLocation = enemyDecisions.transform.position;
                if(_distanceLeft <= 0.1f) {
                    enemyDecisions.Agent.isStopped = true;
                    _movingTowardsInBetween = false;
                    enemyDecisions.ShouldEndTurn = true;
                }
                return;
            }
            
            if(Vector3.Distance(enemyDecisions.transform.position, enemyDecisions.EnemyTarget.position) <= enemyDecisions.TroopStats.MovementRadius) {
                _movingTowardsPlayer = true;
            }
            else{
                _previousLocation = enemyDecisions.transform.position;
                _distanceLeft = enemyDecisions.TroopStats.MovementRadius;
                _movingTowardsInBetween = true;
            }
            if(enemyDecisions.EnemyTarget!=null)
                enemyDecisions.Agent.SetDestination(enemyDecisions.EnemyTarget.position);
        }
    }
}