using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveGame
{
    [CreateAssetMenu(fileName = "New Enemy Drop Item", menuName = "Inventory System/Items/Enemy Drop")]
    public class EnemyDropItemSO : ItemSO
    {
        private void Awake()
        {
            Type = ItemType.EnemyDrop;
        }
    }
}
