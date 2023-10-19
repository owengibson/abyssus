using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CaveGame
{
    public class PlacementSystem : MonoBehaviour
    {
        [SerializeField] private PlacedItemsDatabaseSO _placedItems;

        private bool _isPlaced = true;
        private GameObject _currentPlaceable;
        private Grid _grid;

        private void Start()
        {
            _grid = GetComponent<Grid>();

            foreach(var item in _placedItems.PlacedItems.Keys)
            {
                Instantiate(item.Prefab, _placedItems.PlacedItems[item]);
            }
        }

        private void SelectItem(ShopItemSO item)
        {
            _currentPlaceable = Instantiate(item.Prefab);
            _isPlaced = false;
            _currentPlaceable.GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1, 0.6f);

        }

        private void Update()
        {
            if (!_isPlaced)
            {
                Vector3 pos = _grid.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                _currentPlaceable.transform.position = pos;
                Turret currentPlaceableTurret = _currentPlaceable.GetComponent<Turret>();
                currentPlaceableTurret.enabled = false;

                if (Input.GetKeyDown(KeyCode.R))
                {
                    _currentPlaceable.transform.eulerAngles -= new Vector3(0, 0, 90);
                }

                // PLACE OBJECT
                if(Input.GetMouseButtonDown(0))
                {
                    _isPlaced = true;
                    currentPlaceableTurret.enabled = true;
                    _currentPlaceable.GetComponentInChildren<SpriteRenderer>().color = Color.white;
                    _placedItems.PlacedItems.Add(currentPlaceableTurret.Item, _currentPlaceable.transform);
                }
            }
        }

        private void OnEnable()
        {
            EventManager.OnItemBuy += SelectItem;
        }
        private void OnDisable()
        {
            EventManager.OnItemBuy -= SelectItem;
        }

        private void OnApplicationQuit()
        {
            _placedItems.PlacedItems.Clear();
        }
    }
}
