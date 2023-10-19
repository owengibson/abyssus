using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveGame
{
    public class GameStats : MonoBehaviour
    {
        public GameStatsSO Stats;

        public static GameStats Instance;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }

        private void OnApplicationQuit()
        {
            Stats.CavesVisited = 0;
        }
    }
}
