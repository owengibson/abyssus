using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveGame
{
    [CreateAssetMenu(fileName = "New Repair Shop Item", menuName = "Inventory System/Items/Repair")]
    public class RepairItemSO : ShopItemSO
    {
        public float RepairAmount;
    }
}
