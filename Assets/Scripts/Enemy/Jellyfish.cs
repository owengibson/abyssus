using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveGame
{
    public class Jellyfish : Enemy, IDamageable
    {
        public EnemySO Enemy;
        public Transform Target;

        [SerializeField] private Animator _attackAnimator;

        private float _currentHealth;
        private bool _hasBeenTriggered = false;
        private bool _isPlayerInRange = false;

        private List<IDamageable> _damageablesInsideRange = new();

        private void Start()
        {
            _currentHealth = Enemy.MaxHealth;
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
            foreach (var drop in Enemy.Drops)
            {
                Instantiate(drop.Prefab, new Vector2(transform.position.x + Random.Range(-1f, 1f), transform.position.y + Random.Range(-1f, 1f)), Quaternion.identity);
            }
            Destroy(gameObject);
        }

        public void Explode()
        {
            foreach(var damageable in _damageablesInsideRange)
            {
                damageable.TakeDamage(Enemy.Damage);
            }
            Die();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                _hasBeenTriggered = true;
                _isPlayerInRange = true;

                _attackAnimator.SetTrigger("JellyfishTriggered");

                _damageablesInsideRange.Add(collision.GetComponent<IDamageable>());
            }

            if (collision.TryGetComponent<IDamageable>(out var damageable))
            {
                _damageablesInsideRange.Add(damageable);
            }
            
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                _isPlayerInRange = false;
                _damageablesInsideRange.Remove(collision.GetComponent<IDamageable>());
            }

            if (collision.TryGetComponent<IDamageable>(out var damageable))
            {
                _damageablesInsideRange.Remove(damageable);
            }
        }
    }
}
