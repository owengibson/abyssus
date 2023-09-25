using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveGame
{
    public abstract class ItemSO : ScriptableObject
    {
        public ItemType Type;
        public GameObject Prefab;
        [Space]
        public string Name;
        [TextArea(10, 15)]
        public string Description;
    }
}
