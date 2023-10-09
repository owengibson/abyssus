using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveGame
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private MapGenerator _mapGenerator;
        [SerializeField] private int _spawnSearchRange;

        public GameObject Player;
        public GameObject PlayerParent;

        #region SINGLETON
        public static PlayerSpawner Instance;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            Instance = this;
        }
        #endregion

        private Vector2 CalculatePlayerSpawnPos()
        {
            Vector2 spawnPos = Vector2.zero;
            int mapWidth = _mapGenerator.Map.GetLength(0);
            int mapHeight = _mapGenerator.Map.GetLength(1);

            Debug.Log($"Map size: {mapWidth}, {mapHeight}");

            for (int x = 0; x < _spawnSearchRange; x++)
            {
                for (int y = 0; y < _spawnSearchRange; y++)
                {
                    if (_mapGenerator.Map[mapWidth - 1 - x, mapHeight - 1 - y] == 1) continue;
                    else if (_mapGenerator.Map[mapWidth - 1 - x, mapHeight - 1 - (y + 1)] == 0)
                    {
                        spawnPos = new Vector2(mapWidth - 1 - x + 0.5f, mapHeight - 1 - y);
                        return spawnPos;
                    }
                }
            }

            return spawnPos;
        }

        private void SpawnPlayer()
        {
            if (Player != null) return;
            PlayerParent = Instantiate(_playerPrefab, CalculatePlayerSpawnPos(), Quaternion.identity);
            Debug.Log("Player spawned");
            Player = PlayerParent.GetComponentInChildren<WaterPlayerController>().gameObject;
        }

        private void OnEnable()
        {
            EventManager.OnMapGenerated += SpawnPlayer;
        }
        private void OnDisable()
        {
            EventManager.OnMapGenerated -= SpawnPlayer;
        }
    }
}
