using Map;
using UnityEngine;

namespace Manager
{
    /// <summary>
    /// ステージマネージャ
    /// </summary>
    public class StageManager
        : MonoBehaviour
    {
        public GameObject playerPrefab;
        public GameObject floorPrefab;
        public GameObject wallPrefab;
        
        public float tileSize = 1.0f;

        /// <summary>
        /// ステージデータ
        /// </summary>
        public StageData stageData;
        
        private Camera _mainCamera;
        
        /// <summary>
        /// 開始
        /// </summary>
        private void Start()
        {
            _mainCamera = Camera.main;
            if(_mainCamera == null)
            {
                UnityEngine.Debug.LogError("Main Camera is missing");
                return;
            }
            
            // ステージの初期化
            InitializeStage();
            
            // カメラの位置を調整
            AdjustCameraPosition();
        }

        /// <summary>
        /// カメラの位置を調整
        /// </summary>
        private void AdjustCameraPosition()
        {
            // ステージの中心を計算
            float stageCenterX = stageData?.width * tileSize / 2 ?? 0;
            float stageCenterY = stageData?.height * tileSize / 2 ?? 0;
            Vector3 stageCenter = new Vector3(stageCenterX, stageCenterY, -10f);
            
            // カメラの位置をステージの中心に設定
            _mainCamera.transform.position = new Vector3(
                stageCenter.x, 
                stageCenter.y, 
                _mainCamera.transform.position.z);
            
            // カメラのサイズをステージの大きさに合わせる
            float aspectRatio = _mainCamera.aspect;
            float verticalSize = stageData?.height ?? 0 * tileSize / 2;
            float horizontalSize = stageData?.width ?? 0 * tileSize / (2 * aspectRatio);

            // 縦横両方のサイズを比較して大きい方を採用
            const float padding = 0.8f;
            _mainCamera.orthographicSize = Mathf.Max(verticalSize, horizontalSize) * padding;
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