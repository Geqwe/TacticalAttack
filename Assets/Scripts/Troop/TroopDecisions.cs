using UnityEngine;
using Battle;
using UnityEngine.AI;

namespace Troop {
    
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class TroopDecisions : MonoBehaviour
    {
        [SerializeField] private GameObject _movementRangeCircle;
        [SerializeField] private GameObject _attackRangeCircle;
        protected BattleContoller _battleController;

        void Start() {
            _battleController = FindObjectOfType<BattleContoller>();
        }

        public virtual void StartTurn() {
            ShowMovementReach(true);
            ShowAttackReach(true);
            GenericAudioManager.Instance.PlaySfx("ChangeTurn");
        }

        public virtual void EndTurn() {
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
