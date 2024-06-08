using UnityEngine;

namespace Base
{
    /// <summary>
    /// シーン遷移クラス
    /// </summary>
    public class SceneNavigator
        : MonoBehaviour
    {
        /// <summary>
        /// インスタンス
        /// </summary>
        public static SceneNavigator Instance { get; private set; }
        
        /// <summary>
        /// 初期化
        /// </summary>
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                
                // シーン遷移時に破棄されないようにする
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this);
            }
        }
        
        /// <summary>
        /// シーン遷移
        /// </summary>
        /// <param name="sceneName"></param>
        public void GoToScene(string sceneName)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }
}