using Map;
using Player;
using UnityEngine;

namespace Manager
{
    /// <summary>
    /// ステージマネージャ
    /// </summary>
    public class StageManager
        : MonoBehaviour
    {
        public static StageManager Instance { get; private set; }
        
        public GameObject playerPrefab;
        public GameObject floorPrefab;
        public GameObject wallPrefab;
        
        public float tileSize = 1.0f;

        /// <summary>
        /// ステージデータ
        /// </summary>
        public StageData stageData;

        /// <summary>
        /// 開始
        /// </summary>
        private void Awake()
        {
            // シングルトン
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// 開始
        /// </summary>
        private void Start()
        {
            // ステージの初期化
            InitializeStage();
        }

        /// <summary>
        /// ステージの初期化
        /// </summary>
        private void InitializeStage()
        {
            // GridManagerの初期化
            GridManager.Instance.Initialize();
            
            // Map生成
            GenerateMap();
            
            // プレイヤーの生成
            GeneratePlayer();
        }

        /// <summary>
        /// マップ生成
        /// </summary>
        public void GenerateMap()
        {
            for (int i = 0; i < stageData.width; i++)
            {
                for (int j = 0; j < stageData.height; j++)
                {
                    var randomTile = GetRandomTile();
                    
                    Vector3 position = new Vector3(i * tileSize, j * tileSize,0);
                    var tile = Instantiate(randomTile, position, Quaternion.identity);
                    tile.transform.SetParent(transform);
                    
                    // GridManagerに壁の位置を登録
                    if (randomTile == wallPrefab)
                    {
                        GridManager.Instance.PlaceWall(i, j);
                    }
                }
            }
            
            // プレイヤーの生成
            GeneratePlayer();
        }
        
        /// <summary>
        /// プレイヤーの生成
        /// </summary>
        private void GeneratePlayer()
        {
            // ここでは固定位置に生成
            float playerX = stageData?.width / 2 ?? 0;
            float playerY = stageData?.height / 2 ?? 0;
            Vector3 spawnPosition = new Vector3(playerX, playerY, 0);
            
            var playerObj = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
            
            // プレイヤーの設定
            GameManager.Instance.SetPlayerInstance(playerObj);
        }
        
        /// <summary>
        /// タイルのプレハブを判定
        /// </summary>
        /// <returns></returns>
        private GameObject GetRandomTile()
        {
            const float threshold = 0.8f;
            
            return Random.value > threshold
                ? wallPrefab 
                : floorPrefab;
        }
    }
}