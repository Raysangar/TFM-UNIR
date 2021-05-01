using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Core.Utils.UI;

namespace Game.UI
{
    [RequireComponent(typeof(GamepadMenuController))]
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] Button continueButton;
        [SerializeField] Button newGameButton;
        [SerializeField] Button settingsButton;
        [SerializeField] Button exitButton;

        private GamepadMenuController gamepadMenuController;

        private void Awake()
        {
            gamepadMenuController = GetComponent<GamepadMenuController>();
            gamepadMenuController.ForceSelectionTo(continueButton.gameObject);
            continueButton.onClick.AddListener(OnContinueButtonClicked);
            newGameButton.onClick.AddListener(OnNewGameButtonClicked);
            settingsButton.onClick.AddListener(OnSettingsButtonClicked);
            exitButton.onClick.AddListener(OnExitButtonClicked);
        }

        private void OnContinueButtonClicked()
        {
            SceneManager.LoadScene(2);
        }

        private void OnNewGameButtonClicked()
        {
            SceneManager.LoadScene(2);
        }

        private void OnSettingsButtonClicked()
        {

        }

        private void OnExitButtonClicked()
        {
            Application.Quit();
        }
    }
}
