using UnityEngine;
using Core.EntitySystem;
using Game.Gameplay.Units;

namespace Game.Gameplay
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : Entity
    {
        public Camera Camera { get; private set; }

        [Header("Movement")]
        [SerializeField] Vector3 distanceFromPlayer;
        [SerializeField] AnimationCurve reachPlayerSpeedCurve;
        [SerializeField] float reachPlayerSpeedDuration;
        [SerializeField] AnimationCurve reduceDistanceSpeedCurve;
        [SerializeField] float startingDistanceToReduceSpeed;

        [Header("Angle")]
        [SerializeField] float rotationAnimSpeed;
        [SerializeField] AnimationCurve rotationAnimCurve;

        private PlayerController player;
        private Transform cachedTransform;
        private float timeFollowingPlayer;
        private float previousDistance;
        private Vector3 initialRotation;
        private Vector3 targetRotation;
        private float timeRotating;
        private float currentAnimDuration;

        public void Init(PlayerController player)
        {
            this.player = player;
            Camera = GetComponent<Camera>();
            cachedTransform = transform;
            ResetState(cachedTransform.localEulerAngles);
        }

        public void ResetState(Vector3 rotation)
        {
            initialRotation = rotation;
            targetRotation = rotation;
            timeFollowingPlayer = 0;
            timeRotating = 0;
            currentAnimDuration = 0;
            cachedTransform.position = player.Movement.Position + distanceFromPlayer;
            cachedTransform.localEulerAngles = rotation; 
        }

        public void SetTargetRotation(Vector3 rotation)
        {
            if (targetRotation != rotation)
            {
                initialRotation = cachedTransform.localEulerAngles;
                targetRotation = rotation;
                timeRotating = 0;
                currentAnimDuration = (targetRotation - initialRotation).magnitude / rotationAnimSpeed;
            }
        }

        public override void UpdateBehaviour(float deltaTime)
        {
            UpdateMovement(deltaTime);
            UpdateRotation(deltaTime);
            base.UpdateBehaviour(deltaTime);
        }

        private void UpdateMovement(float deltaTime)
        {
            Vector3 currentPosition = cachedTransform.position;
            Vector3 targetPosition = player.Movement.Position + Quaternion.Euler(0, cachedTransform.localEulerAngles.y, 0) * distanceFromPlayer;
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
        }

        private void UpdateRotation(float deltaTime)
        {
            if (timeRotating < currentAnimDuration)
            {
                timeRotating += deltaTime;
                float lerpValue = rotationAnimCurve.Evaluate(timeRotating / currentAnimDuration);
                cachedTransform.localEulerAngles = Vector3.Lerp(initialRotation, targetRotation, lerpValue);
            }
        }
    }
}
