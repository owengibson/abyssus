using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CaveGame
{
    public class Item : MonoBehaviour
    {
        public ItemSO p_Item;

        /*public void OnPointerClick(PointerEventData eventData)
        {
            EventManager.OnItemClicked?.Invoke(this);
            Debug.Log(gameObject.name + " clicked");
        }*/

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;

            EventManager.OnItemCollided?.Invoke(this);
        }
    }
}
