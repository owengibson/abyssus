using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Sirenix.OdinInspector;

namespace CaveGame
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] private int _width;
        [SerializeField] private int _height;

        [SerializeField] private string _seed;
        [SerializeField] private bool _useRandomSeed;

        [Range(0, 100)]
        [SerializeField] private int _randomFillPercent;

        [Space]
        [SerializeField] private TileBase _groundTile;
        [SerializeField] private TileBase _backgroundTile;
        [SerializeField] private Tilemap _tilemap;

        public int[,] Map { get; private set; }

        private TileBase _blankTile;

        private void Start()
        {
            GenerateMap();
            Debug.Log(Time.time.ToString());
        }

        [Button("Generate Map", ButtonSizes.Large)]
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

        [Button("Clear Map", ButtonSizes.Large), GUIColor(1, 0, 0)]
        private void ClearMap()
        {
            if (Map != null) return;
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    _tilemap.SetTile(new Vector3Int(x, y), _blankTile);
                }
            }
        }

        private void OnDisable()
        {
            ClearMap();
        }
    }
}
