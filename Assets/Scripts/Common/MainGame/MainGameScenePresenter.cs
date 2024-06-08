using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Common.MainGame
{
    public class MainGameScenePresenter
        : MonoBehaviour
    {
        [SerializeField]private Button titleButton;

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            // タイトルボタンをタップするとタイトルシーンに遷移する
            titleButton.OnClickAsObservable()
                .TakeUntilDestroy(this)
                .Subscribe(_ => 
                {
                    Debug.Log("Button Pushed");
                    SceneManager.LoadScene("Scenes/Title");
                    // SceneNavigator.Instance.Change("Title");
                });
        }
    }
}