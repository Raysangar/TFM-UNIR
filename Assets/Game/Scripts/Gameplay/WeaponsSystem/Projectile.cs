using UnityEngine;
using Core.Utils.Pool;
using Core.EntitySystem;

namespace Game.Gameplay.WeaponsSystem
{
    public class Projectile : Entity
    {
        [SerializeField] Renderer projectileRenderer;

        private const float DURATION_IN_SECONDS = 5;

        private Transform cachedTransform;
        private Vector3 movement = Vector3.zero;
        private float durationLeft;
        private AmmoType type;
        private WeaponSettings.ProjectileSettings settings;
        private float distanceTraveled;
        
        public void Init(Transform startingTransform, WeaponSettings.AmmoSettings ammoSettings, WeaponSettings.ProjectileSettings settings)
        {
            this.settings = settings;
            type = ammoSettings.Type;

            if (type != AmmoType.Enemy)
                projectileRenderer.material.color = ammoSettings.Color;
            
            if (cachedTransform == null)
                cachedTransform = transform;
            cachedTransform.position = startingTransform.position;
            cachedTransform.rotation = startingTransform.rotation;
            
            movement.z = settings.Speed;
            durationLeft = DURATION_IN_SECONDS;
            distanceTraveled = 0;
        }

        public override void UpdateBehaviour(float deltaTime)
        {
            durationLeft -= deltaTime;
            if (durationLeft <= 0)
            {
                OnReachedEndOfLifetime();
            }
            else
            {
                Vector3 translation = movement * deltaTime;
                if (distanceTraveled < settings.StartFallingAfterDistance)
                {
                    distanceTraveled += translation.magnitude;
                    if (distanceTraveled >= settings.StartFallingAfterDistance)
                        movement.y = -settings.FallSpeed;
                }
                cachedTransform.Translate(translation);
            }

            base.UpdateBehaviour(deltaTime);
        }

        private void OnReachedEndOfLifetime()
        {
            RemoveFromScene();
        }

        private void OnTriggerEnter(Collider other)
        {
            switch(other.gameObject.layer)
            {
                case Constants.PhysicLayers.Player:
                    var player = other.gameObject.GetComponent<Units.PlayerController>();
                    player.Life.AddDamage(settings.Damage, type);
                    break;
                case Constants.PhysicLayers.Enemies:
                    var enemy = other.gameObject.GetComponent<Units.EnemyController>();
                    enemy.Life.AddDamage(settings.Damage, type);
                    break;
                default:
                    break;

            }
            PoolManager.Instance.Release(this);
        }

    }
}
