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

        protected override void Awake()
        {
            base.Awake();
            Life.OnDeath += OnDeathCallback;

            finiteStateMachine = new FiniteStateMachineComponent(this, false,
                new FiniteState[]
                {
                    new FiniteState(null, null, PatrolBehaviour, new System.Func<bool>[] {null, ShouldFollowPlayer, ShouldStartShootingPlayer}),
                    new FiniteState(null, Stop, FollowPlayerBehaviour, new System.Func<bool>[] {null, null, ShouldStartShootingPlayer}),
                    new FiniteState(Weapon.StartShooting, Weapon.StopShooting, AimPlayerBehaviour, new System.Func<bool>[] {null, ShouldFollowPlayer, null})
                }
            );
            disabledComponents.Add(finiteStateMachine);
        }

        public void InitBehaviour(PlayerController player, Transform[] patrolNodes)
        {
            this.player = player;
            this.patrolNodes = patrolNodes;
            finiteStateMachine.Enabled = true;
            finiteStateMachine.Reset();
        }

        private void PatrolBehaviour(float deltaTime)
        {
            if (Vector3.Distance(Movement.Position, patrolNodes[targetPatrolNode].position) < Constants.DistanceThreshold)
                targetPatrolNode = ++targetPatrolNode % patrolNodes.Length;
            GoTo(patrolNodes[targetPatrolNode].position);
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

        private void AimPlayerBehaviour(float deltaTime)
        {
            Movement.SetLookTarget(player.Movement.Position);
        }

        private void FollowPlayerBehaviour(float deltaTime)
        {
            GoTo(player.Movement.Position);
        }

        private void Stop()
        {
            Movement.SetMovement(Vector3.zero);
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
