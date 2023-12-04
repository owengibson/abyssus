using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveGame
{
    public class EventManager : MonoBehaviour
    {
        public static Action OnMapGenerated;
        public static Action<Item> OnItemCollided;
        public static Action OnInventoryChanged;
        public static Action<Vector3> OnTerrainEdit;
        public static Action OnTerrainModeToggle;
        public static Action<Enemy> OnEnemyClicked;
        public static Action<WaveEnemyAI> OnWaveEnemyClicked;
        public static Action<bool> OnEnterOrExitStartingArea;
        public static Action<float, float> OnPlayerTakeDamage;
        public static Action OnPlayerDeath;
        public static Action<ShopItemSO> OnItemBuy;
        public static Action<GameObject> OnEnemyDie;
        public static Action<int> OnTutorialPromptCompleted;
        public static Action OnTutorialTurretEntered;
        public static Action OnPlaceablePlaced;
        
        public delegate float FloatReturnFloatParamDelegate(float value);
        public static FloatReturnFloatParamDelegate OnSubmarineTakeDamage;


        /*public delegate Item ItemReturnDelegate();
        public static ItemReturnDelegate OnItemClicked;*/
    }
}
