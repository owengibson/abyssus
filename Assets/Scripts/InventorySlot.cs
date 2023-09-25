using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveGame
{
    [System.Serializable]
    public class InventorySlot
    {
        public ItemSO Item;
        public int Amount;
        public InventorySlot(ItemSO item, int amount)
        {
            Item = item;
            Amount = amount;
        }

        public void AddAmount(int value)
        {
            Amount += value;
        }
    }
}
