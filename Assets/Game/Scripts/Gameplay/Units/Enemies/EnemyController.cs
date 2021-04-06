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

        private void Update()
        {
            if (player != null)
                UdateBehaviour();
        }

        private void UdateBehaviour()
        {
            var playerPosition = player.Movement.Position;
            Movement.SetLookTarget(playerPosition);

            if (Vector3.Distance(playerPosition, Movement.Position) <= settings.DistanceToPlayerToStartShooting)
                Weapon.StartShooting();
            else
                Weapon.StopShooting();
        }

        private void OnDeathCallback()
        {
            gameObject.SetActive(false);
        }
    }
}
