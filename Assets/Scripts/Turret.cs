using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveGame
{
    public class Turret : MonoBehaviour
    {
        public ShopItemSO Item;
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
        }
    }
}
