using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.MainMenu
{
    public class SettingsMenuController : MonoBehaviour
    {
        [SerializeField] Slider masterVolumeSlider;
        [SerializeField] Slider musicVolumeSlider;
        [SerializeField] Slider effectsVolumeSlider;

        private Core.Audio.AudioManager audioManager;

        public void OnShow()
        {
            masterVolumeSlider.value = audioManager.MasterVolume;
            musicVolumeSlider.value = audioManager.MusicVolume;
            effectsVolumeSlider.value = audioManager.EffectsVolume;
        }

        private void Awake()
        {
            audioManager = Core.Audio.AudioManager.Instance;
            masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
            musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
            effectsVolumeSlider.onValueChanged.AddListener(OnEffectsVolumeChanged);
        }

        private void OnMasterVolumeChanged(float value)
        {
            audioManager.SetMasterVolume(value);
        }

        private void OnMusicVolumeChanged(float value)
        {
            audioManager.SetMusicVolume(value);
        }

        private void OnEffectsVolumeChanged(float value)
        {
            audioManager.SetEffectsVolume(value);
        }
    }
}
