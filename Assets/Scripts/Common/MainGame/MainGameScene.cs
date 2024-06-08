using UnityEngine;

namespace Common.MainGame
{
    public class MainGameScene
        : MonoBehaviour
    {
        [SerializeField]
        private MainGameScenePresenter presenter;

        /// <summary>
        /// シーン初期化
        /// </summary>
        private void Start()
        {
            presenter.Initialize();
        }
    }
}