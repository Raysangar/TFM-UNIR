using UnityEngine;

namespace Game.Gameplay.Units
{
    public class Movement : MonoBehaviour
    {
        public Vector3 Position => cachedTransform.position;

        private Transform cachedTransform;
        private Vector3 currentMovement;
        private Vector3 currentTarget;
        private float speed;

        public void Init(float speed)
        {
            this.speed = speed;
        }

        private void Awake()
        {
            cachedTransform = transform;
            currentMovement = Vector3.zero;
        }

        public void SetMovement(Vector3 movement)
        {
            currentMovement = movement * speed;
        }

        public void SetLookTarget(Vector3 target)
        {
            currentTarget = target;
        }

        public void SetRotation(Quaternion rotation)
        {
            cachedTransform.localRotation = rotation;
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
