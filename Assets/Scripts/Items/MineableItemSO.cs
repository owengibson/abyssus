using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CaveGame
{
    [CreateAssetMenu(fileName = "New Mineable Item", menuName = "Inventory System/Items/Mineable")]
    public class MineableItemSO : ItemSO
    {
        public int MaxHealth;
        public int Rarity;
        public int ProximityLimit;
        public void Awake()
        {
            Type = ItemType.Mineable;
        }
    }
}
