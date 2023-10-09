using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveGame
{
    public class PlayerCombat : MonoBehaviour, IDamageable
    {
        [SerializeField] private PlayerStatsSO _stats;

        private WaterPlayerController _playerController;

        private float _currentHealth;

        private void Awake()
        {
            _playerController = GetComponent<WaterPlayerController>();

            _currentHealth = _stats.MaxHealth;
        }

        private void Attack(EnemyAI target)
        {
            if (_playerController.CurrentMode == WaterPlayerController.PlayerMode.Terrain) return;
            if (Vector2.Distance(transform.position, target.transform.position) <= _stats.AttackRange)
            {
                target.TakeDamage(_stats.Damage);
            }
        }

        public float TakeDamage(float amount)
        {
            _currentHealth -= amount;
            if (_currentHealth <= 0)
            {
                Die();
            }
            EventManager.OnPlayerTakeDamage?.Invoke(_currentHealth, _stats.MaxHealth);
            return _currentHealth;
        }

        private void Die()
        {

        }

        private void OnEnable()
        {
            EventManager.OnEnemyClicked += Attack;
        }
        private void OnDisable()
        {
            EventManager.OnEnemyClicked -= Attack;
        }
    }
}
