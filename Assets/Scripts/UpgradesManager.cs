using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace CaveGame
{
    public class UpgradesManager : MonoBehaviour
    {
        [SerializeField] private ShopItemSO _item;
        [SerializeField] private InventorySO _inventory;
        //[SerializeField] private GameObject _boughtScreen;
        //[SerializeField] private GameObject _insufficientItems;
        [SerializeField] private GameObject _craftingCanvas;

        public void BuyUpgrade()
        {
            int completeItemCosts = 0;
            foreach (ItemSO item in _item.Cost.Keys )
            {
                foreach(InventorySlot slot in _inventory.Container)
                {
                    if (slot.Item == item && slot.Amount >= _item.Cost[item])
                    {
                        completeItemCosts++;
                    }
                }
            }

            if (completeItemCosts == _item.Cost.Keys.Count)
            {
                // BUY
                List<InventorySlot> newContainer = new List<InventorySlot>(_inventory.Container);

                foreach (ItemSO item in _item.Cost.Keys)
                {
                    foreach (InventorySlot slot in _inventory.Container)
                    {
                        if (slot.Item == item && slot.Amount >= _item.Cost[item])
                        {
                            if (slot.Amount == _item.Cost[item])
                            {
                                newContainer.Remove(slot);
                            }
                            else
                            {
                                slot.Amount -= _item.Cost[item];
                            }
                        }
                    }
                }
                _inventory.Container = newContainer;
                Debug.Log("Item bought");
                //GameStats.Instance.Stats.ItemsBought++;
                EventManager.OnItemBuy?.Invoke(_item);
                EventManager.OnItemAddedToInventory?.Invoke();
                _craftingCanvas.SetActive(false);

            }
            else
            {
                //_insufficientItems.SetActive(true);
            }
        }

        private void Start()
        {
            if (GameStats.Instance.Stats.CavesVisited % 2 == 1)
            {
                _craftingCanvas.SetActive(true);
            }
        }
    }
}
