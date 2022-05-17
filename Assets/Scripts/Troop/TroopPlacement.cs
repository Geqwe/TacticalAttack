using System.Collections.Generic;
using UnityEngine;

namespace Troop {
    public class TroopPlacement : MonoBehaviour
    {
        [SerializeField] private Transform[] _possiblePlayerPositions, _possibleEnemyPositions;
        [SerializeField] private GameObject[] _enemyPrefabs, _playerPrefabs;
        [SerializeField] private GameObject _pickYourTroopsScreen, _rollInitiativeButton;

        void Start()
        {
            _possiblePlayerPositions.ShuffleArray();
            _possibleEnemyPositions.ShuffleArray();
        }

        public void PlaceTroopsOnField(Dictionary<TroopType,int> playerSquad, Dictionary<TroopType,int> enemySquad) {
            int index = 0;
            foreach(KeyValuePair<TroopType,int> troop in enemySquad) {
                for(int i=0;i<troop.Value;++i)
                    Instantiate(_enemyPrefabs[(int)troop.Key], _possibleEnemyPositions[index++].position, Quaternion.identity, transform);
            }
            
            index = 0;
            foreach(KeyValuePair<TroopType,int> troop in playerSquad) {
                for(int i=0;i<troop.Value;++i)
                    Instantiate(_playerPrefabs[(int)troop.Key], _possiblePlayerPositions[index++].position, Quaternion.identity, transform);
            }

            _pickYourTroopsScreen.SetActive(false);
            _rollInitiativeButton.SetActive(true);
        }
    }
}
