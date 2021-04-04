using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Gameplay.Player
{
    [RequireComponent(typeof(PlayerInput), typeof(WeaponsSystem.Weapon))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float speed;
        [SerializeField] WeaponsSystem.Weapon weapon;

        private Transform cachedTransform;
        private Vector3 currentMovement;
        private Vector3 currentTarget;
        private Camera mainCamera;

        private void Awake()
        {
            cachedTransform = transform;
            currentMovement = Vector3.zero;
            mainCamera = Camera.main;
        }

        public void Move(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();
            currentMovement.x = input.x;
            currentMovement.z = input.y;
            currentMovement *= speed;
        }

        public void Look(InputAction.CallbackContext context)
        {
            cachedTransform.localRotation = context.ReadValue<Quaternion>();
        }

        public void LookAt(InputAction.CallbackContext context)
        {
            Vector3 input = context.ReadValue<Vector2>();
            input.z = Vector3.Distance(mainCamera.transform.position, cachedTransform.position);
            currentTarget = mainCamera.ScreenToWorldPoint(input);
        }

        public void Shoot(InputAction.CallbackContext context)
        {
            if (context.started)
                weapon.StartShooting();
            else if (context.canceled)
                weapon.StopShooting();
        }

        private void Update()
        {
            UpdateMovement();
            UpdateAim();
        }

        private void UpdateMovement()
        {
            cachedTransform.Translate(currentMovement * Time.deltaTime, Space.World);
        }

        private void UpdateAim()
        {
            Vector3 lookPosition = currentTarget - cachedTransform.position;
            lookPosition.y = 0;
            cachedTransform.localRotation = Quaternion.LookRotation(lookPosition);
        }
    }
}
