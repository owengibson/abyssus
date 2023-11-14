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

        private bool _canDespawn = false;

        private void Awake()
        {
            Invoke("ActivateDespawnability", _despawnBufferTime);
        }

        private void ActivateDespawnability()
        {
            _canDespawn = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.gameObject.TryGetComponent<IDamageable>(out var enemy);

                ParentWeapon.DamageTarget(enemy);
                Destroy(gameObject);
            }
            else if (collision.gameObject.CompareTag("Ground"))
            {
                if (!_canDespawn) return;
                Destroy(gameObject);
            }
        }
    }
}
