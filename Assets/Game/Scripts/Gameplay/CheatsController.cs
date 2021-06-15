using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Gameplay.Debug
{
    [RequireComponent(typeof(GameManager), typeof(PlayerInput))]
    public class CheatsController : MonoBehaviour
    {
        private GameManager gameManager;

        private void Awake()
        {
            gameManager = GetComponent<GameManager>();
            enabled = UnityEngine.Debug.isDebugBuild && Keyboard.current != null;
        }

        private void Update()
        {
            CheckCheatKeys();
        }

        private void CheckCheatKeys()
        {
            if (Keyboard.current.enterKey.wasPressedThisFrame)
                gameManager.TryLoadNextLevel();
        }
    }
}
