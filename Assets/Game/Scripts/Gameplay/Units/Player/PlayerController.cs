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

        private readonly int DirectionXAnimationId = Animator.StringToHash("DirectionX");
        private readonly int DirectionZAnimationId = Animator.StringToHash("DirectionZ");

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
            Vector3 direction = new Vector3(input.x, 0, input.y).normalized;
            Movement.SetMovement(direction);
            if (!Weapon.IsShooting)
            {
                Movement.SetRotation(Quaternion.LookRotation(direction));
                RemoveMoveTarget();
            }
            animator.SetBool(WalkAnimationId, input.x != 0 || input.y != 0);
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
            {
                Vector3 input = context.ReadValue<Vector2>();
                input.z = Vector3.Distance(mainCamera.transform.position, Movement.Position);
                mousePosition = input;
            }
            else
            {
                RemoveMoveTarget();
            }
        }

        public void Shoot(InputAction.CallbackContext context)
        {
            if (context.started)
            {
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
                Movement.SetLookTarget(mainCamera.ScreenToWorldPoint(mousePosition.Value));
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
