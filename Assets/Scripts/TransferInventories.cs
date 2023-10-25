using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveGame
{
    public class TransferInventories : MonoBehaviour
    {
        [SerializeField] private InventorySO _fromInventory;
        [SerializeField] private InventorySO _toInventory;

        private void TransferItems()
        {
            if (_fromInventory.Container.IsNullOrEmpty()) return;

            foreach (var item in _fromInventory.Container)
            {
                _toInventory.AddItem(item.Item, item.Amount);
            }
            _fromInventory.Container.Clear();
        }

        private void Awake()
        {
            TransferItems();
        }
    }
}
