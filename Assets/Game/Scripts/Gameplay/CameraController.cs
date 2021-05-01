using UnityEngine;
using Core.EntitySystem;

namespace Game.Gameplay
{
    public class CameraController : Entity
    {
        private Transform player;
        [SerializeField] Vector3 distanceFromPlayer;

        private Transform cachedTransform;

        public void Init(Transform player)
        {
            this.player = player;
        }

        protected override void Awake()
        {
            base.Awake();
            cachedTransform = transform;
        }

        public override void UpdateBehaviour(float deltaTime)
        {
            cachedTransform.position = player.position + distanceFromPlayer;
            base.UpdateBehaviour(deltaTime);
        }
    }
}
