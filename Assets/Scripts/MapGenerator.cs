using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Collections;

namespace CaveGame
{
    public class MapGenerator : SerializedMonoBehaviour
    {
        [SerializeField] private int _width;
        [SerializeField] private int _height;

        [ShowIf("@!_useRandomSeed")]
        [SerializeField] private string _seed;
        [SerializeField] private bool _useRandomSeed;

        [Range(0, 100)]
        [SerializeField] private int _randomFillPercent;

        [Space]
        [SerializeField] private TileBase _groundTile;
        [SerializeField] private TileBase _backgroundTile;
        [SerializeField] private Tilemap _tilemap;
        [Space]

        [SerializeField] private MineableItemSO[] _itemsToGenerate;
        [Space]

        [SerializeField] private Dictionary<EnemySO, int> _enemiesToGenerate;

        public int[,] Map { get; private set; }

        private TileBase _blankTile;
        private List<GameObject> _generatedItems = new();
        private List<GameObject> _generatedEnemies = new();

        private void Start()
        {
            GenerateMap();
        }

        private void GenerateMap()
        {
            Map = new int[_width, _height];
            RandomFillMap();

            for (int i = 0; i < 5; i++)
            {
                SmoothMap();
            }

            RenderMap();
            EventManager.OnMapGenerated?.Invoke();
            Debug.Log("Map generated");

            Invoke("RescanPathfindingGraph", 0.1f);

            GenerateItems();
            Debug.Log("Items generated");

            SpawnEnemies();
        }

        private void RescanPathfindingGraph()
        {
            AstarPath.active.Scan();
        }


        private void RandomFillMap()
        {
            if (_useRandomSeed)
            {
                _seed = Time.time.ToString() + UnityEngine.Random.Range(0, 100000).ToString();
            }

            System.Random pseudoRandom = new System.Random(_seed.GetHashCode());

            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    if (x == 0 || x == _width - 1 || y == 0 || y == _height - 1)
                    {
                        Map[x, y] = 1;
                    }
                    else
                    {
                        Map[x, y] = (pseudoRandom.Next(0, 100) < _randomFillPercent) ? 1 : 0;
                    }
                }
            }
        }

        private void SmoothMap()
        {
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    int neighbourWallTiles = GetSurroundingWallCount(x, y);

                    if (neighbourWallTiles > 4)
                        Map[x, y] = 1;
                    else if (neighbourWallTiles < 4)
                        Map[x, y] = 0;

                }
            }
        }

        private int GetSurroundingWallCount(int gridX, int gridY)
        {
            int wallCount = 0;
            for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
            {
                for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
                {
                    if (neighbourX >= 0 && neighbourX < _width && neighbourY >= 0 && neighbourY < _height)
                    {
                        if (neighbourX != gridX || neighbourY != gridY)
                        {
                            wallCount += Map[neighbourX, neighbourY];
                        }
                    }
                    else
                    {
                        wallCount++;
                    }
                }
            }

            return wallCount;
        }

        private void RenderMap()
        {
            if (Map == null) return;
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    _tilemap.SetTile(new Vector3Int(x,y), (Map[x, y] == 1) ? _groundTile : _blankTile);
                }
            }
        }

        private void GenerateItems()
        {
            foreach(var item in _itemsToGenerate)
            {
                if (Map == null) return;
                for (int x = 0; x < _width - 1; x++)
                {
                    for (int y = 0; y < _height - 2; y++)
                    {
                        // This checks to see if x,y is too close to other generated items.
                        foreach (var existingItem in _generatedItems)
                        {
                            if (Vector2.Distance(new Vector2(x, y + 1), existingItem.transform.position) < item.ProximityLimit) continue;
                        }

                        if (Map[x, y] == 0) continue;

                        // This checks to see if there is a blank 2x2 square above and to the right of Map[x,y] (this is a suitable spawn location)
                        else if (Map[x, y + 1] == 0 && Map[x, y + 2] == 0 & Map[x + 1, y + 1] == 0 && Map[x + 1, y + 2] == 0)
                        {
                            if (UnityEngine.Random.Range(0, 100) <= item.Rarity)
                            {
                                //var spawnPos = _tilemap.GetCellCenterWorld(new Vector3Int(x, y));
                                var generatedItem = Instantiate(item.Prefab, new Vector2(x, y + 1), Quaternion.identity);
                                _generatedItems.Add(generatedItem);
                            }
                        }
                    }
                }
            }
        }

        private void SpawnEnemies()
        {

            foreach (EnemySO enemy in _enemiesToGenerate.Keys)
            {
                Debug.Log($"Spawning {_enemiesToGenerate[enemy]} of {enemy.Name}");

                for (int i = 0; i < _enemiesToGenerate[enemy]; i++)
                {
                    int x = UnityEngine.Random.Range(0, _width);
                    int y = UnityEngine.Random.Range(0, _height);

                    if (Map[x, y] == 1)
                    {
                        i--;
                        continue;
                    }
                    /*for (int j = -5; j < 6; j++)
                    {
                    }*/
                    if (Map[x - 1, y] == 1 || Map[x - 1, y - 1] == 1 || Map[x, y - 1] == 1)
                    {
                        i--;
                        continue;
                    }

                    // SUITABLE SPAWN LOCATION
                    var generatedEnemy = Instantiate(enemy.Prefab, new Vector2(x, y), Quaternion.identity);
                    _generatedEnemies.Add(generatedEnemy);
                }
            }
        }


        private void RemoveTileAtPosition(Vector3 position)
        {
            Debug.Log("RemoveTileAtPosition called at " + position);
            _tilemap.SetTile(Vector3Int.FloorToInt(position), _blankTile);
        }
        private void OnEnable()
        {
            EventManager.OnTerrainEdit += RemoveTileAtPosition;
        }

        private void OnDisable()
        {
            EventManager.OnTerrainEdit -= RemoveTileAtPosition;

            ClearMap();
        }

        [Button("Generate Map", ButtonSizes.Large)]
        private void GenerateMapEditorOnly()
        {
            Map = new int[_width, _height];
            RandomFillMap();

            for (int i = 0; i < 5; i++)
            {
                SmoothMap();
            }

            RenderMap();
        }

        [Button("Clear Map", ButtonSizes.Large), GUIColor(1, 0, 0)]
        private void ClearMap()
        {
            //if (Map != null) return;
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    _tilemap.SetTile(new Vector3Int(x, y), _blankTile);
                }
            }
        }

    }
}
