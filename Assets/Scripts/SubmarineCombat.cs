using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CaveGame
{
    public class SubmarineCombat : MonoBehaviour, IDamageable
    {
        [SerializeField] private PlayerStatsSO _stats;
        [SerializeField] private Slider _healthBar;
        [SerializeField] private SubmarinePlayerController _playerController;

        private float _currentHealth;
        private bool _isAttackOnCooldown = false;

        private void Awake()
        {
            _currentHealth = _stats.CurrentHealth;
            _healthBar.value = _currentHealth / _stats.MaxHealth;
        }

/*        private void Attack(WaveEnemyAI target)
        {
            if (_playerController.CurrentMode == SubmarinePlayerController.PlayerMode.Default) return;
            if (Vector2.Distance(transform.position, target.transform.position) <= _stats.AttackRange && !_isAttackOnCooldown)
            {
                StartCoroutine(AttackCooldown(_stats.AttackCooldown));
                target.TakeDamage(_stats.Damage);
            }
        }*/

        public float TakeDamage(float amount)
        {
            _currentHealth -= amount;
            if (_currentHealth <= 0)
            {
                Die();
            }
            else if (_currentHealth > _stats.MaxHealth)
            {
                _currentHealth = _stats.MaxHealth;
            }

            _healthBar.value = _currentHealth / _stats.MaxHealth;
            _stats.CurrentHealth = _currentHealth;

            return _currentHealth;
        }

        private void Die()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("EndScene");
        }

/*        private IEnumerator AttackCooldown(float cooldown)
        {
            _isAttackOnCooldown = true;
            yield return new WaitForSeconds(cooldown);
            _isAttackOnCooldown = false;
        }*/

        private void OnEnable()
        {
            //EventManager.OnWaveEnemyClicked += Attack;
            EventManager.OnSubmarineTakeDamage += TakeDamage;
        }
        private void OnDisable()
        {
            //EventManager.OnWaveEnemyClicked -= Attack;
            EventManager.OnSubmarineTakeDamage -= TakeDamage;
        }
    }
}
