using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveGame
{
    public class WaterPlayerController : MonoBehaviour
    {
        public enum PlayerMode { Normal, Terrain }

        [SerializeField] private float _playerSpeed = 5f;
        [SerializeField] private InventorySO _inventory;
        [SerializeField] private float _pickupRange = 3f;
        [Space]

        [SerializeField] private Texture2D _defaultCursor;
        [SerializeField] private Texture2D _terrainModeCursor;
        [SerializeField] private GameObject _terrainModeBorderPrefab;
        [SerializeField] private GameObject _drill;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private SpriteRenderer _playerSprite;

        private Rigidbody2D _rigidbody2D;
        private Weapon _weapon;

        public PlayerMode CurrentMode = PlayerMode.Normal;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _weapon = GetComponent<Weapon>();

            Cursor.SetCursor(_defaultCursor, Vector2.zero, CursorMode.ForceSoftware);
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

            if (transform.eulerAngles.z > 0 && transform.eulerAngles.z < 180)
            {
                _playerSprite.flipX = true;
            }
            else
            {
                _playerSprite.flipX = false;
            }
        }

        private void FixedUpdate()
        {
            MovePlayer();
            RotateToFaceMouse();
        }

        private void PickupItem(Item item)
        {
            //if (Vector2.Distance(transform.position, item.transform.position) > _pickupRange) return;

            _inventory.AddItem(item.p_Item, 1);
            item.transform.DOMove(transform.position, 0.25f).OnComplete(() => Destroy(item.gameObject));
            item.transform.DOScale(0, 0.25f);
            EventManager.OnInventoryChanged?.Invoke();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                EventManager.OnTerrainModeToggle?.Invoke();
                if (CurrentMode == PlayerMode.Terrain)
                {
                    CurrentMode = PlayerMode.Normal;
                    Cursor.SetCursor(_defaultCursor, Vector2.zero, CursorMode.ForceSoftware);
                    Debug.Log("Exited terrain edit mode");
                    _drill.SetActive(false);
                }
                else
                {
                    CurrentMode = PlayerMode.Terrain;
                    Cursor.SetCursor(_terrainModeCursor, Vector2.zero, CursorMode.ForceSoftware);
                    Debug.Log("Entered terrain edit mode");
                    _drill.SetActive(true);
                }
            }

            // old digging
            /*Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetMouseButtonDown(0) && CurrentMode == PlayerMode.Terrain && Vector2.Distance(transform.position, pos) <= _pickupRange)
            {
                EventManager.OnTerrainEdit?.Invoke(new Vector3(pos.x, pos.y, 0));
            }*/

            else if (Input.GetMouseButtonDown(0) && CurrentMode == PlayerMode.Normal)
            {
                // shooting
                _weapon.PrimaryFire(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }

            // Particle system plays when moving
            if (_rigidbody2D.velocity.magnitude > 0.01 && !_particleSystem.isPlaying)
            {
                _particleSystem.Play();
            }
            else if (_rigidbody2D.velocity.magnitude < 0.01 && _particleSystem.isPlaying)
            {
                _particleSystem.Stop();
            }
        }

        public void DrillBounceback(Vector2 direction)
        {
            Debug.Log(direction);
            // this doesn't work :(
            _rigidbody2D.velocity = direction * 3;
        }

        private void OnEnable()
        {
            EventManager.OnItemCollided += PickupItem;
        }
        private void OnDisable()
        {
            EventManager.OnItemCollided -= PickupItem;
        }
    }
}
