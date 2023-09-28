using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveGame
{
    [CreateAssetMenu(fileName = "New GameStats", menuName = "GameStats")]
    public class GameStatsSO : ScriptableObject
    {
        public int ItemsBought;
        public int CavesVisited;
    }
}
