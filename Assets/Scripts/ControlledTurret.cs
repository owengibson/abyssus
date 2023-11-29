using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveGame
{
    public class ControlledTurret : Weapon
    {
        [SerializeField] private GameObject _popup;

        private SubmarinePlayerController _player;
        private SpriteRenderer _spriteRenderer;
        private bool _isInTrigger = false;

        private void Start()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;
            collision.TryGetComponent<SubmarinePlayerController>(out _player);
            _popup.SetActive(true);
            _isInTrigger = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;
            _popup.SetActive(false);
            _isInTrigger = false;
        }

        private void Update()
        {
            if (_isInTrigger && Input.GetKeyDown(KeyCode.E))
            {
                _popup.SetActive(!_popup.activeSelf);
            }
            if (_player.CurrentMode == SubmarinePlayerController.PlayerMode.Combat)
            {
                RotateToFaceMouse();
                if (Input.GetMouseButtonDown(0))
                {
                    PrimaryFire(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                }
            }
        }

        private void RotateToFaceMouse()
        {
            Vector2 mouseScreenPos = Input.mousePosition;
            Vector2 startingScreenPos = Camera.main.WorldToScreenPoint(transform.position);
            mouseScreenPos.x -= startingScreenPos.x;
            mouseScreenPos.y -= startingScreenPos.y;
            float angle = Mathf.Atan2(mouseScreenPos.y, mouseScreenPos.x) * Mathf.Rad2Deg;
            _spriteRenderer.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        }
    }
}
