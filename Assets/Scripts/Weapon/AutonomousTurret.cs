using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CaveGame
{
    public class AutonomousTurret : Weapon
    {
        public ShopItemSO Item;

        [SerializeField] private List<GameObject> _enemiesInRange = new();

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Enemy")) return;

            _enemiesInRange.Add(collision.gameObject);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.CompareTag("Enemy")) return;

            _enemiesInRange.Remove(collision.gameObject);
        }

        private void Update()
        {
            if (_enemiesInRange.IsNullOrEmpty()) return;

            PrimaryFire(_enemiesInRange.First().transform.position);
            if (TargetHealth <= 0)
            {
                _enemiesInRange.Remove(_enemiesInRange.First());
            }
        }
    }
}
