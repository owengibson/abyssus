using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveGame
{
    public class WaveManager : SerializedMonoBehaviour
    {
        [SerializeField] private Dictionary<EnemySO, int>[] _waves;
        [Space(30)]

        [SerializeField] private Transform[] _spawnPoints;
        [Space]

        [SerializeField] private float _timeBetweenWaves;
        [SerializeField] private float _timeBetweenEnemies;
        [SerializeField] private Transform _enemyTarget;
        [Space]

        [SerializeField] private GameObject _leaveButton;

        private void Start()
        {
            if (GameStats.Instance.Stats.CavesVisited % 2 == 0)
            {
                StartCoroutine(SpawnWaves());
                _leaveButton.SetActive(true);
            }
            //StartCoroutine(SpawnWaves());
        }

        private IEnumerator SpawnWaves()
        {
            foreach (var wave in _waves)
            {
                foreach(var enemyType in wave.Keys)
                {
                    for (int i = 0; i < wave[enemyType]; i++)
                    {
                        var spawnedEnemy = Instantiate(enemyType.Prefab, _spawnPoints[Random.Range(0, _spawnPoints.Length)].position, Quaternion.identity);
                        var spawnedEnemyAI = spawnedEnemy.GetComponent<WaveEnemyAI>();
                        spawnedEnemyAI.Target = _enemyTarget;
                        spawnedEnemyAI.Enemy = enemyType;

                        yield return new WaitForSeconds(_timeBetweenEnemies);
                    }
                }

                yield return new WaitForSeconds(_timeBetweenWaves);
            }
            yield return new WaitForSeconds(5);
            _leaveButton.SetActive(true);
        }
    }
}
