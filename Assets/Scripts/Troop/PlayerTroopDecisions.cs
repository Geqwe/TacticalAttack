using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Troop
{
    public class PlayerTroopDecisions : TroopDecisions
    {
        private TroopStats _troopStats;
        private TroopCircles _troopCircles;
        public NavMeshAgent _agent;
        private bool _movementDone;
        private bool _attackDone;
        private Animator _animator;
        private float _distanceLeft;

        private void Awake() {
            _troopStats = GetComponent<TroopStats>();
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            _troopCircles = GetComponent<TroopCircles>();
        }

        public override void StartTurn()
        {
            base.StartTurn();
            _movementDone = false;
            _attackDone = false;
            _distanceLeft = _troopStats.MovementRadius;
            _troopCircles.CreateMovementCircleWithNewRadius(_troopStats.MovementRadius);
        }

        public void MoveTo(Vector3 target) {
            if(_movementDone) {
                if(_agent.velocity.magnitude == 0f) { //reached destination
                    _movementDone = false;
                }
                else {
                    InGameNotificationManager.Instance.UpdateText("Wait until the troop has reached its destination");
                    return;
                }
            }

            if(Vector3.Distance(transform.position, target) <= _distanceLeft) {
                _movementDone = true;
                _agent.SetDestination(target);
                InGameNotificationManager.Instance.UpdateText("");
                _distanceLeft -= Vector3.Distance(transform.position, target);
                _troopCircles.CreateMovementCircleWithNewRadius(_distanceLeft);
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
                _animator.Play("Attack");
                _attackDone = true;
                transform.LookAt(enemy);
                enemy.GetComponent<TroopStats>().TakeDamage(_troopStats);
                GenericAudioManager.Instance.PlaySfx("Slash");
                InGameNotificationManager.Instance.UpdateText("");
            }
            else {
                InGameNotificationManager.Instance.UpdateText("This enemy is unreachable");
            }
        }

    }
}