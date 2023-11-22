using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace CaveGame
{
    public class UpgradesManager : MonoBehaviour
    {
        [SerializeField] private InventorySO _inventory;
        //[SerializeField] private GameObject _boughtScreen;
        //[SerializeField] private GameObject _insufficientItems;
        [SerializeField] private GameObject _craftingCanvas;

        private bool _isPlacingItem = false;

        public void BuyUpgrade(ShopItemSO itemToBuy)
        {
            int completeItemCosts = 0;
            foreach (ItemSO item in itemToBuy.Cost.Keys )
            {
                foreach(InventorySlot slot in _inventory.Container)
                {
                    if (slot.Item == item && slot.Amount >= itemToBuy.Cost[item])
                    {
                        completeItemCosts++;
                    }
                }
            }

            if (completeItemCosts == itemToBuy.Cost.Keys.Count)
            {
                // BUY
                List<InventorySlot> newContainer = new List<InventorySlot>(_inventory.Container);

                foreach (ItemSO item in itemToBuy.Cost.Keys)
                {
                    foreach (InventorySlot slot in _inventory.Container)
                    {
                        if (slot.Item == item && slot.Amount >= itemToBuy.Cost[item])
                        {
                            if (slot.Amount == itemToBuy.Cost[item])
                            {
                                newContainer.Remove(slot);
                            }
                            else
                            {
                                slot.Amount -= itemToBuy.Cost[item];
                            }
                        }
                    }
                }
                _inventory.Container = newContainer;
                Debug.Log("Item bought");
                //GameStats.Instance.Stats.ItemsBought++;
                EventManager.OnItemBuy?.Invoke(itemToBuy);
                EventManager.OnInventoryChanged?.Invoke();
                _isPlacingItem = true;
                _craftingCanvas.SetActive(false);

            }
            else
            {
                //_insufficientItems.SetActive(true);
            }
        }

        private void Update()
        {
            if (_isPlacingItem && Input.GetMouseButtonDown(0))
            {
                _craftingCanvas.SetActive(true);
            }
        }
    }
}
