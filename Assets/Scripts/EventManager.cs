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
        public static Action<Vector3> OnTerrainEdit;
        public static Action OnTerrainModeToggle;
        public static Action<EnemyController> OnEnemyClicked;
        public static Action<bool> OnEnterOrExitStartingArea;


        /*public delegate Item ItemReturnDelegate();
        public static ItemReturnDelegate OnItemClicked;*/
    }
}
