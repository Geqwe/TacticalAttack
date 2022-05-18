using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Troop.Enemy.StateMachine;

namespace Troop.Enemy
{
    public class EnemyDecisions : TroopDecisions
    {
        private IState _currentState;
        public IdleState IdleState = new IdleState();
        public MoveTowardsState MoveTowardsPlayerState = new MoveTowardsState();
        public AttackState AttackState = new AttackState();
        
        public NavMeshAgent Agent;
        public TroopStats TroopStats;
        public Transform EnemyTarget;
        private bool _myTurn;
        public bool ShouldEndTurn;

        private void Awake() {
            Agent = GetComponent<NavMeshAgent>();
            TroopStats = GetComponent<TroopStats>();

        }

        private void OnEnable()
        {
            _currentState = IdleState;
        }

        void Update()
        {
            if(!_myTurn)
                return;
            _currentState = _currentState.DoState(this);
        }

        public override void StartTurn()
        {
            StartCoroutine(WaitToStartTurn());
        }

        private IEnumerator WaitToStartTurn() {
            base.StartTurn();
            ShouldEndTurn = false;
            Agent.isStopped = false;
            Agent.SetDestination(transform.position);
            yield return new WaitForSeconds(1f);
            _myTurn = true;
        }

        public override void EndTurn()
        {
            _currentState = IdleState;
            _myTurn = false;
            EnemyTarget = null;
            base.EndTurn();
            _battleController.EndTurn();
        }

        public void FindWeakestEnemy() {
            PlayerTroopDecisions[] playerTroops = FindObjectsOfType<PlayerTroopDecisions>();
            
            Transform enemyToAttack = playerTroops[0].transform;
            float minDistance = Vector3.Distance(transform.position, enemyToAttack.position);
            float minHealth = enemyToAttack.GetComponent<TroopStats>().Health;

            foreach(PlayerTroopDecisions playerTroop in playerTroops) {
                if(Vector3.Distance(transform.position, playerTroop.transform.position) < minDistance) {
                    if(playerTroop.GetComponent<TroopStats>().Health < minHealth) {
                        enemyToAttack = playerTroop.transform;
                        minDistance = Vector3.Distance(transform.position, enemyToAttack.position);
                        minHealth = enemyToAttack.GetComponent<TroopStats>().Health;
                    }
                }
            }

            EnemyTarget = enemyToAttack;
        }

        public bool EnemyInAttackRange() {
            PlayerTroopDecisions[] playerTroops = FindObjectsOfType<PlayerTroopDecisions>();

            foreach(PlayerTroopDecisions playerTroop in playerTroops) {
                if(Vector3.Distance(transform.position, playerTroop.transform.position) <= TroopStats.AttackRadius) {
                    EnemyTarget = playerTroop.transform;
                    return true;
                }
            }

            return false;
        }

        public bool EnemyInMovementRange() { // can do with overlap sphere 
            PlayerTroopDecisions[] playerTroops = FindObjectsOfType<PlayerTroopDecisions>();

            foreach(PlayerTroopDecisions playerTroop in playerTroops) {
                if(Vector3.Distance(transform.position, playerTroop.transform.position) <= TroopStats.MovementRadius) {
                    EnemyTarget = playerTroop.transform;
                    return true;
                }
            }

            return false;
        }

        public void WaitForAttackAnimation() {
            _myTurn = false;
            StartCoroutine(WaitForAttackAnimationCor());
        }

        private IEnumerator WaitForAttackAnimationCor() {
            yield return new WaitForSeconds(2f);
            GenericAudioManager.Instance.PlaySfx("Slash");
            EndTurn();
        }
    }
}