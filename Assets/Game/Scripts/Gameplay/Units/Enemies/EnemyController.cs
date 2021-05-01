using UnityEngine;

namespace Game.Gameplay.Units
{
    public class EnemyController : BaseUnit<EnemySettings>
    {
        private PlayerController player;

        protected override void Awake()
        {
            base.Awake();
            Life.OnDeath += OnDeathCallback;
        }

        public void InitBehaviour(PlayerController player)
        {
            this.player = player;
        }

        public override void UpdateBehaviour(float deltaTime)
        {
            var playerPosition = player.Movement.Position;
            Movement.SetLookTarget(playerPosition);

            if (Vector3.Distance(playerPosition, Movement.Position) <= settings.DistanceToPlayerToStartShooting)
                Weapon.StartShooting();
            else
                Weapon.StopShooting();

            base.UpdateBehaviour(deltaTime);
        }

        private void OnDeathCallback()
        {
            RemoveFromScene();
        }
    }
}
