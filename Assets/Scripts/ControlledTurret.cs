using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveGame
{
    public class ControlledTurret : Weapon
    {
        [SerializeField] private GameObject _popup;

        private bool _isInTrigger = false;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;
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
            if (_isInTrigger && Input.GetMouseButtonDown(0))
            {
                PrimaryFire(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
        }
    }
}
