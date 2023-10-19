using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveGame
{
    [CreateAssetMenu(fileName = "New Placed Items Database", menuName = "Placed Items Database")]
    public class PlacedItemsDatabaseSO : ScriptableObject
    {
        public Dictionary<ShopItemSO, Transform> PlacedItems = new();
    }
}
