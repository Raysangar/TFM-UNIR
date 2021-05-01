using UnityEngine;
using UnityEngine.SceneManagement;
using Core.EntitySystem;

namespace Game.Gameplay
{
    public class GameManager : MonoBehaviour
    {
        public Units.PlayerController Player { get; private set; }

        [SerializeField] Units.PlayerController playerPrefab;
        [SerializeField] CameraController cameraController;

        private EntitiesManager entitiesManager;

        public void Pause()
        {
            entitiesManager.Pause();
        }

        public void Resume()
        {
            entitiesManager.Resume();
        }

        private void Awake()
        {
            entitiesManager = new EntitiesManager();

            Player = Instantiate(playerPrefab);
            Player.OnDeath += OnPlayerDeath;

            cameraController.Init(Player.transform);
        
            var enemies = FindObjectsOfType<Units.EnemyController>(true);
            foreach (var enemy in enemies)
                enemy.InitBehaviour(Player);
        }

        private void Update()
        {
            entitiesManager.Update(Time.deltaTime);
        }

        private void OnPlayerDeath()
        {
        }

        private void OnDestroy()
        {
            entitiesManager.ReleaseAllEntities();
        }
    }
}
