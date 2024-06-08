using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Map
{
    public class MapGenerator
        : MonoBehaviour
    {
        public int width = 10;
        public int height = 10;
        public GameObject floorPrefab;
        public GameObject wallPrefab;
        public float tileSize = 1.0f;

        private void Start()
        {
            GenerateMap();
        }

        /// <summary>
        /// マップ生成
        /// </summary>
        private void GenerateMap()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    var tilePrefab = Random.value > 0.8f ? wallPrefab : floorPrefab;
                    Vector3 position = new Vector3(i * tileSize, j * tileSize,0);
                    var tile = Instantiate(tilePrefab, position, Quaternion.identity);
                    tile.transform.SetParent(transform);
                }
            }
        }
    }
}