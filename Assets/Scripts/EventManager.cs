using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveGame
{
    public class EventManager : MonoBehaviour
    {
        public static Action OnMapGenerated;
        public static Action<Item> OnItemClicked;
        public static Action OnItemAddedToInventory;


        /*public delegate Item ItemReturnDelegate();
        public static ItemReturnDelegate OnItemClicked;*/
    }
}
