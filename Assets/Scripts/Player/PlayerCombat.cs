using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveGame
{
    public class PlayerCombat : MonoBehaviour, IDamageable
    {
        [SerializeField] private PlayerStatsSO _stats;
        [SerializeField] private AudioSource _playerHitSound;

        private WaterPlayerController _playerController;

        private float _currentHealth;
        //private bool _isAttackOnCooldown = false;

        private void Awake()
        {
            _playerController = GetComponent<WaterPlayerController>();

            _currentHealth = _stats.MaxHealth;
        }

/*        private void Attack(Enemy target)
        {
            if (_playerController.CurrentMode == WaterPlayerController.PlayerMode.Terrain) return;
            if (Vector2.Distance(transform.position, target.transform.position) <= _stats.AttackRange && !_isAttackOnCooldown)
            {
                StartCoroutine(AttackCooldown(_stats.AttackCooldown));
                target.GetComponent<IDamageable>().TakeDamage(_stats.Damage);
            }
        }*/

        public float TakeDamage(float amount)
        {
            _currentHealth -= amount;
            if (_currentHealth <= 0)
            {
                Die();
            }
            _playerHitSound.Play(); 
            EventManager.OnPlayerTakeDamage?.Invoke(_currentHealth, _stats.MaxHealth);
            return _currentHealth;
        }

        private void Die()
        {
            EventManager.OnPlayerDeath?.Invoke();
        }

        private void DisablePlayer()
        {
            _playerController.enabled = false;
            enabled = false;
        }

/*        private IEnumerator AttackCooldown(float cooldown)
        {
            _isAttackOnCooldown = true;
            yield return new WaitForSeconds(cooldown);
            _isAttackOnCooldown = false;
        }*/

        private void OnEnable()
        {
            //EventManager.OnEnemyClicked += Attack;
            EventManager.OnPlayerDeath += DisablePlayer;
        }
        private void OnDisable()
        {
            //EventManager.OnEnemyClicked -= Attack;
            EventManager.OnPlayerDeath -= DisablePlayer;
        }
    }
}
