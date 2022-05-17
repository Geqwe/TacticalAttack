using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Troop {
    public class TroopSelection : MonoBehaviour
    {
        [SerializeField] private TMP_Text _enemyKnightAmountText, _enemyAxeAmountText, _enemyArcherAmountText;
        [SerializeField] private TMP_Text _playerKnightAmountText, _playerAxeAmountText, _playerArcherAmountText;
        private Dictionary<TroopType,int> _playerSquad, _enemySquad;
        private int _maxPlayerTroopAmount = 4;
        private int _currentPlayerTroopAmount;
        [SerializeField] private Button _toBattleButton;

        void Start()
        {
            InitPlayerSquad();
            GetEnemyTroops();
            ShowEnemyTroops();
        }

        private void InitPlayerSquad() {
            _playerSquad = new Dictionary<TroopType, int>();
            _playerSquad.Add(TroopType.Knight, 0);
            _playerSquad.Add(TroopType.Axe, 0);
            _playerSquad.Add(TroopType.Archer, 0);
        }

        private void GetEnemyTroops() {
            _enemySquad = new Dictionary<TroopType, int>();
            _enemySquad.Add(TroopType.Knight, Random.Range(2,4));
            _enemySquad.Add(TroopType.Axe, Random.Range(1,4));
            _enemySquad.Add(TroopType.Archer, Random.Range(1,3));
        }

        private void ShowEnemyTroops() {
            _enemyKnightAmountText.text = _enemySquad[TroopType.Knight].ToString();
            _enemyAxeAmountText.text = _enemySquad[TroopType.Axe].ToString();
            _enemyArcherAmountText.text = _enemySquad[TroopType.Archer].ToString();
        }

        public void AddTroop(int troopIndex) {
            if(_currentPlayerTroopAmount == _maxPlayerTroopAmount) {
                return;
            }
            else if(++_currentPlayerTroopAmount == _maxPlayerTroopAmount) {
                _toBattleButton.interactable = true;
            }

            if(troopIndex == (int)TroopType.Knight) {
                ++_playerSquad[TroopType.Knight];
                _playerKnightAmountText.text = _playerSquad[TroopType.Knight].ToString();
            }
            else if(troopIndex == (int)TroopType.Axe) {
                ++_playerSquad[TroopType.Axe];
                _playerAxeAmountText.text = _playerSquad[TroopType.Axe].ToString();
            }
            else {
                ++_playerSquad[TroopType.Archer];
                _playerArcherAmountText.text = _playerSquad[TroopType.Archer].ToString();
            }
        }

        public void RemoveTroop(int troopIndex) {

            if(troopIndex == (int)TroopType.Knight) {
                if(_playerSquad[TroopType.Knight]>0) {
                    --_playerSquad[TroopType.Knight];
                    _playerKnightAmountText.text = _playerSquad[TroopType.Knight].ToString();
                    --_currentPlayerTroopAmount;
                    _toBattleButton.interactable = false;
                }
            }
            else if(troopIndex == (int)TroopType.Axe) {
                if(_playerSquad[TroopType.Axe]>0) {
                    --_playerSquad[TroopType.Axe];
                    _playerAxeAmountText.text = _playerSquad[TroopType.Axe].ToString();
                    --_currentPlayerTroopAmount;
                    _toBattleButton.interactable = false;
                }
            }
            else {
                if(_playerSquad[TroopType.Archer]>0) {
                    --_playerSquad[TroopType.Archer];
                    _playerArcherAmountText.text = _playerSquad[TroopType.Archer].ToString();
                    --_currentPlayerTroopAmount;
                    _toBattleButton.interactable = false;
                }
            }
        }

        public void ToBattle() {
            FindObjectOfType<TroopPlacement>().PlaceTroopsOnField(_playerSquad, _enemySquad);
        }
    }
}