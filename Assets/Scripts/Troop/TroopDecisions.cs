using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle;
using UnityEngine.AI;

namespace Troop {
    
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class TroopDecisions : MonoBehaviour
    {
        // [SerializeField] private GameObject _selectionCircle;
        [SerializeField] private GameObject _movementRangeCircle;
        [SerializeField] private GameObject _attackRangeCircle;
        protected BattleContoller _battleController;

        void Start() {
            _battleController = FindObjectOfType<BattleContoller>();
        }

        public virtual void StartTurn() {
            // _selectionCircle.SetActive(true);
            ShowMovementReach(true);
            ShowAttackReach(true);
        }

        public virtual void EndTurn() {
            // _selectionCircle.SetActive(false);
            InGameNotificationManager.Instance.UpdateText("");
            ShowMovementReach(false);
            ShowAttackReach(false);
        }

        private void ShowMovementReach(bool show) {
            if(show) {
                _movementRangeCircle.SetActive(true);
            }
            else {
                _movementRangeCircle.SetActive(false);
            }
        }

        private void ShowAttackReach(bool show) {
            if(show) {
                _attackRangeCircle.SetActive(true);
            }
            else {
                _attackRangeCircle.SetActive(false);
            }
        }


    }
}
