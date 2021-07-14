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
        [SerializeField] float cameraFieldOfView;
        [SerializeField] Vector3 cameraInitialRotation;
        [SerializeField] Transform cameraRotationSettersParent;
        [SerializeField] Transform unlockLevenEndPickupsParent;
        [SerializeField] LevelEndArea endArea;
        [SerializeField] AudioClip music;

        private CameraController cameraController;

        public void Init(PlayerController player, CameraController cameraController)
        {
            player.transform.position = playerInitialPosition.position;
            player.transform.rotation = playerInitialPosition.rotation;

            this.cameraController = cameraController;
            cameraController.Camera.fieldOfView = cameraFieldOfView;
            cameraController.ResetState(cameraInitialRotation);

            if (cameraRotationSettersParent != null)
            {
                var cameraRotationSetters = cameraRotationSettersParent.GetComponentsInChildren<CameraRotationSetter>(true);
                foreach (var cameraRotationSetter in cameraRotationSetters)
                    cameraRotationSetter.Init(cameraController);
            }

            if (music != null)
                Core.Audio.AudioManager.Instance.PlayMusic(music);

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
                for (int i = 0; i < unlockLevelEndPickups.Length; ++i)
                {
                    var pickup = unlockLevelEndPickups[i];
                    pickup.OnPlayerEarned += OnPlayerGotUnlockLevelEndPickup;
                    pickup.SetVisual(i);
                }

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
