using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace CaveGame
{
    public class PlacementSystem : MonoBehaviour
    {
        [SerializeField] private PlacedItemsDatabaseSO _placedItems;
        [SerializeField] private AudioSource _turretPlaceSfx;
        [SerializeField] private Vector2[] _suitablePlaces;

        private bool _isPlaced = true;
        private GameObject _currentPlaceable;
        private Grid _grid;

        //private Tween _shakeTween(Transform t) => t.DOPunchPosition(Vector2.left * 0.5f, .4f, 20, 45).OnComplete(() => Debug.Log("Tween called"));

        private void Start()
        {
            DOTween.Clear(true);

            _grid = GetComponent<Grid>();

            foreach(var item in _placedItems.PlacedItems)
            {
                Instantiate(item.Item.Prefab, item.Position, Quaternion.Euler(item.Rotation));
            }
        }

        private void SelectItem(ShopItemSO item)
        {
            if (!item.IsPlaceable) return;
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
                AutonomousTurret currentPlaceableTurret = _currentPlaceable.GetComponent<AutonomousTurret>();
                currentPlaceableTurret.enabled = false;

                RotatePlaceable();


                // PLACE OBJECT
                if(Input.GetMouseButtonDown(0))
                {
                    if (_suitablePlaces.Contains(pos))
                    {
                        _turretPlaceSfx.Play();
                        _isPlaced = true;
                        currentPlaceableTurret.enabled = true;
                        _currentPlaceable.GetComponentInChildren<SpriteRenderer>().color = Color.white;
                        Transform copiedTransform = _currentPlaceable.transform;
                        _placedItems.PlacedItems.Add(new PlacedItem(currentPlaceableTurret.Item, _currentPlaceable.transform.position, _currentPlaceable.transform.eulerAngles));

                        EventManager.OnTutorialPromptCompleted?.Invoke(3);
                        EventManager.OnPlaceablePlaced?.Invoke();
                    }
                    else
                    {
                        Debug.Log("Invalid placeable location");
                        //_shakeTween(_currentPlaceable.transform).Play();
                        var placeableRenderer = currentPlaceableTurret.GetComponentInChildren<SpriteRenderer>();
                        placeableRenderer.DOColor(Color.red, 0.25f).OnComplete(() => placeableRenderer.DOColor(Color.white, 0.25f));
                        DOTween.Shake(() => _currentPlaceable.transform.position, x => _currentPlaceable.transform.position = x, 0.4f, new Vector3(0.5f, 0, 0), 20, 45);
                    } 
                }
            }
        }

        private void RotatePlaceable()
        {
            Vector2 placeable = Camera.main.WorldToScreenPoint(_currentPlaceable.transform.position);
            Vector2 origin = Camera.main.WorldToScreenPoint(new Vector2(0, 4));
            placeable.x -= origin.x;
            placeable.y -= origin.y;
            float angle = Mathf.Atan2(placeable.y, placeable.x) * Mathf.Rad2Deg;
            int zAngle = Mathf.CeilToInt(new Vector3(0, 0, angle - 180).z);
            _currentPlaceable.transform.rotation = Quaternion.Euler(new Vector3(0, 0, zAngle));
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
