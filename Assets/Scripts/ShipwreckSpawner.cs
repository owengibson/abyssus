using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveGame
{
    public class ShipwreckSpawner : MonoBehaviour
    {
        [SerializeField] private MapGenerator _mapGenerator;
        [SerializeField] private GameObject _shipwreckPrefab;

        private void SpawnShipwreck()
        {
            Vector2 spawnPos = CalculateShipwreckSpawnPosition();
            Instantiate(_shipwreckPrefab, spawnPos, Quaternion.identity);
            Debug.Log("Shipwreck spawned at " + spawnPos);
        }

        // This calculates a suitable spawn position for the shipwreck.
        // A suitable spawn position constitutes five ground tiles in a row,
        // and an empty 5 * 5 area above that. Only the bottom-left quadrant of the map is searched.
        private Vector2 CalculateShipwreckSpawnPosition()
        {
            int mapWidth = _mapGenerator.Map.GetLength(0);
            int mapHeight = _mapGenerator.Map.GetLength(1);

            for (int y = 0; y < mapHeight / 2; y++)
            {
                for (int x = 0; x < mapWidth / 2; x++)
                {
                    // Is the spawn root ground? If not, find a new root.
                    if (_mapGenerator.Map[x, y] == 0) continue;
                    Debug.Log("Shipwreck root is ground");

                    // // Are the 4 blocks to the right of the root ground? If not, find a new root.
                    for (int g = 0; g < 4; g++)
                    {
                        if (_mapGenerator.Map[x + g, y] == 0) break;
                    }
                    Debug.Log("Shipwreck root has 5 wide ground platform");

                    // Is there a clear 5*5 area above and to the right of the root? If not, find a new root
                    bool isGroundTile = false;
                    for (int up = 0; up < 5; up++)
                    {
                        if (isGroundTile) break;
                        for (int across = 0; across < 4; across++)
                        {
                            if (_mapGenerator.Map[x + across, y + up] == 1)
                            {
                                isGroundTile = true;
                                Debug.Log("Tile [" + _mapGenerator.Map[x + across, y + up] + "] is [" + isGroundTile + "]");
                                break;
                            }
                        }
                    }
                    Debug.Log("Shipwreck has a 5*5 clear area above root");
                    Debug.Log(x + " " + y);

                    // Suitable spawn position found!!!
                    Vector2 spawnPos = new Vector2(x, y);
                    return spawnPos;
                }
            }
            Debug.LogWarning("Suitable shipwreck spawn position not found");
            return Vector2.zero;
        }

        private void OnEnable() => EventManager.OnMapGenerated += SpawnShipwreck;
        private void OnDisable() => EventManager.OnMapGenerated -= SpawnShipwreck;
    }
}
