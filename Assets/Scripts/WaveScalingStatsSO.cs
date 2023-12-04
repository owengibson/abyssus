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
        public List<Dictionary<EnemySO, int>> Waves;
        public int WaveRoundCount;
        public float TimeBetweenWaves;
        public float TimeBetweenEnemies;
        [Space]

        [FoldoutGroup("Scaling Parameters")]
        [MinValue(1f)]
        [SerializeField] private float spawnTimeReduction = 1.25f;
        [FoldoutGroup("Scaling Parameters")]
        [MinValue(1f)]
        [SerializeField] private float newWaveCountMultiplier = 1.25f;
        [FoldoutGroup("Scaling Parameters")]
        [MinValue(1f)]
        [SerializeField] private float enemyAmountMultiplier = 1.25f;
        [Space]

        public EnemySO Piranha;

        public void ScaleWaves()
        {
            int previousNumOfEnemies = Waves.Last().Last().Value;

            TimeBetweenEnemies /= spawnTimeReduction;
            TimeBetweenWaves /= spawnTimeReduction;

            int numOfNewWaves = Mathf.Clamp(Mathf.FloorToInt(WaveRoundCount / newWaveCountMultiplier), 1, 10000);

            for (int i = 0; i < numOfNewWaves; i++)
            {
                var newWave = new Dictionary<EnemySO, int>
                        {
                            { Piranha, Mathf.CeilToInt((previousNumOfEnemies + i) * enemyAmountMultiplier) }
                        };
                Waves.Add(newWave);
                Debug.Log($"Added {numOfNewWaves} new waves");
            }
        }

        public void Init()
        {
            Waves = new List<Dictionary<EnemySO, int>>
            {
                new Dictionary<EnemySO, int> { { Piranha, 3 } },
                new Dictionary<EnemySO, int> { { Piranha, 4 } },
            };
            WaveRoundCount = 0;
            TimeBetweenEnemies = 1;
            TimeBetweenWaves = 8;
        }

        private void OnEnable()
        {
            hideFlags = HideFlags.DontUnloadUnusedAsset;
            Init();
        }
        private void OnDisable() => Init();
    }
}
