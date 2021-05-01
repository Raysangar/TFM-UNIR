using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Gameplay.Units
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerController : BaseUnit<PlayerSettings>
    {
        public System.Action OnDeath;

        private Camera mainCamera;
        private PlayerInput playerInput;
        private Vector3? mousePosition;

        public override void OnGameplayPaused()
        {
            base.OnGameplayPaused();
            playerInput.enabled = false;
        }

        public override void OnGameplayResumed()
        {
            base.OnGameplayResumed();
            playerInput.enabled = true;
        }

        protected override void Awake()
        {
            base.Awake();
            mousePosition = null;
            mainCamera = Camera.main;
            playerInput = GetComponent<PlayerInput>();
            Life.OnDeath += OnDeathCallback;
        }

        public void Move(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();
            Movement.SetMovement(new Vector3(input.x, 0, input.y));
        }

        public void Look(InputAction.CallbackContext context)
        {
            Movement.SetRotation(context.ReadValue<Quaternion>());
            mousePosition = null;
        }

        public void LookAt(InputAction.CallbackContext context)
        {
            Vector3 input = context.ReadValue<Vector2>();
            input.z = Vector3.Distance(mainCamera.transform.position, Movement.Position);
            mousePosition = input;
        }

        public void Shoot(InputAction.CallbackContext context)
        {
            if (context.started)
                Weapon.StartShooting();
            else if (context.canceled)
                Weapon.StopShooting();
        }

        public override void UpdateBehaviour(float deltaTime)
        {
            if (mousePosition.HasValue)
                Movement.SetLookTarget(mainCamera.ScreenToWorldPoint(mousePosition.Value));
            base.UpdateBehaviour(deltaTime);
        }

        private void OnDeathCallback()
        {
            playerInput.enabled = false;
            OnDeath();
        }
    }
}
