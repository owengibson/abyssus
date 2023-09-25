using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CaveGame
{
    public class Item : MonoBehaviour, IPointerClickHandler
    {
        public ItemSO p_Item;

        public void OnPointerClick(PointerEventData eventData)
        {
            EventManager.OnItemClicked?.Invoke(this);
            Debug.Log(gameObject.name + " clicked");
        }
    }
}
