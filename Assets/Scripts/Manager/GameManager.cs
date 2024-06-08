using Map;
using UnityEngine;

namespace Manager
{
    /// <summary>
    /// ゲームマネージャ
    /// </summary>
    public class GameManager
        : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        
        public GameObject playerPrefab;
        private GameObject playerInstance;

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
        /// 初期化
        /// </summary>
        private void Start()
        {
            // GridManagerの初期化
            GridManager.Instance.Initialize();
            
            // MapGeneratorの初期化
            MapGenerator.Instance.GenerateMap();
            
            // プレイヤーの生成
            GeneratePlayer();
        }

        /// <summary>
        /// プレイヤーの生成
        /// </summary>
        private void GeneratePlayer()
        {
            Vector3 spawnPosition = new Vector3(1, 1, 0);
            playerInstance = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
        }
    }
}