using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        [SerializeField] private GameObject _repairedNotification;
        [SerializeField] private AudioSource _repairSfx;

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
                if (itemToBuy.IsPlaceable)
                {
                    _craftingCanvas.SetActive(false);
                    _isPlacingItem = true;
                  
                }
                else if (itemToBuy.GetType() == typeof(RepairItemSO))
                {
                    RepairItemSO repairItem = (RepairItemSO)itemToBuy;

                    EventManager.OnSubmarineTakeDamage?.Invoke(-repairItem.RepairAmount);
                    EventManager.OnTutorialPromptCompleted?.Invoke(2);

                    _repairSfx.Play();
                    var popup = Instantiate(_repairedNotification, _craftingCanvas.transform);
                    popup.GetComponentInChildren<TextMeshProUGUI>().text = $"Repaired submarine by {repairItem.RepairAmount} health points";
                    popup.transform.DOMoveY(280, 0.5f).SetEase(Ease.OutBack).OnComplete(() => StartCoroutine(DismissRepairNotification(popup)));
                }
            }
            else
            {
                //_insufficientItems.SetActive(true);
            }
        }

        private IEnumerator DismissRepairNotification(GameObject notification)
        {
            yield return new WaitForSeconds(2);
            notification.transform.DOMoveY(-196, 0.5f).SetEase(Ease.InBack).OnComplete(() => Destroy(notification));
        }

        private void ActivateCraftingUI()
        {
            _craftingCanvas.SetActive(true);
        }

        private void OnEnable()
        {
            EventManager.OnPlaceablePlaced += ActivateCraftingUI;
        }
        private void OnDisable()
        {
            EventManager.OnPlaceablePlaced -= ActivateCraftingUI;
        }
    }
}
