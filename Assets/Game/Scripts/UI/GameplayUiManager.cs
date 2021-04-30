using UnityEngine;
using UnityEngine.InputSystem;
using Game.Gameplay;

namespace Game.UI
{
    [RequireComponent(typeof(GameManager), typeof(PlayerInput))]
    public class GameplayUiManager : MonoBehaviour
    {
        [SerializeField] HudController hud;
        [SerializeField] AmmoSelectionPanelController ammoSelectionPanel;

        private GameManager gameManager;
        private PlayerInput input;

        public void ToggleAmmoSelectionPanel(InputAction.CallbackContext context)
        {
            switch(context.phase)
            {
                case InputActionPhase.Started:
                    ammoSelectionPanel.Show();
                    break;
                case InputActionPhase.Canceled:
                    ammoSelectionPanel.Hide(equipLastAmmoHovered: input.currentControlScheme != "KeyBoardMouse");
                    break;
            }
                
        }

        public void OnLookInput(InputAction.CallbackContext context)
        {
            if (ammoSelectionPanel.IsShowing)
                ammoSelectionPanel.OnJoystickSelectionInput(context);
        }

        public void OnLookAtInput(InputAction.CallbackContext context)
        {
            if (ammoSelectionPanel.IsShowing)
                ammoSelectionPanel.OnMouseSelectionInput(context);
        }

        public void OnSubmitInput(InputAction.CallbackContext _)
        {
            if (ammoSelectionPanel.IsShowing)
                ammoSelectionPanel.OnSubmitInput();
        }

        private void Awake()
        {
            gameManager = GetComponent<GameManager>();
            input = GetComponent<PlayerInput>();

            gameManager.Player.OnDeath += OnPlayerDeath;
        }

        private void Start()
        {
            hud.Init(gameManager.Player);
            ammoSelectionPanel.Init(gameManager.Player);
        }

        private void OnPlayerDeath()
        {
            //TODO: Game over popup
        }
    }
}
