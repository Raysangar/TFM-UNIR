using UnityEngine;
using UnityEngine.InputSystem;
using Game.Gameplay;
using Core.Utils.UI;

namespace Game.UI.Gameplay
{
    [RequireComponent(typeof(GameManager), typeof(PlayerInput), typeof(GamepadMenuController))]
    public class GameplayUiManager : MonoBehaviour
    {
        [SerializeField] HudController hud;
        [SerializeField] AmmoSelectionPanelController ammoSelectionPanel;
        [SerializeField] GameOverPanelController gameOverPanel;
        [SerializeField] PausePanelController pausePanel;

        private GameManager gameManager;
        private PlayerInput input;
        private GamepadMenuController gamepadMenuController;

        public void ToggleAmmoSelectionPanel(InputAction.CallbackContext context)
        {
            switch(context.phase)
            {
                case InputActionPhase.Started:
                    ammoSelectionPanel.Show();
                    break;
                case InputActionPhase.Canceled:
                    if (ammoSelectionPanel.IsActive)
                        ammoSelectionPanel.Hide();
                    break;
            }
                
        }

        public void OnPauseInput(InputAction.CallbackContext _)
        {
            if (ammoSelectionPanel.IsActive)
                ammoSelectionPanel.Hide();

            if (pausePanel.IsActive)
                pausePanel.Hide();
            else
                pausePanel.Show();
        }

        public void OnLookInput(InputAction.CallbackContext context)
        {
            if (ammoSelectionPanel.IsActive)
                ammoSelectionPanel.OnJoystickSelectionInput(context);
        }

        public void OnLookAtInput(InputAction.CallbackContext context)
        {
            if (ammoSelectionPanel.IsActive)
                ammoSelectionPanel.OnMouseSelectionInput(context);
        }

        public void OnSubmitInput(InputAction.CallbackContext _)
        {
            if (ammoSelectionPanel.IsActive)
                ammoSelectionPanel.OnSubmitInput();
        }

        private void GainUiFocus()
        {
            gameManager.Pause();
        }

        private void LooseUiFocus()
        {
            gameManager.Resume();
        }

        private void Awake()
        {
            gameManager = GetComponent<GameManager>();
            input = GetComponent<PlayerInput>();
            gamepadMenuController = GetComponent<GamepadMenuController>();

            gamepadMenuController.ForceSelectionTo(null);
            gameManager.Player.OnDeath += OnPlayerDeath;
        }

        private void Start()
        {
            hud.Init(gameManager.Player);
            
            ammoSelectionPanel.Init(gameManager.Player, equipLastAmmoHoveredBeforeHiding: input.currentControlScheme != "KeyBoardMouse");
            ammoSelectionPanel.Init(GainUiFocus, LooseUiFocus);

            gameOverPanel.Init(gamepadMenuController);
            gameOverPanel.Init(GainUiFocus, LooseUiFocus);

            pausePanel.Init(gamepadMenuController);
            pausePanel.Init(GainUiFocus, LooseUiFocus);
        }

        private void OnPlayerDeath()
        {
            input.enabled = false;
            gameOverPanel.Show();
        }
    }
}
