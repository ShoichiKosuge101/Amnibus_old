using Base;
using Constants;
using UnityEngine;
using Utils;

namespace Manager
{
    /// <summary>
    /// ゲームマネージャ
    /// </summary>
    public class GameManager
        : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        /// <summary>
        /// プレイヤーのインスタンス
        /// </summary>
        private GameObject _playerInstance;

        /// <summary>
        /// 現在のゲームの状態
        /// </summary>
        public MainGameStatus CurrentGameStatus { get; private set; } = MainGameStatus.Playing;
        public bool IsPlaying => CurrentGameStatus == MainGameStatus.Playing;
        
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
            InitializeGame();
            
            // GridManagerの初期化
            GridManager.Instance.Initialize();
        }

        /// <summary>
        /// ゲームの初期化
        /// </summary>
        private void InitializeGame()
        {
            // ステージの加算ロード
            // 最初のステージはStage1
            string scenePath = SceneUtility.GetScenePath(SceneName.Stage1);
            SceneNavigator.Instance.LoadAdditiveScene(scenePath);
        }

        /// <summary>
        /// ポーズ中かどうかを設定
        /// </summary>
        /// <param name="status"></param>
        public void SetGameState(MainGameStatus status)
        {
            CurrentGameStatus = status;
        }
        
        /// <summary>
        /// プレイヤーのインスタンスを設定
        /// </summary>
        /// <param name="playerInstance"></param>
        public void SetPlayerInstance(GameObject playerInstance)
        {
            _playerInstance = playerInstance;
        }
        
        /// <summary>
        /// プレイヤーのインスタンスを取得
        /// </summary>
        /// <returns></returns>
        public GameObject GetPlayerInstance()
        {
            return _playerInstance;
        }

        /// <summary>
        /// クリーンアップ
        /// </summary>
        public void Cleanup()
        {
            // GridManagerのクリーンアップ
            GridManager.Instance.Cleanup();
            
            // シーンの破棄
            string scenePath = SceneUtility.GetScenePath(SceneName.Stage1);
            SceneNavigator.Instance.UnloadAdditiveScene(scenePath);
            
            // 自身のリセット
            Instance = null;
        }
    }
}