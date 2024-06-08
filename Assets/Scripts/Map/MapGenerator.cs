using UnityEngine;
using Random = UnityEngine.Random;

namespace Map
{
    /// <summary>
    /// マップジェネレーター
    /// </summary>
    public class MapGenerator
        : MonoBehaviour
    {
        public static MapGenerator Instance { get; private set; }
        
        public int width = 10;
        public int height = 10;
        public GameObject floorPrefab;
        public GameObject wallPrefab;
        public float tileSize = 1.0f;

        /// <summary>
        /// 開始
        /// </summary>
        private void Awake()
        {
            // シングルトン
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// マップ生成
        /// </summary>
        public void GenerateMap()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    var randomTile = JudgeTilePrefab();
                    
                    Vector3 position = new Vector3(i * tileSize, j * tileSize,0);
                    var tile = Instantiate(randomTile, position, Quaternion.identity);
                    tile.transform.SetParent(transform);
                    
                    // GridManagerに壁の位置を登録
                    if (randomTile == wallPrefab)
                    {
                        Manager.GridManager.Instance.PlaceWall(i, j);
                    }
                }
            }
        }
        
        /// <summary>
        /// タイルのプレハブを判定
        /// </summary>
        /// <returns></returns>
        private GameObject JudgeTilePrefab()
        {
            const float threshold = 0.8f;
            
            return Random.value > threshold
                ? wallPrefab 
                : floorPrefab;
        }
    }
}