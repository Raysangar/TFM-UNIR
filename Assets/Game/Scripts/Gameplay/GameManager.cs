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
        [SerializeField] LevelsSettings levelsSettings;

        private EntitiesManager entitiesManager;
        private int currentLevelIndex;
        private LevelController currentLevel;
        private AsyncOperation unloadLevelOperation;
        private AsyncOperation loadLevelOperation;

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

            cameraController.Init(Player);

            currentLevelIndex = -1;
            LoadNextLevel();
        
            SceneManager.activeSceneChanged += OnSceneChanged;
        }

        private void Update()
        {
            entitiesManager.Update(Time.deltaTime);
        }

        private void LoadNextLevel()
        {
            if (currentLevelIndex > -1)
            {
                unloadLevelOperation = SceneManager.UnloadSceneAsync(levelsSettings.Levels[currentLevelIndex].SceneIndex);
                unloadLevelOperation.completed += OnPreviousLevelUnloaded;
            }

            ++currentLevelIndex;
            
            loadLevelOperation = SceneManager.LoadSceneAsync(levelsSettings.Levels[currentLevelIndex].SceneIndex, LoadSceneMode.Additive);
            loadLevelOperation.completed += OnNextLevelLoaded;
        }

        private void OnPreviousLevelUnloaded(AsyncOperation _)
        {
            if (loadLevelOperation.isDone)
                OnLevelReady();
        }

        private void OnNextLevelLoaded(AsyncOperation _)
        {
            if (unloadLevelOperation != null && unloadLevelOperation.isDone)
                OnLevelReady();
        }

        private void OnLevelReady()
        {
            currentLevel = FindObjectOfType<LevelController>();
            currentLevel.Init(Player);
        }

        private void OnPlayerDeath()
        {
        }

        private void OnSceneChanged(Scene _, Scene __)
        {
            entitiesManager.ReleaseAllEntities();
        }
    }
}
