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
        [SerializeField] AnimationCurve reduceDistanceSpeedCurve;
        [SerializeField] float startingDistanceToReduceSpeed;

        private PlayerController player;
        private Transform cachedTransform;
        private float timeFollowingPlayer;
        private float previousDistance;

        public void Init(PlayerController player)
        {
            this.player = player;
            cachedTransform = transform;
            ResetPosition();
        }

        public void ResetPosition()
        {
            timeFollowingPlayer = 0;
            cachedTransform.position = player.Movement.Position + distanceFromPlayer;
        }

        public override void UpdateBehaviour(float deltaTime)
        {
            Vector3 currentPosition = cachedTransform.position;
            Vector3 targetPosition = player.Movement.Position + distanceFromPlayer;
            float currentDistance = (targetPosition - currentPosition).sqrMagnitude;
            if (currentDistance < Constants.DistanceThreshold)
            {
                timeFollowingPlayer = 0;
                cachedTransform.position = targetPosition;
                previousDistance = 0;
            }
            else
            {
                if (currentDistance < previousDistance && currentDistance <= startingDistanceToReduceSpeed)
                {
                    float speed = reachPlayerSpeedCurve.Evaluate(timeFollowingPlayer / reachPlayerSpeedDuration) * player.Movement.CurrentSpeed;
                    speed *= reduceDistanceSpeedCurve.Evaluate((startingDistanceToReduceSpeed - currentDistance) / startingDistanceToReduceSpeed);
                    cachedTransform.Translate((targetPosition - currentPosition).normalized * deltaTime * speed, Space.World);
                }
                else
                {
                    timeFollowingPlayer += deltaTime;
                    float speed = reachPlayerSpeedCurve.Evaluate(timeFollowingPlayer / reachPlayerSpeedDuration) * player.Movement.CurrentSpeed;
                    cachedTransform.Translate((targetPosition - currentPosition).normalized * deltaTime * speed, Space.World);
                }
                previousDistance = currentDistance;
            }
            base.UpdateBehaviour(deltaTime);
        }
    }
}
