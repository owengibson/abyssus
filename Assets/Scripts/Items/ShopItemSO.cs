using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveGame
{
    [CreateAssetMenu(fileName = "New Shop Item", menuName = "Inventory System/Items/Shop")]
    public class ShopItemSO : ItemSO
    {
        public Dictionary<ItemSO, int> Cost;
        public bool IsPlaceable;

        private void Awake()
        {
            Type = ItemType.Shop;
        }
    }
}
