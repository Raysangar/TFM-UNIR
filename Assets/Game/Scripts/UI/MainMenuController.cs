using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] Button continueButton;
    [SerializeField] Button newGameButton;
    [SerializeField] Button settingsButton;
    [SerializeField] Button exitButton;

    private void Awake()
    {
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
