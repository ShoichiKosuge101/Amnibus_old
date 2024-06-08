using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Common.Title
{
    public class TitleScenePresenter
        : MonoBehaviour
    {
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
                    Debug.Log("Button Pushed");
                    SceneManager.LoadScene("Scenes/MainGame");
                });
        }
    }
}