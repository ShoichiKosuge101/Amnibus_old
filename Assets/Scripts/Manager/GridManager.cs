using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    /// <summary>
    /// グリッドマネージャ
    /// </summary>
    public class GridManager
        : MonoBehaviour
    {
        public static GridManager Instance { get; private set; }
        
        /// <summary>
        /// 壁の位置情報
        /// </summary>
        private readonly HashSet<Vector2Int> _walls = new HashSet<Vector2Int>();

        /// <summary>
        /// 開始
        /// </summary>
        private void Awake()
        {
            // シングルトン
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            // 初期化処理
            _walls.Clear();
        }
        
        /// <summary>
        /// 壁の配置
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void PlaceWall(int x, int y)
        {
            _walls.Add(new Vector2Int(x, y));
        }
        
        /// <summary>
        /// 壁かどうか
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool IsWall(int x, int y)
        {
            return _walls.Contains(new Vector2Int(x, y));
        }
    }
}