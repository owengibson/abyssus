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
                Die(true);
            }

            return _currentHealth;
        }

        public void Die(bool wasKilled)
        {
            if (!wasKilled)
            {
                foreach (var drop in Enemy.Drops)
                {
                    Instantiate(drop.Prefab, new Vector2(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f)), Quaternion.identity);
                }
            }
            
            Destroy(gameObject);
        }
    }
}
