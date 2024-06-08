using Base;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Common.Title
{
    /// <summary>
    /// タイトルシーンプレゼンター
    /// </summary>
    public class TitleScenePresenter
        : SceneBasePresenter
    {
        /// <summary>
        /// スタートボタン
        /// </summary>
        [SerializeField]
        private Button startButton;
        
        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            startButton.OnClickAsObservable()
                .TakeUntilDestroy(this)
                .Subscribe(_ =>
                {
                    RequestChangeScene(SceneName.MainGame);
                });
        }
    }
}