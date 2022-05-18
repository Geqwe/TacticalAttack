using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using UnityEngine.UI;
using Battle;

namespace Troop {
    public enum TroopType { Knight, Axe, Archer }

    public class TroopStats : MonoBehaviour
    {
        [SerializeField] protected TMP_Text _initiativeText;
        [SerializeField] protected TMP_Text _damageText;
        [SerializeField] protected Image _healthBar;
        public bool IsDead;
        
        #region Properties
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
        [SerializeField] private float _extraEffectiveDamage;
        public float ExtraEffectiveDamage => _extraEffectiveDamage;

        [SerializeField] private TroopType _troopType;
        public TroopType TroopType => _troopType;
        [SerializeField] private TroopType _counterTroopType;
        #endregion

        public Animator Animator;
        private  NavMeshAgent _agent;

        private void Awake() {
            Health = _maxHealth;
            Animator = GetComponent<Animator>();
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update() {
            Animator.SetFloat("Velocity",_agent.velocity.magnitude);
        }

        public void TakeDamage(TroopStats attacker) {
            float amount = attacker.AttackDamage;

            if(attacker.TroopType == _counterTroopType) {
                amount += ExtraEffectiveDamage;
            }

            Health -= amount;

            LeanTween.scale(_healthBar.gameObject, Vector3.one*1.2f, 0.5f).setEasePunch();
            _damageText.text = "-"+amount;
            StartCoroutine(ShowDamageText());
            
            if(Health<=0f) {
                Health = 0f;
                if(!IsDead) {
                    IsDead = true;
                    Die();
                }
            }
        }

        private IEnumerator ShowDamageText() {
            _damageText.gameObject.SetActive(true);
            LeanTween.move(_damageText.gameObject, _damageText.transform.position+new Vector3(0,2f,0), 1.5f).setEaseLinear();
            yield return new WaitForSeconds(2f);
            _damageText.gameObject.SetActive(false);
            _damageText.transform.localPosition = new Vector3(0,0.714f,0);
        }

        public void Die() { //could be done with event but more expensive
            FindObjectOfType<BattleContoller>().TroopDied(this);
            Animator.Play("Die");
            GenericAudioManager.Instance.PlaySfx("Die");
            Destroy(gameObject,1.5f);
        }
    }
}