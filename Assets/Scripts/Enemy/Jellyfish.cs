using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveGame
{
    public class Jellyfish : Enemy, IDamageable
    {
        public EnemySO Enemy;

        private float _currentHealth;

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

        public void Die()
        {
            foreach (var drop in Enemy.Drops)
            {
                Instantiate(drop.Prefab, new Vector2(transform.position.x + Random.Range(-1f, 1f), transform.position.y + Random.Range(-1f, 1f)), Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
