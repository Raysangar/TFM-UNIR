using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Gameplay.Units
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerController : BaseUnit<PlayerSettings>
    {
        public System.Action OnDeath;

        private CameraController cameraController;
        private PlayerInput playerInput;
        private Vector3? mousePosition;

        private readonly int DirectionXAnimationId = Animator.StringToHash("DirectionX");
        private readonly int DirectionZAnimationId = Animator.StringToHash("DirectionZ");

        public void ResetValues()
        {
            Life.AddLife(Life.Max);
            for (int i = 0; i < Weapon.AmmoTypesCount; ++i)
                Weapon.AddAmmo(i, Weapon.ClipSize);
            Movement.SetLookTarget(null);
            Movement.SetMovement(Vector3.zero);
        }

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

        public void InitBehaviour(CameraController cameraController)
        {
            mousePosition = null;
            this.cameraController = cameraController;
            playerInput = GetComponent<PlayerInput>();
            Life.OnDeath += OnDeathCallback;
        }

        public void Move(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();
            Vector3 direction = Quaternion.Euler(0, cameraController.transform.localEulerAngles.y, 0) * new Vector3(input.x, 0, input.y).normalized;
            bool isMoving = input.x != 0 || input.y != 0;
            Movement.SetMovement(direction);
            if (!Weapon.IsShooting && isMoving)
            {
                Movement.SetRotation(Quaternion.LookRotation(direction));
                RemoveMoveTarget();
            }
            animator.SetBool(WalkAnimationId, isMoving);
        }

        public void Look(InputAction.CallbackContext context)
        {
            if (Weapon.IsShooting)
                Movement.SetRotation(context.ReadValue<Quaternion>());
            RemoveMoveTarget();
        }

        public void LookAt(InputAction.CallbackContext context)
        {
            if (Weapon.IsShooting)
                AimToMousePosition(context.ReadValue<Vector2>());
            else
                RemoveMoveTarget();
        }

        private void AimToMousePosition(Vector3 input)
        {
            input.z = Vector3.Distance(cameraController.transform.position, Movement.Position);
            mousePosition = input;
        }

        public void Shoot(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
                    AimToMousePosition(Mouse.current.position.ReadDefaultValue());
                Weapon.StartShooting();
                animator.SetBool(ShootAnimationId, true);
            }
            else if (context.canceled)
            {
                Weapon.StopShooting();
                animator.SetBool(ShootAnimationId, false);
            }
        }

        public void AmmoShortcut1(InputAction.CallbackContext _)
        {
            Weapon.SetEquippedAmmo(0);
        }

        public void AmmoShortcut2(InputAction.CallbackContext _)
        {
            Weapon.SetEquippedAmmo(1);
        }

        public void AmmoShortcut3(InputAction.CallbackContext _)
        {
            Weapon.SetEquippedAmmo(2);
        }

        public override void UpdateBehaviour(float deltaTime)
        {
            if (mousePosition.HasValue)
                Movement.SetLookTarget(cameraController.Camera.ScreenToWorldPoint(mousePosition.Value));
            if (Weapon.IsShooting)
            {
                if (Movement.CurrentDirection.x > 0 || Movement.CurrentDirection.y > 0)
                {
                    float radianBetweenDirectionAndTarget = Vector3.Angle(Movement.CurrentDirection, transform.forward) * Mathf.Deg2Rad;
                    float blendX = Mathf.Cos(radianBetweenDirectionAndTarget);
                    float blendY = Mathf.Sin(radianBetweenDirectionAndTarget);
                    animator.SetFloat(DirectionXAnimationId, blendX);
                    animator.SetFloat(DirectionZAnimationId, blendY);
                }
            }
            base.UpdateBehaviour(deltaTime);
        }

        private void RemoveMoveTarget()
        {
            mousePosition = null;
            Movement.SetLookTarget(null);
        }

        private void OnDeathCallback()
        {
            playerInput.enabled = false;
            OnDeath();
        }
    }
}
