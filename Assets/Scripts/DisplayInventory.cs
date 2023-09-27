using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

namespace CaveGame
{
    public class DisplayInventory : MonoBehaviour
    {
        [SerializeField] private InventorySO _inventory;

        private Dictionary<InventorySlot, GameObject> _itemsDisplayed = new();
        private Transform[] _slots;

        private void Start()
        {
            _slots = GetComponentsInChildren<Transform>();
            CreateDisplay();
        }

        private void Update()
        {
            //UpdateDisplay();
        }

        private void CreateDisplay()
        {
            Transform[] slots = GetComponentsInChildren<Transform>();
            Debug.Log(slots.Length);

            for (int i = 0; i < _inventory.Container.Count; i++)
            {
                var newItem = Instantiate(_inventory.Container[i].Item.UIPrefab, slots[i + 1].transform.position, Quaternion.identity, slots[i + 1]);
                newItem.GetComponentInChildren<TextMeshProUGUI>().text = _inventory.Container[i].Amount.ToString("n0");
                _itemsDisplayed.Add(_inventory.Container[i], newItem);
            }
        }

        private void UpdateDisplay()
        {
            for (int i = 0; i < _inventory.Container.Count; i++)
            {
                if (_itemsDisplayed.ContainsKey(_inventory.Container[i]))
                {
                    _itemsDisplayed[_inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = _inventory.Container[i].Amount.ToString("n0");
                }
                else
                {
                    var newItem = Instantiate(_inventory.Container[i].Item.UIPrefab, _slots[i + 1].transform.position, Quaternion.identity, _slots[i + 1]);
                    newItem.GetComponentInChildren<TextMeshProUGUI>().text = _inventory.Container[i].Amount.ToString("n0");
                    _itemsDisplayed.Add(_inventory.Container[i], newItem);
                }
            }
        }

        private void OnEnable()
        {
            EventManager.OnItemAddedToInventory += UpdateDisplay;
        }
        private void OnDisable()
        {
            EventManager.OnItemAddedToInventory -= UpdateDisplay;
        }
    }
}
