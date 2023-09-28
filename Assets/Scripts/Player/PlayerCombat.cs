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

        private void Attack(EnemyController target)
        {
            if (Vector2.Distance(transform.position, target.transform.position) > _stats.AttackRange && _playerController.CurrentMode == WaterPlayerController.PlayerMode.Terrain) return;

            target.TakeDamage(_stats.Damage);
        }

        public float TakeDamage(float amount)
        {
            _currentHealth -= amount;
            if (_currentHealth <= 0)
            {
                Die();
            }
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
