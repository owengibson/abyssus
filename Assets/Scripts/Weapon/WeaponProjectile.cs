using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveGame
{
    public class WeaponProjectile : MonoBehaviour
    {
        public Weapon ParentWeapon;

        [SerializeField] private float _despawnBufferTime = 0.15f;
        [SerializeField] private AudioSource _hitWallSfx;

        private bool _canDespawn = false;
        private bool _canDamage = true;
        private SpriteRenderer _spriteRenderer;
        private ParticleSystem _particleSystem;

        private void Awake()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _particleSystem = GetComponent<ParticleSystem>();

            Invoke("ActivateDespawnability", _despawnBufferTime);
        }

        private void ActivateDespawnability()
        {
            _canDespawn = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy") && _canDamage)
            {
                collision.gameObject.TryGetComponent<IDamageable>(out var enemy);

                ParentWeapon.DamageTarget(enemy);
                _canDamage = false;
                _spriteRenderer.enabled = false;
                _particleSystem.Stop();
                Destroy(gameObject, _particleSystem.main.duration);
            }
            else if (collision.gameObject.CompareTag("Ground"))
            {
                if (!_hitWallSfx.isPlaying)
                {
                    _hitWallSfx.Play();
                }
                if (!_canDespawn) return;
                _canDamage = false;
                _spriteRenderer.enabled = false;
                _particleSystem.Stop();
                Destroy(gameObject, _particleSystem.main.duration);
            }
        }
    }
}
