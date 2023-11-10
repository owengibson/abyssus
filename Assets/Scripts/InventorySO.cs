using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveGame
{
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
    public class InventorySO : ScriptableObject
    {
        public List<InventorySlot> Container = new();

        private void OnEnable() => hideFlags = HideFlags.DontUnloadUnusedAsset;

        public void AddItem(ItemSO item, int amount)
        {
            bool hasItem = false;
            foreach (var slot in Container)
            {
                if (slot.Item == item)
                {
                    slot.AddAmount(amount);
                    hasItem = true;
                    break;
                }
            }
            if (!hasItem)
            {
                Container.Add(new InventorySlot(item, amount));
            }
        }
    }
}
