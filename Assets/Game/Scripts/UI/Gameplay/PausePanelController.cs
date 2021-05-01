using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Core.Utils.UI;

namespace Game.UI.Gameplay
{
    public class PausePanelController : BasePanelController
    {
        [SerializeField] Button continueButton;
        [SerializeField] Button exitButton;

        private GamepadMenuController gamepadMenuController;

        public override void Show()
        {
            gamepadMenuController.ForceSelectionTo(continueButton.gameObject);
            gameObject.SetActive(true);
            IsActive = true;
        }

        public override void Hide()
        {
            gamepadMenuController.ForceSelectionTo(null);
            gameObject.SetActive(false);
            IsActive = false;
        }

        public void Init(GamepadMenuController gamepadMenuController)
        {
            this.gamepadMenuController = gamepadMenuController;
            continueButton.onClick.AddListener(OnContinueButtonClicked);
            exitButton.onClick.AddListener(OnExitButtonClicked);
        }

        private void OnContinueButtonClicked()
        {
            Hide();
        }

        private void OnExitButtonClicked()
        {
            SceneManager.LoadScene(Constants.Scenes.MainMenuIndex);
        }
    }
}
