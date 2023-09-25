using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveGame
{
    public class WaterPlayerController : MonoBehaviour
    {
        [SerializeField] private float _playerSpeed = 5f;
        [SerializeField] private InventorySO _inventory;
        [SerializeField] private float _pickupRange = 3f;

        private Rigidbody2D _rigidbody2D;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void MovePlayer()
        {
            var horizontalInput = Input.GetAxisRaw("Horizontal");
            var verticalInput = Input.GetAxisRaw("Vertical");
            //_rigidbody2D.velocity = new Vector2(horizontalInput, verticalInput) * _playerSpeed;
            _rigidbody2D.AddForce(new Vector2(horizontalInput, verticalInput) * _playerSpeed);
        }

        private void RotateToFaceMouse()
        {
            Vector2 mouseScreenPos = Input.mousePosition;
            Vector2 startingScreenPos = Camera.main.WorldToScreenPoint(transform.position);
            mouseScreenPos.x -= startingScreenPos.x;
            mouseScreenPos.y -= startingScreenPos.y;
            float angle = Mathf.Atan2(mouseScreenPos.y, mouseScreenPos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        }

        private void FixedUpdate()
        {
            MovePlayer();
            RotateToFaceMouse();
        }

        private void PickupItem(Item item)
        {
            if (Vector2.Distance(transform.position, item.transform.position) >= _pickupRange) return;

            _inventory.AddItem(item.p_Item, 1);
            Destroy(item.gameObject);
            EventManager.OnItemAddedToInventory?.Invoke();
        }

        private void OnEnable()
        {
            EventManager.OnItemClicked += PickupItem;
        }
        private void OnDisable()
        {
            EventManager.OnItemClicked -= PickupItem;
        }

        private void OnApplicationQuit()
        {
            _inventory.Container.Clear();
        }
    }
}
