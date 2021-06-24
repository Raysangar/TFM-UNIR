using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Core.Utils.UI;
using System.Collections;

namespace Game.UI
{
    [RequireComponent(typeof(GamepadMenuController))]
    public class MainMenuController : MonoBehaviour
    {
        public enum State { Hub, Settings, Credits };

        public State CurrentState { get; private set; }

        [Header("HUB")]
        [SerializeField] CanvasGroup hubParent;
        [SerializeField] Button continueButton;
        [SerializeField] Button newGameButton;
        [SerializeField] Button creditsButton;
        [SerializeField] Button settingsButton;
        [SerializeField] Button exitButton;

        [Header("Settings")]
        [SerializeField] CanvasGroup settingsParent;
        [SerializeField] Button settingsBackButton;

        [Header("Credits")]
        [SerializeField] CanvasGroup creditsParent;

        private GamepadMenuController gamepadMenuController;

        private void Awake()
        {
            gamepadMenuController = GetComponent<GamepadMenuController>();
            gamepadMenuController.ForceSelectionTo(continueButton.gameObject);
            continueButton.onClick.AddListener(OnContinueButtonClicked);
            newGameButton.onClick.AddListener(OnNewGameButtonClicked);
            settingsButton.onClick.AddListener(OnSettingsButtonClicked);
            exitButton.onClick.AddListener(OnExitButtonClicked);
            creditsButton.onClick.AddListener(OnCreditsButtonClicked);
            settingsBackButton.onClick.AddListener(OnSettingsBackButtonClicked);

            SetState(State.Hub);
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
            SetState(State.Settings);
        }

        private void OnExitButtonClicked()
        {
            Application.Quit();
        }

        private void OnCreditsButtonClicked()
        {
            SetState(State.Credits);
            StartCoroutine(CreditsAnimation());
        }

        private IEnumerator CreditsAnimation()
        {
            yield return new WaitForSeconds(3);
            SetState(State.Hub);
        }

        private void OnSettingsBackButtonClicked()
        {
            SetState(State.Hub);
        }

        private void SetState(State state)
        {
            CurrentState = state;
            hubParent.gameObject.SetActive(state == State.Hub);
            creditsParent.gameObject.SetActive(state == State.Credits);
            settingsParent.gameObject.SetActive(state == State.Settings);
        }
    }
}
