using UnityEngine;
using Core.EntitySystem;

namespace Game.Gameplay.Units
{
    public class Movement : EntityComponent
    {
        public Vector3 Position => cachedTransform.position;
        public Vector3 CurrentDirection { get; private set; }

        public float CurrentSpeed => weapon.IsShooting ? speedWhileShooting : usualSpeed;

        private readonly float usualSpeed;
        private readonly float speedWhileShooting;
        private readonly WeaponsSystem.Weapon weapon;
        private readonly Transform cachedTransform;

        private Vector3? currentTarget;

        public Movement(Entity entity, float usualSpeed, float speedWhileShooting, WeaponsSystem.Weapon weapon) : base(entity)
        {
            this.usualSpeed = usualSpeed;
            this.speedWhileShooting = speedWhileShooting;
            this.weapon = weapon;
            CurrentDirection = Vector3.zero;
            currentTarget = null;
            cachedTransform = entity.transform;
        }

        public void SetMovement(Vector3 movement)
        {
            CurrentDirection = movement;
        }

        public void SetLookTarget(Vector3? target)
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
            cachedTransform.Translate(CurrentDirection * CurrentSpeed * deltaTime, Space.World);
        }

        private void UpdateAim()
        {
            if (currentTarget.HasValue)
            {
                Vector3 lookPosition = currentTarget.Value - cachedTransform.position;
                lookPosition.y = 0;
                cachedTransform.localRotation = Quaternion.LookRotation(lookPosition);
            }
        }
    }
}
