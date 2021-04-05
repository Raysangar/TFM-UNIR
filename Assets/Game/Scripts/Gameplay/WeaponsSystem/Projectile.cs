using UnityEngine;
using Core.Utils.Pool;

namespace Game.Gameplay.WeaponsSystem
{
    public class Projectile : PoolObject
    {
        private const float DURATION_IN_SECONDS = 5;

        private Transform cachedTransform;
        private Vector3 movement = Vector3.zero;
        private float durationLeft;
        private int damage;
        
        public void Init(Vector3 position, Quaternion direction, float speed, int damage)
        {
            this.damage = damage;
            if (cachedTransform == null)
                cachedTransform = transform;
            cachedTransform.position = position;
            cachedTransform.rotation = direction;
            movement.z = speed;
            durationLeft = DURATION_IN_SECONDS;
        }

        private void Update()
        {
            float delta = Time.deltaTime;
            durationLeft -= delta;
            if (durationLeft <= 0)
                OnReachedEndOfLifetime();
            else
                cachedTransform.Translate(movement * delta);
        }

        private void OnReachedEndOfLifetime()
        {
            PoolManager.Instance.Release(this);
        }

        private void OnCollisionEnter(Collision collision)
        {
            PoolManager.Instance.Release(this);
            var collisionLife = collision.gameObject.GetComponent<Units.Life>();
            if (collisionLife != null)
                collisionLife.AddDamage(damage);
        }
    }
}
