using UnityEngine;

namespace Game.UI.Gameplay
{
    public class LoadingScreenController : BasePanelController
    {
        [SerializeField] CanvasGroup canvasGroup;
        
        public override void Hide()
        {
            gameObject.SetActive(false);
        }

        public override void Show()
        {
            gameObject.SetActive(true);
        }
    }
}