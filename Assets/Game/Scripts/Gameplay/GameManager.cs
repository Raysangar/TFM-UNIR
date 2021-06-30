using UnityEngine;
using UnityEngine.SceneManagement;
using Core.EntitySystem;

namespace Game.Gameplay
{
    public class GameManager : MonoBehaviour
    {
        public System.Action<System.Action> OnLevelAboutToLoad;
        public System.Action OnLevelLoaded;
        public System.Action OnGameFinished;

        public Units.PlayerController Player { get; private set; }

        [SerializeField] Units.PlayerController playerPrefab;
        [SerializeField] CameraController cameraController;
        [SerializeField] LevelsSettings levelsSettings;

        private EntitiesManager entitiesManager;
        private int currentLevelIndex;
        private int targetLevelIndex;
        private LevelController currentLevel;
        private AsyncOperation unloadLevelOperation;
        private AsyncOperation loadLevelOperation;
        private SaveFile saveFile;
        private int selectedSaveFileIndex;

        public void Pause()
        {
            entitiesManager.Pause();
        }

        public void Resume()
        {
            entitiesManager.Resume();
        }

        public void TryLoadNextLevel()
        {
            Pause();
            saveFile.LastLevelIndexCompleted = currentLevelIndex;
            Core.Saves.SavesSystem.TrySave(saveFile, selectedSaveFileIndex);
            targetLevelIndex = currentLevelIndex + 1;
            OnLevelAboutToLoad(StartLoadingNextLevel);
        }

        private void GameFinished()
        {
            saveFile.LastLevelIndexCompleted = -1;
            ++saveFile.TimesGameFinished;
            Core.Saves.SavesSystem.TrySave(saveFile, selectedSaveFileIndex);
            OnGameFinished();
        }

        private void Awake()
        {
            Core.Audio.AudioManager.Instance.AttachTo(cameraController.transform);

            entitiesManager = new EntitiesManager();

            Player = Instantiate(playerPrefab);
            Player.OnDeath += OnPlayerDeath;

            cameraController.Init(Player);

            selectedSaveFileIndex = PlayerPrefs.GetInt(Constants.PlayerPrefsIds.SelectedSaveFileIndexId, 0);
            saveFile = Core.Saves.SavesSystem.TryLoad<SaveFile>(selectedSaveFileIndex) ?? new SaveFile();

            currentLevelIndex = -1;
            targetLevelIndex = saveFile.LastLevelIndexCompleted + 1;
            StartLoadingNextLevel();
        
            SceneManager.activeSceneChanged += OnSceneChanged;
        }

        private void Update()
        {
            entitiesManager.Update(Time.deltaTime);
        }

        private void StartLoadingNextLevel()
        {
            if (currentLevelIndex > -1)
            {
                unloadLevelOperation = SceneManager.UnloadSceneAsync(levelsSettings.Levels[currentLevelIndex].SceneIndex);
                unloadLevelOperation.completed += OnPreviousLevelUnloaded;
            }
            
            loadLevelOperation = SceneManager.LoadSceneAsync(levelsSettings.Levels[targetLevelIndex].SceneIndex, LoadSceneMode.Additive);
            loadLevelOperation.completed += OnNextLevelLoaded;
        }

        private void OnPreviousLevelUnloaded(AsyncOperation _)
        {
            if (loadLevelOperation.isDone)
                OnLevelReady();
        }

        private void OnNextLevelLoaded(AsyncOperation _)
        {
            if (unloadLevelOperation == null || unloadLevelOperation.isDone)
                OnLevelReady();
        }

        private void OnLevelReady()
        {
            currentLevelIndex = targetLevelIndex;
            currentLevel = FindObjectOfType<LevelController>();
            currentLevel.Init(Player, cameraController);
            currentLevel.EndArea.OnPlayerReachedEndOfLevel += OnPlayerReachEndOfLevel;
            Player.ResetValues();
            cameraController.ResetPosition();
            OnLevelLoaded();
            Resume();
        }

        private void OnPlayerReachEndOfLevel()
        {
            if (currentLevelIndex + 1 == levelsSettings.Levels.Length)
                GameFinished();
            else
                TryLoadNextLevel();
        }

        private void OnPlayerDeath()
        {
            Pause();
        }

        private void OnSceneChanged(Scene _, Scene __)
        {
            entitiesManager.ReleaseAllEntities();
        }
    }
}
