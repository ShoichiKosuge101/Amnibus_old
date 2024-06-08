using UnityEngine;

namespace Map
{
    /// <summary>
    /// タイル
    /// </summary>
    public class Tile
        : MonoBehaviour
    {
        public enum TileType
        {
            Floor = 0,
            Wall  = 1,
        }
        
        public TileType Type { get; private set; }
    }
}