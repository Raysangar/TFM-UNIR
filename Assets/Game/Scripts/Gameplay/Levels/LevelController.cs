using UnityEngine;
using Game.Gameplay.Units;

namespace Game.Gameplay
{
    public class LevelController : MonoBehaviour
    {
        public LevelEndArea EndArea => endArea;
        public int UnlockLevelEndPickupsTotalCount { get; private set; }
        public int UnlockLevelEndPickupsEarnedCount { get; private set; }

        [SerializeField] Transform playerInitialPosition;
        [SerializeField] Transform enemiesParent;
        [SerializeField] Transform unlockLevenEndPickupsParent;
        [SerializeField] LevelEndArea endArea;

        public void Init(PlayerController player)
        {
            player.transform.position = playerInitialPosition.position;
            player.transform.rotation = playerInitialPosition.rotation;

            if (enemiesParent != null)
            {
                var enemies = enemiesParent.GetComponentsInChildren<EnemyController>();
                foreach (var enemy in enemies)
                    enemy.InitBehaviour(player);
            }

            if (unlockLevenEndPickupsParent != null)
            {
                var unlockLevelEndPickups = unlockLevenEndPickupsParent.GetComponentsInChildren<UnlockLevelEndPickup>();
                UnlockLevelEndPickupsTotalCount = unlockLevelEndPickups.Length;
                foreach (var pickup in unlockLevelEndPickups)
                    pickup.OnPlayerEarned += OnPlayerGotUnlockLevelEndPickup;

                if (UnlockLevelEndPickupsTotalCount > 0)
                    endArea.gameObject.SetActive(false);
            }
        }

        private void OnPlayerGotUnlockLevelEndPickup()
        {
            if (++UnlockLevelEndPickupsEarnedCount == UnlockLevelEndPickupsTotalCount)
                endArea.gameObject.SetActive(true);
        }
    }
}
