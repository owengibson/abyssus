using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveGame
{
    [CreateAssetMenu(fileName = "New Placed Items Database", menuName = "Placed Items Database")]
    public class PlacedItemsDatabaseSO : ScriptableObject
    {
        public List<PlacedItem> PlacedItems = new();

        private void OnEnable() => hideFlags = HideFlags.DontUnloadUnusedAsset;
    }

    [System.Serializable]
    public class PlacedItem
    {
        public ShopItemSO Item;
        public Vector3 Position;
        public Vector3 Rotation;

        public PlacedItem(ShopItemSO item, Vector3 position, Vector3 rotation)
        {
            Item = item;
            Position = position;
            Rotation = rotation;
        }
    }
}
