using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveGame
{
    public class WeaponProjectile : MonoBehaviour
    {
        public Weapon ParentWeapon;
        private Rigidbody2D _rb;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
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
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            Debug.Log(_rb.velocity.magnitude);
        }
    }
}
