using UnityEngine;
using Core.EntitySystem;
using Game.Gameplay.Units;

namespace Game.Gameplay
{
    public class CameraController : Entity
    {
        [SerializeField] Vector3 distanceFromPlayer;
        [SerializeField] AnimationCurve reachPlayerSpeedCurve;
        [SerializeField] float reachPlayerSpeedDuration;

        private PlayerController player;
        private Transform cachedTransform;
        private float timeFollowingPlayer;

        public void Init(PlayerController player)
        {
            this.player = player;
            cachedTransform = transform;
            timeFollowingPlayer = 0;
            cachedTransform.position = player.Movement.Position + distanceFromPlayer;    
        }

        public override void UpdateBehaviour(float deltaTime)
        {
            Vector3 currentPosition = cachedTransform.position;
            Vector3 targetPosition = player.Movement.Position + distanceFromPlayer;
            if ((targetPosition - currentPosition).sqrMagnitude < Constants.DistanceThreshold)
            {
                timeFollowingPlayer = 0;
                cachedTransform.position = targetPosition;
            }
            else
            {
                timeFollowingPlayer += deltaTime;
                float speed = reachPlayerSpeedCurve.Evaluate(timeFollowingPlayer / reachPlayerSpeedDuration) * player.Movement.CurrentSpeed;
                cachedTransform.Translate((targetPosition - currentPosition).normalized * deltaTime * speed, Space.World);
            }
            base.UpdateBehaviour(deltaTime);
        }
    }
}
