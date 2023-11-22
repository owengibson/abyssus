using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveGame
{
    public class JellyfishTrigger : MonoBehaviour
    {
        [SerializeField] private Jellyfish _jellyfish;

        private Animator _animator;
        private List<IDamageable> _damageablesInsideRange = new();

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void Explode()
        {
            foreach (var damageable in _damageablesInsideRange)
            {
                damageable.TakeDamage(_jellyfish.Enemy.Damage);
            }
            _jellyfish.Die(false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                _animator.SetTrigger("JellyfishTriggered");

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
                _damageablesInsideRange.Remove(collision.GetComponent<IDamageable>());
            }

            if (collision.TryGetComponent<IDamageable>(out var damageable))
            {
                _damageablesInsideRange.Remove(damageable);
            }
        }
    }
}
