using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Gameplay.Debug
{
    public class CheatsController : MonoBehaviour
    {
        [SerializeField] GameManager gameManager;
        [SerializeField] PlayerInput input;

        private void Start()
        {
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
