using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveGame
{
    public class WeaponProjectile : MonoBehaviour
    {
        public Weapon ParentWeapon;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag("Enemy")) return;
            collision.gameObject.TryGetComponent<IDamageable>(out var enemy);

            ParentWeapon.DamageTarget(enemy);
            Destroy(gameObject);
        }
    }
}
