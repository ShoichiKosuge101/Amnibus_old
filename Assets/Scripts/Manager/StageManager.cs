using Map;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

namespace Manager
{
    /// <summary>
    /// ステージマネージャ
    /// </summary>
    public class StageManager
        : MonoBehaviour
    {
        /// <summary>
        /// ステージデータ
        /// </summary>
        public StageData stageData;
        
        public GameObject playerPrefab;
        public GameObject floorPrefab;
        public GameObject wallPrefab;
        public GameObject goalPrefab;
        
        public float tileSize = 1.0f;
        
        private Camera _mainCamera;
        
        /// <summary>
        /// ゴールの位置
        /// </summary>
        private Vector3 _goalPosition;
        
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
            
            Assert.IsNotNull(stageData);
            
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
            float stageCenterX = stageData.width * tileSize / 2;
            float stageCenterY = stageData.height * tileSize / 2;
            Vector3 stageCenter = new Vector3(stageCenterX, stageCenterY, -10f);
            
            // カメラの位置をステージの中心に設定
            _mainCamera.transform.position = new Vector3(
                stageCenter.x, 
                stageCenter.y, 
                _mainCamera.transform.position.z);
            
            // カメラのサイズをステージの大きさに合わせる
            float aspectRatio = _mainCamera.aspect;
            float verticalSize = stageData.height * tileSize / 2;
            float horizontalSize = stageData.width * tileSize / (2 * aspectRatio);

            // 縦横両方のサイズを比較して大きい方を採用
            const float padding = 1.0f;
            _mainCamera.orthographicSize = Mathf.Max(verticalSize, horizontalSize) * padding;
        }

        /// <summary>
        /// ステージの初期化
        /// </summary>
        private void InitializeStage()
        {
            // GridManagerの初期化
            GridManager.Instance.Initialize();

            // ゴールの生成
            GenerateGoal();

            // Map生成
            GenerateMap();
            
            // プレイヤーの生成
            GeneratePlayer();
        }

        /// <summary>
        /// ゴールの生成
        /// </summary>
        private void GenerateGoal()
        {
            int goalX, goalY;
            do
            {
                goalX = Random.Range(1, stageData.width - 1);
                goalY = Random.Range(1, stageData.height - 1);
            } while (GridManager.Instance.IsWall(goalX, goalY) || IsMapEdge(goalX, goalY));
            
            _goalPosition = new Vector3(goalX * tileSize, goalY * tileSize, 0);
            Instantiate(goalPrefab, _goalPosition, Quaternion.identity);
            
            // ゴールの位置を設定
            GameManager.Instance.SetGoalPosition(_goalPosition);
        }
        
        /// <summary>
        /// ゴールの位置を取得
        /// </summary>
        /// <returns></returns>
        public Vector3 GetGoalPosition()
        {
            return _goalPosition;
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
                    GameObject tileObj;
                    // ゴールの位置に壁を生成しない
                    if (new Vector3(i * tileSize, j * tileSize, 0) == _goalPosition)
                    {
                        tileObj = floorPrefab;
                    }
                    else
                    {
                        // マップの端なら壁、それ以外なら床か壁をランダムで生成
                        tileObj = IsMapEdge(i, j) 
                            ? wallPrefab 
                            : GetRandomTile();
                    }
                    
                    Vector3 position = new Vector3(i * tileSize, j * tileSize,0);
                    var tile = Instantiate(tileObj, position, Quaternion.identity);
                    tile.transform.SetParent(transform);
                    
                    // GridManagerに壁の位置を登録
                    if (tileObj == wallPrefab)
                    {
                        GridManager.Instance.PlaceWall(i, j);
                    }
                }
            }
        }

        /// <summary>
        /// マップの端かどうか
        /// </summary>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        /// <returns></returns>
        private bool IsMapEdge(int posX, int posY)
        {
            // マップの端かどうか
            return posX == 0 
                   || posY == 0 
                   || posX == stageData.width - 1 
                   || posY == stageData.height - 1;
        }

        /// <summary>
        /// プレイヤーの生成
        /// </summary>
        private void GeneratePlayer()
        {
            // ここでは固定位置に生成
            float playerX = (float)stageData.width / 2;
            float playerY = (float)stageData.height / 2;
            Vector3 spawnPosition = new Vector3(playerX, playerY, 0);
            
            // プレイヤーの位置が壁とゴールに被らないようにする
            while (GridManager.Instance.IsWall((int)playerX, (int)playerY)
                   || spawnPosition == _goalPosition)
            {
                playerX++;
                // ステージの端に到達したら反対側に移動
                if(playerX >= stageData.width - 1)
                {
                    playerX = 1;
                }
                
                playerY++;
                // ステージの端に到達したら反対側に移動
                if(playerY >= stageData.height - 1)
                {
                    playerY = 1;
                }
                
                spawnPosition = new Vector3(playerX * tileSize, playerY * tileSize, 0);
            }
            
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