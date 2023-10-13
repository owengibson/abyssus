using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace CaveGame
{
    public class SubmarinePlayerController : MonoBehaviour
    {
        public enum PlayerMode { Default, Combat }
        public PlayerMode CurrentMode = PlayerMode.Default;

        [SerializeField] private float _playerSpeed;

        private Rigidbody2D _rigidbody2D;
        private SpriteRenderer _spriteRenderer;

        private bool _isInTurretArea = false;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
        private void FixedUpdate()
        {
            MovePlayer();
            RotateToFaceMouse();
        }

        private void MovePlayer()
        {
            if (CurrentMode == PlayerMode.Combat) return;
            var horizontalInput = Input.GetAxisRaw("Horizontal");
            var verticalInput = Input.GetAxisRaw("Vertical");
            //_rigidbody2D.velocity = new Vector2(horizontalInput, verticalInput) * _playerSpeed;
            //_rigidbody2D.AddForce(new Vector2(horizontalInput, verticalInput) * _playerSpeed);
            _rigidbody2D.position += new Vector2(horizontalInput, verticalInput) * _playerSpeed;
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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E) && _isInTurretArea)
            {
                if (CurrentMode == PlayerMode.Default)
                {
                    CurrentMode = PlayerMode.Combat;
                    _spriteRenderer.enabled = false;
                }
                else
                {
                    CurrentMode = PlayerMode.Default;
                    _spriteRenderer.enabled = true;
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Turret")) return;
            _isInTurretArea = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.CompareTag("Turret")) return;
            _isInTurretArea = false;
        }
    }
}
