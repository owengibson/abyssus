using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CaveGame
{
    [CreateAssetMenu(fileName = "New Mineable Item", menuName = "Inventory System/Items/Mineable")]
    public class MineableItemSO : ItemSO, IDamageable
    {
        public int MaxHealth;

        private int _currentHealth;
        public void Awake()
        {
            Type = ItemType.Mineable;
            _currentHealth = MaxHealth;
        }

        public int TakeDamage(int amount)
        {
            _currentHealth -= amount;
            return _currentHealth;
        }
    }
}
