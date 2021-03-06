using UnityEngine;
using UnityEngine.SceneManagement;
using Core.Utils;

namespace Game
{
    public class LoadingScreen : MonoBehaviour
    {
        private void Awake()
        {
            loadSceneOperation = SceneManager.LoadSceneAsync(1);
            loadingBar.SetLocalScaleX(0);
        }

        private void Update()
        {
            loadingBar.SetLocalScaleX(loadSceneOperation.progress);
        }

        [SerializeField] RectTransform loadingBar;

        private AsyncOperation loadSceneOperation;
    }
}
