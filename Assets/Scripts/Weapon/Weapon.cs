using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CaveGame
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private WeaponStatsSO _stats;
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private float _projectileSpeed;

        private bool _isAttackOnCooldown = false;

        public void PrimaryFire(Vector3 targetPosition)
        {
            if (_isAttackOnCooldown) return;

            Vector2 targetScreenPos = Camera.main.WorldToScreenPoint(targetPosition);
            Vector2 startingScreenPos = Camera.main.WorldToScreenPoint(transform.position);
            targetScreenPos.x -= startingScreenPos.x;
            targetScreenPos.y -= startingScreenPos.y;
            float angle = Mathf.Atan2(targetScreenPos.y, targetScreenPos.x) * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.Euler(new Vector3(0, 0, angle - 90));

            GameObject firedProjectile = Instantiate(_projectilePrefab, transform.position, rot);
            Vector3 direction = (targetPosition - transform.position).normalized;
            //firedProjectile.GetComponent<Rigidbody2D>().AddForce(direction * _projectileSpeed);
            firedProjectile.GetComponent<Rigidbody2D>().velocity = direction * _projectileSpeed;
            firedProjectile.GetComponent<WeaponProjectile>().ParentWeapon = this;

            StartCoroutine(AttackCooldown(_stats.AttackCooldown));
        }

        public void DamageTarget(IDamageable target)
        {
            target.TakeDamage(_stats.Damage);
        }

        private IEnumerator AttackCooldown(float cooldown)
        {
            _isAttackOnCooldown = true;
            yield return new WaitForSeconds(cooldown);
            _isAttackOnCooldown = false;
        }
    }
}
