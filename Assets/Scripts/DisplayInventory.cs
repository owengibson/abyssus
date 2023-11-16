using DG.Tweening;
using Sirenix.OdinInspector;
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
        [Space]

        [SerializeField] private bool _canInventoryBeToggled = true;
        [ShowIf("_canInventoryBeToggled")]
        [SerializeField] private Vector3 _hiddenPosition;
        [ShowIf("_canInventoryBeToggled")]
        [SerializeField] private Vector3 _shownPosition;

        private Dictionary<InventorySlot, GameObject> _itemsDisplayed = new();
        private Transform[] _slots;

        private enum InventoryDisplayState { Hidden, InProgress, Shown };
        private InventoryDisplayState _displayState = InventoryDisplayState.Hidden;


        private void Start()
        {
            _slots = GetComponentsInChildren<Transform>();
            CreateDisplay();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab) && _canInventoryBeToggled)
            {
                ToggleInventory(ref _displayState);
            }
        }

        private void ToggleInventory(ref InventoryDisplayState state)
        {
            switch (state)
            {
                case InventoryDisplayState.Hidden:
                    state = InventoryDisplayState.InProgress;
                    transform.DOLocalMove(_shownPosition, 0.5f).SetEase(Ease.OutBack).OnComplete(() => _displayState = InventoryDisplayState.Shown);
                    break;

                case InventoryDisplayState.InProgress:
                    break;

                case InventoryDisplayState.Shown:
                    state = InventoryDisplayState.InProgress;
                    transform.DOLocalMove(_hiddenPosition, 0.5f).SetEase(Ease.InBack).OnComplete(() => _displayState = InventoryDisplayState.Hidden);
                    break;

                default:
                    break;
            }
        }

        private void CreateDisplay()
        {
            Transform[] slots = GetComponentsInChildren<Transform>();

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

        private void OnApplicationQuit()
        {
            _inventory.Container.Clear();
        }
    }
}
