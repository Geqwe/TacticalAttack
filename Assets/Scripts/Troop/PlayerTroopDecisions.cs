using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Troop
{
    public class PlayerTroopDecisions : TroopDecisions
    {
        private TroopStats _troopStats;
        public NavMeshAgent _agent;
        private bool _movementDone;
        private bool _attackDone;

        private void Awake() {
            _troopStats = GetComponent<TroopStats>();
            _agent = GetComponent<NavMeshAgent>();
        }

        public override void StartTurn()
        {
            base.StartTurn();
            _movementDone = false;
            _attackDone = false;
        }

        public void MoveTo(Vector3 target) {
            Debug.Log(_movementDone);
            if(_movementDone) {
                InGameNotificationManager.Instance.UpdateText("You don't have any more movement in this turn");
                return;
            }

            if(Vector3.Distance(transform.position, target) <= _troopStats.MovementRadius) {
                _movementDone = true;
                _agent.SetDestination(target);
                InGameNotificationManager.Instance.UpdateText("");
            }
            else {
                InGameNotificationManager.Instance.UpdateText("This position is unreachable");
            }
        }

        public void AttackEnemy(Transform enemy) {
            if(_attackDone) {
                InGameNotificationManager.Instance.UpdateText("You don't have any more attacks in this turn");
                return;
            }

            if(Vector3.Distance(transform.position, enemy.position) <= _troopStats.AttackRadius) {
                _attackDone = true;
                enemy.GetComponent<TroopStats>().TakeDamage(_troopStats.AttackDamage);
                InGameNotificationManager.Instance.UpdateText("");
            }
            else {
                InGameNotificationManager.Instance.UpdateText("This enemy is unreachable");
            }
        }

    }
}