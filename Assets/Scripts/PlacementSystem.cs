using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CaveGame
{
    public class PlacementSystem : MonoBehaviour
    {
        private bool _isPlaced = true;
        private GameObject _currentPlaceable;
        private Grid _grid;

        private void Start()
        {
            _grid = GetComponent<Grid>();
        }

        private void SelectItem(ShopItemSO item)
        {
            _currentPlaceable = Instantiate(item.Prefab);
            _isPlaced = false;
            _currentPlaceable.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.6f);

        }

        private void Update()
        {
            if (!_isPlaced)
            {
                Vector3 pos = _grid.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition)) + _grid.cellSize / 2;
                _currentPlaceable.transform.position = pos;

                if(Input.GetKeyDown(KeyCode.R))
                {
                    _currentPlaceable.transform.eulerAngles += new Vector3(0, 0, 90);
                }

                // PLACE OBJECT
                if(Input.GetMouseButtonDown(0))
                {
                    _isPlaced = true;
                    _currentPlaceable.GetComponent<Turret>().enabled = true;
                    _currentPlaceable.GetComponent<SpriteRenderer>().color = Color.white;
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
    }
}
