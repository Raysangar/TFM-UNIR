using UnityEngine;
using Core.EntitySystem;

namespace Game.Gameplay.Units
{
    public class Movement : EntityComponent
    {
        public Vector3 Position => cachedTransform.position;

        public float CurrentSpeed => weapon.IsShooting ? speedWhileShooting : usualSpeed;

        private readonly float usualSpeed;
        private readonly float speedWhileShooting;
        private readonly WeaponsSystem.Weapon weapon;

        private Transform cachedTransform;
        private Vector3 currentMovement;
        private Vector3 currentTarget;

        public Movement(Entity entity, float usualSpeed, float speedWhileShooting, WeaponsSystem.Weapon weapon) : base(entity)
        {
            this.usualSpeed = usualSpeed;
            this.speedWhileShooting = speedWhileShooting;
            this.weapon = weapon;
            currentMovement = Vector3.zero;
            currentTarget = Vector3.forward;
            cachedTransform = entity.transform;
        }

        public void SetMovement(Vector3 movement)
        {
            currentMovement = movement;
        }

        public void SetLookTarget(Vector3 target)
        {
            currentTarget = target;
        }

        public void SetRotation(Quaternion rotation)
        {
            cachedTransform.localRotation = rotation;
        }

        public override void UpdateBehaviour(float deltaTime)
        {
            UpdateMovement(deltaTime);
            UpdateAim();
        }

        private void UpdateMovement(float deltaTime)
        {
            cachedTransform.Translate(currentMovement * CurrentSpeed * deltaTime, Space.World);
        }

        private void UpdateAim()
        {
            Vector3 lookPosition = currentTarget - cachedTransform.position;
            lookPosition.y = 0;
            cachedTransform.localRotation = Quaternion.LookRotation(lookPosition);
        }
    }
}
