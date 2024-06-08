using UnityEngine;

namespace Common.Title
{
    public class TitleScene
        : MonoBehaviour
    {
        [SerializeField]
        private TitleScenePresenter presenter;
        
        /// <summary>
        /// シーン初期化
        /// </summary>
        private void Start()
        {
            presenter.Initialize();
        }
    }
}