namespace Utils
{
    /// <summary>
    /// シーン名
    /// </summary>
    public enum SceneName
    {
        Title,
        MainGame,
    }
    
    /// <summary>
    /// シーンパスを取得するユーティリティ
    /// </summary>
    public static class SceneUtility
    {
        /// <summary>
        /// シーン名からシーンパスを取得する
        /// </summary>
        /// <param name="sceneName"></param>
        /// <returns></returns>
        public static string GetScenePath(SceneName sceneName)
        {
            return sceneName switch
            {
                SceneName.Title    => "Scenes/Title",
                SceneName.MainGame => "Scenes/MainGame",
                _                  => null,
            };
        }
    }
}