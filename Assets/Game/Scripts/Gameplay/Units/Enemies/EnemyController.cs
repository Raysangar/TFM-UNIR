using UnityEngine;
using Core.AI;

namespace Game.Gameplay.Units
{
    public class EnemyController : BaseUnit<EnemySettings>
    {
        private FiniteStateMachineComponent finiteStateMachine;
        private PlayerController player;
        private Transform[] patrolNodes;
        private int targetPatrolNode;

        private bool ShouldMoveInPatrol => patrolNodes != null && patrolNodes.Length > 1;

        protected override void Awake()
        {
            base.Awake();
            Life.OnDeath += OnDeathCallback;

            finiteStateMachine = new FiniteStateMachineComponent(this, false,
                new FiniteState[]
                {
                    new FiniteState(OnPatrolStarted, null, PatrolBehaviour, new System.Func<bool>[] {null, ShouldFollowPlayer, ShouldStartShootingPlayer}),
                    new FiniteState(OnStartFollowingPlayer, Stop, FollowPlayerBehaviour, new System.Func<bool>[] {null, null, ShouldStartShootingPlayer}),
                    new FiniteState(OnStartShooting, OnStopShooting, AimPlayerBehaviour, new System.Func<bool>[] {null, ShouldFollowPlayer, null})
                }
            )
            {
                Enabled = false
            };
        }

        public void InitBehaviour(PlayerController player, Transform[] patrolNodes = null)
        {
            this.player = player;
            this.patrolNodes = patrolNodes;
            finiteStateMachine.Enabled = true;
            finiteStateMachine.Reset();
        }

        private void OnPatrolStarted()
        {
            animator.SetBool(WalkAnimationId, ShouldMoveInPatrol);
        }

        private void PatrolBehaviour(float deltaTime)
        {
            if (ShouldMoveInPatrol)
            {
                if (Vector3.Distance(Movement.Position, patrolNodes[targetPatrolNode].position) < Constants.DistanceThreshold)
                    targetPatrolNode = ++targetPatrolNode % patrolNodes.Length;
                GoTo(patrolNodes[targetPatrolNode].position);
            }
        }

        private bool ShouldFollowPlayer()
        {
            var playerPosition = player.Movement.Position;
            float distanceToPlayer = Vector3.Distance(playerPosition, Movement.Position);
            return distanceToPlayer <= settings.DistanceToPlayerToStartFollowing && distanceToPlayer > settings.DistanceToPlayerToStartShooting;
        }

        private bool ShouldStartShootingPlayer()
        {
            var playerPosition = player.Movement.Position;
            float distanceToPlayer = Vector3.Distance(playerPosition, Movement.Position);
            return distanceToPlayer <= settings.DistanceToPlayerToStartShooting;
        }

        private void OnStartShooting()
        {
            Weapon.StartShooting();
            animator.SetBool(ShootAnimationId, true);
        }

        private void AimPlayerBehaviour(float deltaTime)
        {
            Movement.SetLookTarget(player.Movement.Position);
        }

        private void OnStopShooting()
        {
            Weapon.StopShooting();
            animator.SetBool(ShootAnimationId, false);
        }

        private void FollowPlayerBehaviour(float deltaTime)
        {
            GoTo(player.Movement.Position);
        }

        private void OnStartFollowingPlayer()
        {
            animator.SetBool(WalkAnimationId, true);
        }

        private void Stop()
        {
            Movement.SetMovement(Vector3.zero);
            animator.SetBool(WalkAnimationId, false);
        }

        private void OnDeathCallback()
        {
            RemoveFromScene();
        }

        private void GoTo(Vector3 position)
        {
            Movement.SetLookTarget(position);
            Movement.SetMovement((position - Movement.Position).normalized);
        }
    }
}
