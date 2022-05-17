using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Troop;

namespace Battle {
    public class BattleContoller : MonoBehaviour
    {
        private List<TroopStats> _troopsPlaying;
        [SerializeField] private GameObject _rollInitiativeButton, _endTurnButton;
        private int _troopTurnIndex = -1;

        private int _numOfPlayerTroops, _numOfEnemyTroops;
        public bool PlayersTurn;

        void Start()
        {
            
        }

        public void RollInitiative() {
            _rollInitiativeButton.SetActive(false);

            _troopsPlaying = GameObject.FindObjectsOfType<TroopStats>().ToList();
            
            foreach(TroopStats troop in _troopsPlaying) { //could sort in here as well
                troop.Initiative = Random.Range(1,21);
                if(troop.gameObject.layer == 6) {
                    ++_numOfPlayerTroops;
                }
                else { //layer 7
                    ++_numOfEnemyTroops;
                }
            }

            _troopsPlaying = _troopsPlaying.OrderByDescending(t => t.Initiative).ToList();
            int index = 1;
            foreach(TroopStats troop in _troopsPlaying) { //could sort in here as well
                troop.Initiative = index++;
            }
            GiveNextTurn();
        }

        private void GiveNextTurn() {
            if(_troopTurnIndex == _troopsPlaying.Count-1) {
                _troopTurnIndex = 0;
            }
            else {
                ++_troopTurnIndex;
            }

            CheckIfEndTurnButtonIsNeeded();

            _troopsPlaying[_troopTurnIndex].GetComponent<TroopDecisions>().StartTurn();
        }

        public void EndTurn() {
            if(_troopsPlaying[_troopTurnIndex].gameObject.layer==6)
                _troopsPlaying[_troopTurnIndex].GetComponent<TroopDecisions>().EndTurn();
            GiveNextTurn();
        }

        private void CheckIfEndTurnButtonIsNeeded() {
            if(_troopsPlaying[_troopTurnIndex].gameObject.layer==6) {
                PlayerTurn();
            }
            else {
                AITurn();
            }
        }

        private void PlayerTurn() {
            _endTurnButton.SetActive(true);
            PlayersTurn = true;
        }

        private void AITurn() {
            PlayersTurn = false;
            _endTurnButton.SetActive(false);
        }

        public void TroopAttack(Transform target) {
            _troopsPlaying[_troopTurnIndex].GetComponent<PlayerTroopDecisions>().AttackEnemy(target);
        }

        public void TroopMove(Vector3 target) {
            _troopsPlaying[_troopTurnIndex].GetComponent<PlayerTroopDecisions>().MoveTo(target);
        }

        public void TroopDied(TroopStats troop) {
            if(troop.gameObject.layer==6) {
                if(--_numOfPlayerTroops==0) {
                    PlayerLost();
                }
            }
            else { //layer 7
                if(--_numOfEnemyTroops==0) {
                    PlayerWon();
                }
            }
        }

        private void PlayerLost() {

        }

        private void PlayerWon() {

        }
    }
}