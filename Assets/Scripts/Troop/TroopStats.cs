using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Battle;

namespace Troop {
    public enum TroopType { Knight, Axe, Archer }

    public class TroopStats : MonoBehaviour
    {
        [SerializeField] protected TMP_Text _initiativeText;
        [SerializeField] protected Image _healthBar;
        protected bool _isDead;

        [SerializeField] private float _maxHealth;
        private float _health;
        public float Health {
            get {
                return _health;
            }
            set {
                _health = value;
                _healthBar.fillAmount = Health/_maxHealth;
            }
        }

        private int _initiative;
        public int Initiative {
            get {
                return _initiative;
            }
            set {
                _initiative = value;
                _initiativeText.text = _initiative.ToString();
            }
        }

        [SerializeField] private float _movementRadius;
        public float MovementRadius => _movementRadius;

        [SerializeField] private float _attackRadius;
        public float AttackRadius => _attackRadius;

        [SerializeField] private float _attackDamage;
        public float AttackDamage => _attackDamage;

        private void Awake() {
            Health = _maxHealth;
        }

        public void TakeDamage(float amount) {
            Health -= amount;
            if(Health<=0f) {
                Health = 0f;
                if(!_isDead) {
                    _isDead = true;
                    Die();
                }
            }
        }

        public void Die() { //could be done with event but more expensive
            FindObjectOfType<BattleContoller>().TroopDied(this);
        }
    }
}