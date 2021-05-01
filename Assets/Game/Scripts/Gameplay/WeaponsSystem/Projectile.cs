using UnityEngine;
using Core.Utils.Pool;
using Core.EntitySystem;

namespace Game.Gameplay.WeaponsSystem
{
    public class Projectile : Entity
    {
        private const float DURATION_IN_SECONDS = 5;

        private Transform cachedTransform;
        private Vector3 movement = Vector3.zero;
        private float durationLeft;
        private int damage;
        private AmmoType type;
        
        public void Init(Vector3 position, Quaternion direction, float speed, int damage, AmmoType type)
        {
            this.type = type;
            this.damage = damage;
            if (cachedTransform == null)
                cachedTransform = transform;
            cachedTransform.position = position;
            cachedTransform.rotation = direction;
            movement.z = speed;
            durationLeft = DURATION_IN_SECONDS;
        }

        public override void UpdateBehaviour(float deltaTime)
        {
            durationLeft -= deltaTime;
            if (durationLeft <= 0)
                OnReachedEndOfLifetime();
            else
                cachedTransform.Translate(movement * deltaTime);

            base.UpdateBehaviour(deltaTime);
        }

        private void OnReachedEndOfLifetime()
        {
            RemoveFromScene();
        }

        private void OnTriggerEnter(Collider other)
        {
            var player = other.gameObject.GetComponent<Units.PlayerController>();
            var life = player == null ? other.gameObject.GetComponent<Units.EnemyController>().Life : player.Life;
            life.AddDamage(damage, type);
            PoolManager.Instance.Release(this);
        }
    }
}
