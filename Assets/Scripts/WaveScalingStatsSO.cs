using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CaveGame
{
    [CreateAssetMenu(fileName = "New Wave Scaling Stats", menuName = "Wave Scaling Stats")]
    public class WaveScalingStatsSO : SerializedScriptableObject
    {
        public Dictionary<EnemySO, int>[] Waves;
        public int WaveRoundCount;
        public float TimeBetweenWaves;
        public float TimeBetweenEnemies;
        [Space]

        public EnemySO Piranha;

        public void ScaleWaves()
        {
            TimeBetweenEnemies /= 1.25f;
            TimeBetweenWaves /= 1.25f;

            int numOfNewWaves = Mathf.Clamp(Mathf.FloorToInt(WaveRoundCount / 1.25f), 1, 10000);

            for (int i = 0; i < numOfNewWaves; i++)
            {
                int numOfEnemies = Waves.Last().Last().Value;
                var newWave = new Dictionary<EnemySO, int>
                        {
                            { Piranha, Mathf.CeilToInt(numOfEnemies * 1.5f) }
                        };
                Waves.Append(newWave);
                Debug.Log($"Added {numOfNewWaves} new waves");
            }
        }

        private void Awake()
        {
            Waves = new Dictionary<EnemySO, int>[]
            {
                new Dictionary<EnemySO, int> { { Piranha, 5 } },
                new Dictionary<EnemySO, int> { { Piranha, 6 } },
            };
            WaveRoundCount = 0;
            TimeBetweenEnemies = 1;
            TimeBetweenWaves = 8;
        }

        private void OnEnable() => hideFlags = HideFlags.DontUnloadUnusedAsset;
    }
}
